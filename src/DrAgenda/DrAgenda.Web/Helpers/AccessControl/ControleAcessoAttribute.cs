using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DrAgenda.Web.Helpers.AccessControl
{
    public class ControleAcessoAttribute : Attribute, IActionFilter
    {
        public ControleAcessoAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string Action { get; set; }

        public bool IsController { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var appUserState = context.HttpContext.RequestServices.GetService<AppUserState>();

            bool permitted = false;

            if (appUserState.IsAuthenticated)
            {
                if (appUserState.IsInRole("Admin") || IsController)
                {
                    permitted = true;
                }
                else
                {
                    var controllerName = context.RouteData.Values["controller"].ToString().ToLower();
                    var actionName = string.IsNullOrWhiteSpace(Action) ? context.RouteData.Values["action"].ToString().ToLower() : Action.ToLower();
                    var acao = $"{controllerName}.{actionName}";
                    permitted = appUserState.IsInRole(acao);
                }
            }

            if (!permitted)
            {
                Redirect(context, appUserState.IsAuthenticated);
            }
        }

        private void Redirect(ActionExecutingContext context, bool isAuthenticated)
        {
            if (IsAjaxRequest(context.HttpContext.Request))
            {
                context.Result = isAuthenticated
                    ? new JsonResult(new { result = false, message = "Acesso não autorizado!" })
                    : new JsonResult(new { result = false, message = "Sua login expirou, efetue login novamente!" });
            }
            else
            {
                context.Result = isAuthenticated
                    ? new RedirectResult("~/Home/NaoAutorizado")
                    : new RedirectResult("~/Home/Login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }


    }
}