using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Encodings.Web;

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
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Get the context information
            if (context.Items[typeof(CardContext)] is not CardContext cardContext)
                throw new ArgumentException("CardContext is not specified in context parameter");

            //Setup basic tag information
            output.TagName = "div";
            output.AddClass("card-header", HtmlEncoder.Default);

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
        }
    }
}
