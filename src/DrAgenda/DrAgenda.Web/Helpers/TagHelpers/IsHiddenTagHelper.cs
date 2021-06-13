using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DrAgenda.Web.Helpers.TagHelpers
{
    [HtmlTargetElement("*", Attributes = "is-hidden")]
    public class IsHiddenTagHelper : TagHelper
    {
        public bool IsHidden { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsHidden)
                output.Attributes.Add("style", "display: none");

            base.Process(context, output);
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (IsHidden)
                output.Attributes.Add("style", "display: none");

            return base.ProcessAsync(context, output);
        }
    }
}
