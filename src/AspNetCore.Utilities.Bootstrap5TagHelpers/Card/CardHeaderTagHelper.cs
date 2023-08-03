using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Card
{
    /// <summary>
    /// Tag Helper for rendering the header for a card
    /// </summary>
    [HtmlTargetElement("card-header", ParentTag = "card")]
    public class CardHeaderTagHelper : TagHelper
    {
        /// <summary>
        /// The title of the header
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Renders the header for a bootstrap card
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Get the context information
            if (context.Items[typeof(CardContext)] is not CardContext cardContext)
                throw new ArgumentException("CardContext is not specified in context parameter");

            return ProcessAsyncInternal(output, cardContext);
        }

        private async Task ProcessAsyncInternal(TagHelperOutput output, CardContext cardContext)
        {
            //Setup basic tag information
            output.TagName = "div";
            output.AddClass("card-header", HtmlEncoder.Default);

            //Get sub controls if we need them
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();

            if (!string.IsNullOrWhiteSpace(body))
            {
                output.AddClass("d-flex", HtmlEncoder.Default);
                output.AddClass("flex-column", HtmlEncoder.Default);
                output.AddClass("flex-sm-row", HtmlEncoder.Default);
                output.AddClass("align-items-sm-center", HtmlEncoder.Default);
                output.AddClass("justify-content-between", HtmlEncoder.Default);
            }

            //If we have an id make a custom span
            if (!string.IsNullOrEmpty(cardContext.Id))
            {
                var wrapper = new TagBuilder("span");
                wrapper.Attributes.Add("id", $"{cardContext.Id}Label");
                wrapper.InnerHtml.Append(Title);
                output.Content.AppendHtml(wrapper);
            }
            else
            {
                output.Content.AppendHtml(Title);
            }


            //Add sub-content after our title
            if (!string.IsNullOrEmpty(body))
                output.Content.AppendHtml(body);
            
        }
    }
}
