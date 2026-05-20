using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;

namespace WebApplication1.Filters
{
    public class AuthTokenFilter : IAsyncActionFilter
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthTokenFilter(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Cookies["token"];

            if (!string.IsNullOrWhiteSpace(token))
            {
                context.HttpContext.Items["token"] = token;
            }

           

            await next();
        }
    }
}