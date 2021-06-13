using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace DrAgenda.Web.Helpers.TagHelpers
{
    [HtmlTargetElement("a-in-role")]
    public class AnchorInRoleTagHelper : TagHelper
    {
        private const string ActionAttributeName = "asp-action";
        private const string ControllerAttributeName = "asp-controller";
        private const string RouteValuesPrefix = "asp-route-";

        private HttpContext _context;

        public AnchorInRoleTagHelper(IActionContextAccessor context, IHtmlGenerator generator)
        {
            Generator = generator;
            _context = context.ActionContext.HttpContext;
        }

        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// Gets or sets the <see cref="Rendering.ViewContext"/> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// The name of the action method.
        /// </summary>
        /// <remarks>Must be <c>null</c> if <see cref="Route"/> is non-<c>null</c>.</remarks>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        /// <summary>
        /// The name of the controller.
        /// </summary>
        /// <remarks>Must be <c>null</c> if <see cref="Route"/> is non-<c>null</c>.</remarks>
        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        /// <summary>
        /// Additional parameters for the route.
        /// </summary>
        [HtmlAttributeName(DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var appUserState = new AppUserState(_context);

            if (!(appUserState.IsInRole("Admin") || appUserState.IsInRole($"{Controller}.{Action}".ToLower())))
            {
                output.SuppressOutput();
            }
            else
            {
                output.TagName = "a";

                var routeValues = RouteValues.ToDictionary(
                    kvp => kvp.Key,
                    kvp => (object) kvp.Value,
                    StringComparer.OrdinalIgnoreCase);


                var tagBuilder = Generator.GenerateActionLink(
                    ViewContext,
                    linkText: string.Empty,
                    actionName: Action,
                    controllerName: Controller,
                    protocol: null,
                    hostname: null,
                    fragment: null,
                    routeValues: routeValues,
                    htmlAttributes: null);

                    output.MergeAttributes(tagBuilder);
            }
            
        }
    }
}
