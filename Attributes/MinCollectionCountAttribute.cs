using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Attributes
{
    public class MinCollectionCountAttribute : ValidationAttribute
    {
        private readonly int _minCount;

        public MinCollectionCountAttribute(int minCount)
        {
            _minCount = minCount;
            ErrorMessage = $"The collection must contain at least {_minCount} item(s).";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false; // null dianggap invalid

            if (value is ICollection collection)
            {
                return collection.Count >= _minCount;
            }

            return false; // bukan collection dianggap invalid
        }
    }
}
