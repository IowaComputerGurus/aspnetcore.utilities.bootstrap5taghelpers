using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Card
{
    /// <summary>
    /// Tag Helper for the body of a Card
    /// </summary>
    [HtmlTargetElement("card-body", ParentTag = "card")]
    public class CardBodyTagHelper : TagHelper
    {
        /// <summary>
        /// Renders the card
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("card-body", HtmlEncoder.Default);

            var content = (await output.GetChildContentAsync()).GetContent();

            output.Content.AppendHtml(content);
        }
    }
}
