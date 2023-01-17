using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers;

/// <summary>
///     A tag helper for working with progress bars
/// </summary>
public class ProgressTagHelper : TagHelper
{
    /// <summary>
    ///     What color should be used
    /// </summary>
    public BootstrapColor? BackgroundColor { get; set; }

    /// <summary>
    /// The label for those with non-visual support
    /// </summary>
    public string AriaLabel { get; set; }
    /// <summary>
    ///     An optional display label value for the current value
    /// </summary>
    public string ProgressDisplayLabel { get; set; }

    /// <summary>
    /// If set to true will display as striped
    /// </summary>
    public bool IsStriped { get; set; }

    /// <summary>
    /// If set to true will display as animated
    /// </summary>
    public bool IsAnimated { get; set; }
    
    /// <summary>
    ///     The progress value which must be between min/max
    /// </summary>
    public int ProgressValue { get; set; } = 0;

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Validate progress value
        if (ProgressValue < 0 || ProgressValue > 100)
            throw new ArgumentOutOfRangeException("ProgressValue", "The progress value must be within the range");

        //Add
        output.TagName = "div";
        output.AddClass("progress", HtmlEncoder.Default);
        output.Attributes.Add("role", "progressbar");
        if (!string.IsNullOrEmpty(AriaLabel))
            output.Attributes.Add("aria-label", AriaLabel);
        output.Attributes.Add("aria-valuenow", ProgressValue.ToString());
        output.Attributes.Add("aria-valuemin", "0");
        output.Attributes.Add("aria-valuemax", "100");

        //Build the internal tag
        var barTag = new TagBuilder("div");
        barTag.AddCssClass("progress-bar");
        barTag.Attributes.Add("style", $"width: {ProgressValue}%");
        if (!string.IsNullOrEmpty(ProgressDisplayLabel))
            barTag.InnerHtml.Append(ProgressDisplayLabel);
        if(BackgroundColor.HasValue)
            barTag.AddCssClass($"bg-{BackgroundColor.Value.ToString().ToLower()}");
        if (IsStriped)
            barTag.AddCssClass("progress-bar-striped");
        if (IsAnimated)
            barTag.AddCssClass("progress-bar-animated");

        //Add to the tag
        output.Content.AppendHtml(barTag);
    }
}