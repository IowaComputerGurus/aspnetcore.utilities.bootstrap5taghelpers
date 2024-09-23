using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Accordion;

/// <summary>
/// Tag helper for individual items
/// </summary>
[HtmlTargetElement("accordion-header", ParentTag = "accordion-item")]
public class AccordionHeaderTagHelper : TagHelper
{
    /// <summary>
    /// Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Get our context
        var accordionContext = (AccordionContext)context.Items[typeof(AccordionContext)];
        var itemContext = (AccordionItemContext)context.Items[typeof(AccordionItemContext)];

        //Set as a Div
        output.TagName = "h2";
        
        //Add default class
        output.AddClass("accordion-header", HtmlEncoder.Default);
        
        //Build wrapping button
        var wrappingButton = new TagBuilder("button");
        wrappingButton.AddCssClass("accordion-button");
        wrappingButton.Attributes.Add("type", "button");
        wrappingButton.Attributes.Add("data-bs-toggle", "collapse");
        wrappingButton.Attributes.Add("data-bs-target", $"#{itemContext.ItemId}");
        wrappingButton.Attributes.Add("aria-expanded", itemContext.Expanded.ToString().ToLower());
        wrappingButton.Attributes.Add("aria-controls", itemContext.ItemId);

        //Special behaviors for if it is not open
        if (!itemContext.Expanded)
        {
            wrappingButton.AddCssClass("collapsed");
        }
        
        //Get the child content
        var content = (await output.GetChildContentAsync()).GetContent();
        wrappingButton.InnerHtml.AppendHtml(content);

        //Add to the output
        output.Content.AppendHtml(wrappingButton);
    }
}