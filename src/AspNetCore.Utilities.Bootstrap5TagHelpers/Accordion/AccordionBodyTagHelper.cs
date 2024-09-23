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
[HtmlTargetElement("accordion-body", ParentTag = "accordion-item")]
public class AccordionBodyTagHelper : TagHelper
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
        output.TagName = "div";

        //Add an id attribute
        output.Attributes.Add("id", itemContext.ItemId);

        //Setup default items that are always there
        output.AddClass("accordion-collapse", HtmlEncoder.Default);
        output.AddClass("collapse", HtmlEncoder.Default);
        
        //If not always open add the parent id
        if(!accordionContext.AlwaysOpen)
            output.Attributes.Add("data-bs-parent", $"#{accordionContext.Id}");

        if (itemContext.Expanded)
            output.AddClass("show", HtmlEncoder.Default);
        
        //Create a wrapping div
        var wrappingDiv = new TagBuilder("div");
        wrappingDiv.AddCssClass("accordion-body");
        
        //Get the child content
        var content = (await output.GetChildContentAsync()).GetContent();
        wrappingDiv.InnerHtml.AppendHtml(content);

        //Add to the output
        output.Content.AppendHtml(wrappingDiv);
    }
}