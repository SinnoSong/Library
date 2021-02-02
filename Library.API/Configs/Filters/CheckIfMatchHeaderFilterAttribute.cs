using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;

namespace Library.API.Configs.Filters
{
    public class CheckIfMatchHeaderFilterAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey(HeaderNames.IfMatch))
            {
                context.Result = new BadRequestObjectResult(new ApiError
                {
                    Message = "必须提供If-Match消息头"
                });
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}