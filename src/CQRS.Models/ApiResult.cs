using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Models
{
    public class ApiResult : IActionResult
    {
        public Result Value { get; set; }

        public ApiResult(Result value)
        {
            Value = value;
        }

        public static implicit operator ApiResult(Result value)
        {
            return new ApiResult(value);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {            
            var result = new JsonResult(Value)
            {
                StatusCode = (int)(Value.Failure?.GetStatusCode() ?? HttpStatusCode.OK)
            };

            await result.ExecuteResultAsync(context);
        }
    }
}
