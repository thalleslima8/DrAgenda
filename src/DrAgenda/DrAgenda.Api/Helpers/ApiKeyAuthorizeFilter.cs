using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DrAgenda.Api.Helpers
{
    public class ApiKeyAuthorizeFilter : ActionFilterAttribute
    {
        private readonly ApiSettings _apiSettings;

        public ApiKeyAuthorizeFilter(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("apikey"))
            {
                context.Result = new StatusCodeResult(403);
            }
            else
            {
                var apiKey = context.HttpContext.Request.Headers["apikey"].ToString();

                if (string.IsNullOrWhiteSpace(apiKey) || apiKey != _apiSettings.ApiKey)
                    context.Result = new StatusCodeResult(403);                
            }
        }
    }
}