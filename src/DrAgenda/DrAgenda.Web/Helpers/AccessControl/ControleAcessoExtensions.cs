using System.Collections.Generic;
using System.Linq;
using DrAgenda.Shared.Dto.ControleAcesso;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DrAgenda.Web.Helpers.AccessControl
{
    public static class ControleAcessoExtensions
    {
        public static bool HasPermission(this UsuarioDto user, string action, string controller)
        {
            var permitido = false;

            if (user != null)
            {
                if (user.Admin)
                {
                    permitido = true;
                }
                else
                {
                    var requiredPermission = $"{controller}.{action}".ToLower();
                    permitido = user.PerfisAcesso.Any(x => x.PermissoesAcesso.Any(p => p.Acao == requiredPermission));
                }
            }

            return permitido;
        }

        public static bool HasPermission(this UsuarioDto user, string controller)
        {
            var permitido = false;

            if (user != null)
            {
                if (user.Admin)
                {
                    permitido = true;
                }
                else
                {
                    var requiredPermission = $"{controller}".ToLower();
                    permitido = user.PerfisAcesso.Any(x => x.PermissoesAcesso.Any(p => p.Acao == requiredPermission));
                }
            }

            return permitido;
        }

        public static HtmlString ButtonHasPermission(this IHtmlHelper helper, string action, string controller, string innerHtml, object htmlAttributes)
        {
            return ButtonHasPermission(helper, action, controller, innerHtml,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)
            );
        }

        public static HtmlString ButtonHasPermission(this IHtmlHelper helper, string action, string controller, string innerHtml, IDictionary<string, object> htmlAttributes)
        {
            var user = helper.ViewContext.HttpContext.Session.Get<UsuarioDto>(nameof(UsuarioDto));

            if (!HasPermission(user, action, controller))
                return new HtmlString(string.Empty);

            var builder = new TagBuilder("button");
            builder.InnerHtml.AppendHtml(innerHtml);
            builder.MergeAttributes(htmlAttributes);
            
            return new HtmlString(builder.ToString());
        }

        public static HtmlString LinkHasPermission(this IHtmlHelper helper, string action, string controller, string href, string content, object htmlAttributes)
        {
            return LinkHasPermission(helper, action, controller, href, content, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)
            );
        }

        public static HtmlString LinkHasPermission(this IHtmlHelper helper, string action, string controller, string href, string content, IDictionary<string, object> htmlAttributes)
        {
            var user = helper.ViewContext.HttpContext.Session.Get<UsuarioDto>(nameof(UsuarioDto));

            if (!HasPermission(user, action, controller))
                return new HtmlString(string.Empty);

            var builder = new TagBuilder("a");
            builder.InnerHtml.AppendHtml(content);
            builder.Attributes.Add("href", href);

            builder.MergeAttributes(htmlAttributes);
            return new HtmlString(builder.ToString());
        }
    }
}