using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Accordion;

/// <summary>
/// A tag helper for rendering a bootstrap card to a view
/// </summary>
[RestrictChildren("accordion-item")]
public class AccordionTagHelper : TagHelper
{
    /// <summary>
    /// Should this render as a Flush accordion
    /// </summary>
    public bool IsFlush { get; set; } = false;

    /// <summary>
    /// If set to true child items can be opened/closed at any time
    /// </summary>
    public bool AlwaysOpen { get; set; } = false;

    /// <summary>
    /// Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var id = output.Attributes["id"]?.Value?.ToString();

        output.TagName = "div";

        output.AddClass("accordion", HtmlEncoder.Default);

        if (IsFlush)
            output.AddClass("accordion-flush", HtmlEncoder.Default);

        // setup content
        var accordionContext = new AccordionContext() {Id = id, AlwaysOpen = AlwaysOpen};
        context.Items[typeof(AccordionContext)] = accordionContext;

        var content = (await output.GetChildContentAsync()).GetContent();

        output.Content.AppendHtml(content);
    }
}