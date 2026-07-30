using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Attributes
{
    public class RedisCashAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSeconds;

        public RedisCashAttribute(int durationInSeconds = 60)
        {
            _durationInSeconds = durationInSeconds;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachService = context.HttpContext.RequestServices.GetRequiredService<ICachService>();
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var data = await cachService.GetDataAsync(cacheKey);
            if(!string.IsNullOrEmpty(data))
            {
                context.Result = new ContentResult()
                {
                    Content = data,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }

            var executedContext = await next.Invoke();
            if(executedContext.Result is OkObjectResult {Value: not null } ok)
            {
                await cachService.SetAsync(cacheKey, ok.Value , TimeSpan.FromSeconds(_durationInSeconds));
            }
        }

        private static string CreateCacheKey (HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            if(request.Query.Any())
            {
                key.Append('?');
                foreach(var (k , v) in request.Query.OrderBy(x => x.Key))
                {
                    key.Append(k).Append("=").Append(v).Append('&');
                }
            }
            return key.ToString();
        }
    }
}
