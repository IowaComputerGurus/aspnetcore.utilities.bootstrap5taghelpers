using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.OffCanvas;

/// <summary>
///     A tag helper that adds a dismiss button to the header of a Modal
/// </summary>
[HtmlTargetElement("offcanvas-dismiss")]
public class OffCanvasDismissTagHelper : TagHelper
{
    /// <summary>
    ///     Renders the needed HTML for the dismiss button
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("data-bs-dismiss", "offcanvas");
        output.AddClass("btn-close", HtmlEncoder.Default);
        output.Attributes.Add("type", "button");
        output.Attributes.Add("aria-label", "Close");
    }
}
