using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Filters
{
    public class RecursiveValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg != null)
                {
                    var validationResults = new List<ValidationResult>();
                    bool isValid = TryValidateObjectRecursive(arg, validationResults);

                    if (!isValid)
                    {
                        var errors = new Dictionary<string, string>();
                        foreach (var validationResult in validationResults)
                        {
                            var memberName = validationResult.MemberNames != null
                                ? string.Join(",", validationResult.MemberNames)
                                : "";
                            errors[memberName] = validationResult.ErrorMessage!;
                        }

                        context.Result = new BadRequestObjectResult(new
                        {
                            meta = new
                            {
                                success = false,
                                code = 42200,
                                message = validationResults[0].ErrorMessage
                            },
                            data = (object?)null,
                            errors = errors
                        });
                        return;
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private bool TryValidateObjectRecursive(object obj, List<ValidationResult> results)
        {
            var context = new ValidationContext(obj, null, null);
            bool result = Validator.TryValidateObject(obj, context, results, true);

            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string) || property.PropertyType.IsValueType)
                    continue;

                var value = property.GetValue(obj);

                if (value == null)
                    continue;

                if (value is IEnumerable enumerable)
                {
                    foreach (var element in enumerable)
                    {
                        if (element != null)
                            result &= TryValidateObjectRecursive(element, results);
                    }
                }
                else
                {
                    result &= TryValidateObjectRecursive(value, results);
                }
            }

            return result;
        }
    }
}
