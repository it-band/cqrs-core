using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Models
{
    public class ApiResult : IActionResult
    {
        public Result Value { get; set; }

        public int? StatusCode { get; set; }

        public ApiResult(Result value, int? statusCode = null)
        {
            Value = value;
            StatusCode = statusCode;
        }

        public static implicit operator ApiResult(Result value)
        {
            return new ApiResult(value);
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

    public class ApiResultExecutor
    {
        public virtual async Task ExecuteAsync(ActionContext context, ApiResult result)
        {
            var jsonResult = new JsonResult(result.Value)
            {
                StatusCode = result.StatusCode ?? result.Value.GetStatusCode()
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
