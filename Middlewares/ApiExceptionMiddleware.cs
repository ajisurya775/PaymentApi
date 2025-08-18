using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaymentApi.Helpers; // namespace ApiResponser dan ApiResponse

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;

    public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var response = context.Response;
            response.ContentType = "application/json";

            ApiResponse apiResponse;
            int statusCode;
            int metaCode;

            // Contoh handling beberapa exception
            switch (ex)
            {
                case Microsoft.EntityFrameworkCore.DbUpdateException dbEx when dbEx.InnerException != null && dbEx.InnerException.Message.Contains("1451"):
                    statusCode = (int)HttpStatusCode.Conflict;
                    metaCode = 40900;
                    apiResponse = new ApiResponse
                    {
                        Meta = new Meta
                        {
                            Success = false,
                            Code = metaCode,
                            Message = "Cannot remove this resource permanently. It is related with other resources"
                        },
                        Data = null
                    };
                    break;

                case System.ComponentModel.DataAnnotations.ValidationException _:
                    statusCode = 422;
                    metaCode = 42200;
                    apiResponse = new ApiResponse
                    {
                        Meta = new Meta
                        {
                            Success = false,
                            Code = metaCode,
                            Message = "Validation error"
                        },
                        Data = null
                    };
                    break;

                case UnauthorizedAccessException _:
                    statusCode = 401;
                    metaCode = 40100;
                    apiResponse = new ApiResponse
                    {
                        Meta = new Meta
                        {
                            Success = false,
                            Code = metaCode,
                            Message = "Unauthenticated."
                        },
                        Data = null
                    };
                    break;
                case InvalidOperationException _:
                    statusCode = 404;
                    metaCode = 40400;
                    apiResponse = new ApiResponse
                    {
                        Meta = new Meta
                        {
                            Success = false,
                            Code = metaCode,
                            Message = "Resource not found"
                        },
                        Data = null
                    };
                    break;


                default:
                    statusCode = 500;
                    metaCode = 50000;
                    var msg = context.RequestServices.GetService<IHostEnvironment>()?.IsDevelopment() == true
                        ? ex.Message
                        : "Unexpected Exception. Try later";

                    apiResponse = new ApiResponse
                    {
                        Meta = new Meta
                        {
                            Success = false,
                            Code = metaCode,
                            Message = msg
                        },
                        Data = null
                    };
                    break;
            }

            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(apiResponse);
        }
    }
}
