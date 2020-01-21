using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Models
{
    public class ApiResult : IActionResult
    {
        public object Value { get; set; }

        public int? StatusCode { get; set; }

        public ApiResult(object value, int? statusCode = null)
        {
            Value = value;
            StatusCode = statusCode;
        }

        public static implicit operator ApiResult(Result value)
        {
            return new ApiResult(value, value.Failure?.GetStatusCode());
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {            
            var result = new JsonResult(Value)
            {
                StatusCode = StatusCode
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
