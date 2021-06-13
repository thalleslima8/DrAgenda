using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DrAgenda.Web.Helpers {
    public class JsonExceptionFilterAttribute : IExceptionFilter {
        public void OnException(ExceptionContext filterContext) {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                filterContext.Result = new JsonResult(filterContext.Exception);
            }
        }
    }
}
