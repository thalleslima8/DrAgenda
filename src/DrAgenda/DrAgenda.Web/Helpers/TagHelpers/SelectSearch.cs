using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace DrAgenda.Web.Helpers.TagHelpers
{
    [HtmlTargetElement("search")]
    public class SelectSearch: TagHelper
    {
        private readonly IActionContextAccessor _context;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private IHtmlHelper _htmlHelper;
        private const string ActionAttributeName = "asp-action";
        private const string ControllerAttributeName = "asp-controller";
        private const string RouteValuesPrefix = "asp-route-";

        public SelectSearch(IActionContextAccessor context, 
            IHtmlGenerator generator, 
            IUrlHelperFactory urlHelperFactory,
            IHtmlHelper htmlHelper)
        {
            _context = context;
            _urlHelperFactory = urlHelperFactory;
            _htmlHelper = htmlHelper;
            Generator = generator;
        }

        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.AspNetCore.Mvc.Rendering.ViewContext"/> for the current request.
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

        /// <summary>
        /// Model property Id
        /// </summary>
        public ModelExpression AspFor { get; set; }

        /// <summary>
        /// Model property Text
        /// </summary>
        public ModelExpression AspForText { get; set; }

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

            var urlHelper = _urlHelperFactory.GetUrlHelper(_context.ActionContext);;

            var action = urlHelper.Action(Action, Controller, RouteValues);

            (_htmlHelper as IViewContextAware)?.Contextualize(ViewContext);
            
            var id = _htmlHelper.GenerateIdFromName(AspFor.Name);

            output.TagName = "select";

            var currentValues = Generator.GetCurrentValues(
                ViewContext,
                AspFor.ModelExplorer,
                expression: AspFor.Name,
                allowMultiple: false);

            var currentText = AspForText?.Model as string;
            var currentId = AspForText?.Model as string;

            var items = Enumerable.Empty<SelectListItem>();

            if (!string.IsNullOrWhiteSpace(currentText))
                items = new[] {new SelectListItem(currentId, currentText)};

            var tagBuilder = Generator.GenerateSelect(
                ViewContext,
                AspFor.ModelExplorer,
                optionLabel: null,
                expression: AspFor.Name,
                selectList: items,
                currentValues: currentValues,
                allowMultiple: false,
                htmlAttributes: null);
            
            if (tagBuilder != null)
            {
                output.MergeAttributes(tagBuilder);
                output.PostContent.AppendHtml(tagBuilder.InnerHtml);
            }

            
            ViewContext.FormContext.FormData[AspFor.Name] = currentValues;

            var searchScript = @"
            <script>
            $(function(){
                $('#"+ id +@"').select2({
                    placeholder: 'Selecione',
                    theme: 'bootstrap4',
                    language: 'pt-BR',
                    width: '100%',
                    minimumInputLength: 0,
                    allowClear: true,
                    ajax: {
                        url: '"+ action +@"',
                        dataType: 'json',
                        delay: 250,
                        data: function (params) {
                            return {
                                pageSize: 20,
                                pageNum: params.page,
                                searchTerm: params.term
                            };
                        },
                        processResults: function (data, params) {
                            params.page = params.page || 1;
                            return {
                                results: data.items,
                                pagination: {
                                    more: (params.page * 20) < data.totalCount
                                }
                            };
                        },
                        cache: true
                    }
                });
            });
            </script>";
            
            output.PostContent.AppendHtmlLine(searchScript);
        }

    }
}
