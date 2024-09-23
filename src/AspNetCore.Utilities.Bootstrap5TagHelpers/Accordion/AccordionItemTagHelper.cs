using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Accordion;

/// <summary>
/// Tag helper for individual items
/// </summary>
[HtmlTargetElement("accordion-item", ParentTag = "accordion")]
public class AccordionItemTagHelper : TagHelper
{
    /// <summary>
    /// Should this be rendered as expanded
    /// </summary>
    public bool Expanded { get; set; }

    /// <summary>
    /// Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var id = output.Attributes["id"]?.Value?.ToString();

        if (id != null)
        {
            //Remove the id from the wrapping div
            output.Attributes.Remove(output.Attributes["id"]);
        }
        else
        {
            id = Guid.NewGuid().ToString();
        }

        //Set as a Div
        output.TagName = "div";


        //Add wrapping class
        output.AddClass("accordion-item", HtmlEncoder.Default);


        // setup content
        var itemContext = new AccordionItemContext() { ItemId = id, Expanded = Expanded };
        context.Items[typeof(AccordionItemContext)] = itemContext;

        var content = (await output.GetChildContentAsync()).GetContent();

        output.Content.AppendHtml(content);
    }
}
