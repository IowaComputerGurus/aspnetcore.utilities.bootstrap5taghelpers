using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Spinner;

/// <summary>
///     The options for the spinner mode
/// </summary>
public enum SpinnerMode
{
    /// <summary>
    ///     Will render with .spinner-border
    /// </summary>
    Border = 1,

    /// <summary>
    ///     Will render with .spinner-grow
    /// </summary>
    Grow = 2
}

/// <summary>
///     A tag helper for working with bootstrap spinners
/// </summary>
[HtmlTargetElement("bs-spinner")]
public class SpinnerTagHelper : TagHelper
{
    /// <summary>
    ///     The mode of the spinner
    /// </summary>
    public SpinnerMode SpinnerMode { get; set; } = SpinnerMode.Border;

    /// <summary>
    ///     The size of the spinner
    /// </summary>
    public bool IsSmall { get; set; } = false;

    /// <summary>
    ///     If set to true the element will render with an aria-hidden attribute with a value of true
    /// </summary>
    public bool AriaHidden { get; set; } = false;

    /// <summary>
    ///     If set will render the spinner with a color
    /// </summary>
    [HtmlAttributeName("bs-color")]
    public BootstrapColor? Color { get; set; }

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Add
        output.TagName = "div";
        var modeClass = SpinnerMode == SpinnerMode.Border ? "spinner-border" : "spinner-grow";
        output.AddClass(modeClass, HtmlEncoder.Default);
        if (IsSmall)
        {
            output.AddClass($"{modeClass}-sm", HtmlEncoder.Default);
        }

        if (Color.HasValue)
        {
            output.AddClass($"text-{Color.Value.ToString().ToLowerInvariant()}", HtmlEncoder.Default);
        }

        if (AriaHidden)
        {
            output.Attributes.Add("aria-hidden", "true");
        }
    }
}