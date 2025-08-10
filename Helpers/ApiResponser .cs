using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PaymentApi.Helpers
{
    public class ApiResponse
    {
        public required Meta Meta { get; set; }
        public object? Data { get; set; } = string.Empty;
    }

    public class Meta
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public required string Message { get; set; }
    }

    public class ApiResponser : ControllerBase
    {
        protected IActionResult SuccessResponse(object data, int statusCode = 200, string metaMessage = "Request success")
        {
            return new ObjectResult(new ApiResponse
            {
                Meta = new Meta
                {
                    Success = true,
                    Code = 20000,
                    Message = metaMessage
                },
                Data = data
            })
            {
                StatusCode = statusCode
            };
        }

        protected IActionResult ErrorResponse(string errorMessage, int statusCode, int metaCode)
        {
            return new ObjectResult(new ApiResponse
            {
                Meta = new Meta
                {
                    Success = false,
                    Code = metaCode,
                    Message = errorMessage
                },
                Data = null
            })
            {
                StatusCode = statusCode
            };
        }

        protected IActionResult ShowAll<T>(IEnumerable<T> collection, int? statusCode = null, string? metaMessage = null)
        {
            return SuccessResponse(
                collection.ToList(),
                statusCode ?? 200,
                metaMessage ?? "Request success"
            );
        }

        protected IActionResult ShowOne<T>(
           [MaybeNull] T item,
           int? statusCode = null,
           string? metaMessage = null) where T : class
        {
            if (item == null)
            {
                return ErrorResponse(
                    errorMessage: "Item not found",
                    statusCode: 404,
                    metaCode: 40400);
            }

            return SuccessResponse(
                data: item,
                statusCode: statusCode ?? 200,
                metaMessage: metaMessage ?? "Request success");
        }


        protected IActionResult Paginate<T>(IEnumerable<T> items, int total, int currentPage, int perPage, string path, string resultKey = "data", string? metaMessage = null)
        {
            var lastPage = (int)Math.Ceiling((double)total / perPage);

            return new ObjectResult(new ApiResponse
            {
                Meta = new Meta
                {
                    Success = true,
                    Code = 20000,
                    Message = metaMessage ?? "Request success"
                },
                Data = new
                {
                    PageInfo = new
                    {
                        LastPage = lastPage,
                        CurrentPage = currentPage,
                        Path = path,
                        Total = total,
                        PerPage = perPage
                    },
                    Data = new Dictionary<string, object>
                    {
                        [resultKey] = items
                    }
                }
            });
        }
    }
}