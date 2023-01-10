using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers;

/// <summary>
///     A tag helper for working with bootstrap Badges
/// </summary>
public class BadgeTagHelper : TagHelper
{
    /// <summary>
    ///     What style of badge should this be
    /// </summary>
    public BootstrapColor BadgeColor { get; set; }

    /// <summary>
    ///     If set to true the element will not be shown
    /// </summary>
    public bool HideDisplay { get; set; }

    /// <summary>
    ///     If set to true will add the class rounded-pill to the badge
    /// </summary>
    public bool DisplayAsPill { get; set; }

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Stop render if hidden
        if (HideDisplay)
        {
            output.SuppressOutput();
            return;
        }

        //Add
        output.TagName = "span";
        output.AddClass("badge", HtmlEncoder.Default);
        output.AddClass($"text-bg-{BadgeColor.ToString().ToLower()}", HtmlEncoder.Default);
        if(DisplayAsPill)
            output.AddClass("rounded-pill", HtmlEncoder.Default);
    }
}