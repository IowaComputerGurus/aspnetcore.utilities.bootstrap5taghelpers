using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers;
#nullable enable

/// <summary>
///     Types of button
/// </summary>
public enum ButtonType
{
    /// <summary>
    ///     A regular button
    /// </summary>
    Button,

    /// <summary>
    ///     A form submit button
    /// </summary>
    Submit,

    /// <summary>
    ///     A form reset button
    /// </summary>
    Reset
}

/// <summary>
///     Sizes of button
/// </summary>
public enum ButtonSize
{
    /// <summary>
    ///     Normal size
    /// </summary>
    Normal,

    /// <summary>
    ///     A large button
    /// </summary>
    Large,

    /// <summary>
    ///     A small button
    /// </summary>
    Small
}

/// <summary>
///     A tag helper for creating a Bootstrap button.
/// </summary>
[HtmlTargetElement("bs-button")]
public class ButtonTagHelper : TagHelper
{
    /// <summary>
    ///     What style of button to create.
    /// </summary>
    [HtmlAttributeName("bs-color")]
    public BootstrapColor Color { get; set; } = BootstrapColor.Info;

    /// <summary>
    ///     The type of button this will be
    /// </summary>
    public ButtonType Type { get; set; } = ButtonType.Button;

    /// <summary>
    ///     If set to true the element will not be shown
    /// </summary>
    public bool HideDisplay { get; set; }

    /// <summary>
    ///     If selected will render as an outlined button
    /// </summary>
    public bool IsOutline { get; set; }

    /// <summary>
    ///     The value of this button
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    ///     Is this button disabled
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    ///     Is this a block level button
    /// </summary>
    public bool Block { get; set; }

    /// <summary>
    ///     The size of this button
    /// </summary>
    public ButtonSize Size { get; set; } = ButtonSize.Normal;

    /// <inheritdoc />
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (HideDisplay)
        {
            output.SuppressOutput();
            return;
        }

        output.TagName = "button";
        output.Attributes.Add("type", Type.ToString().ToLowerInvariant());
        output.AddClass("btn", HtmlEncoder.Default);
        if(IsOutline)
            output.AddClass($"btn-outline-{Color.ToString().ToLowerInvariant()}", HtmlEncoder.Default);
        else
            output.AddClass($"btn-{Color.ToString().ToLowerInvariant()}", HtmlEncoder.Default);
        output.Attributes.Add("role", "button");

        if (!string.IsNullOrEmpty(Value))
        {
            output.Attributes.Add("value", Value);
        }

        if (Disabled)
        {
            output.Attributes.Add(new TagHelperAttribute("disabled"));
        }

        if (Size != ButtonSize.Normal)
        {
            output.AddClass(Size switch
            {
                ButtonSize.Large => "btn-lg",
                ButtonSize.Small => "btn-sm",
                _ => throw new ArgumentOutOfRangeException(nameof(Size))
            }, HtmlEncoder.Default);
        }

        if (Block)
        {
            output.AddClass("btn-block", HtmlEncoder.Default);
        }

        TagHelperContent? content = await output.GetChildContentAsync();
        if (content.IsEmptyOrWhiteSpace)
        {
            output.TagMode = TagMode.SelfClosing;
        }
        else
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content = content;
        }
    }
}