using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DrAgenda.Web.Helpers.TagHelpers
{
    [HtmlTargetElement(Attributes = InRoleAttributeName)]
    public class InRoleTagHelper : TagHelper
    {
        private const string InRoleAttributeName = "asp-in-role";

        private HttpContext _context;

        public InRoleTagHelper(IActionContextAccessor context)
        {
            _context = context.ActionContext.HttpContext;
        }

        [HtmlAttributeName(InRoleAttributeName)]
        public string Roles { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var appUserState = new AppUserState(_context);

            if (!(appUserState.IsInRole("Admin")
                || appUserState
                    .IsInRole(Roles
                                .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Select(x => x.Trim().ToLower()).ToArray())
                ))
            {
                output.SuppressOutput();
            }
        }
    }
}
