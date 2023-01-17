using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;

/// <summary>
///     A collection of options for modal dialog sizing
/// </summary>
public enum ModalSize
{
    /// <summary>
    ///     Will render with .modal-sm
    /// </summary>
    Small = 0,

    /// <summary>
    ///     Will render without any additional class
    /// </summary>
    Default = 1,

    /// <summary>
    ///     Will render with .modal-lg
    /// </summary>
    Large = 2,

    /// <summary>
    ///     Will render with .modal-xl
    /// </summary>
    ExtraLarge = 3
}

/// <summary>
/// A collection of options for setting the full-screen mode of a modal
/// </summary>
public enum ModalFullscreenMode
{
    /// <summary>
    /// The default behavior, it will NEVER be full screen
    /// </summary>
    Never = 0,
    /// <summary>
    /// The dialog will ALWAYS be full scree
    /// </summary>
    Always = 1,
    /// <summary>
    /// The dialog will only be full screen below the small breakpoint
    /// </summary>
    BelowSmall = 2,
    /// <summary>
    /// The dialog will only be full screen below the medium breakpoint
    /// </summary>
    BelowMedium = 3,
    /// <summary>
    /// The dialog will only be full screen below the large breakpoint
    /// </summary>
    BelowLarge = 4,
    /// <summary>
    /// The dialog will only be full screen below the extra large breakpoint
    /// </summary>
    BelowXLarge = 5,
    /// <summary>
    /// The dialog will only be full screen below the extra extra large breakpoint
    /// </summary>
    BelowXXLarge = 6
}

/// <summary>
///     Extension methods for helping with conversion of enum to class
/// </summary>
public static class ModalEnumExtensions
{
    /// <summary>
    ///     Converts to the proper CSS class
    /// </summary>
    /// <param name="modalSize">The targeted size of the modal</param>
    /// <returns></returns>
    public static string ToClass(this ModalSize modalSize)
    {
        switch (modalSize)
        {
            case ModalSize.Small:
                return "modal-sm";
            case ModalSize.Large:
                return "modal-lg";
            case ModalSize.ExtraLarge:
                return "modal-xl";
            default:
                return string.Empty;
        }
    }

    /// <summary>
    ///     Converts to proper css class
    /// </summary>
    /// <param name="mode">The targeted mode for full screen dispay</param>
    /// <returns></returns>
    public static string ToClass(this ModalFullscreenMode mode)
    {
        switch (mode)
        {
            case ModalFullscreenMode.Always:
                return "modal-fullscreen";
            case ModalFullscreenMode.BelowSmall:
                return "modal-fullscreen-sm-down";
            case ModalFullscreenMode.BelowMedium:
                return "modal-fullscreen-md-down";
            case ModalFullscreenMode.BelowLarge:
                return "modal-fullscreen-lg-down";
            case ModalFullscreenMode.BelowXLarge:
                return "modal-fullscreen-xl-down";
            case ModalFullscreenMode.BelowXXLarge:
                return "modal-fullscreen-xxl-down";
            default:
                return string.Empty;
        }
    }
}


/// <summary>
///     A high-level wrapper Tag Helper for rendering a bootstrap Modal
/// </summary>
[RestrictChildren("modal-body", "modal-header", "modal-footer")]
public class ModalTagHelper : TagHelper
{
    /// <summary>
    ///     Determines the optional size of the modal dialog
    /// </summary>
    public ModalSize Size { get; set; } = ModalSize.Default;

    /// <summary>
    ///     Determines the optional full screen mode of the dialog. 
    /// </summary>
    public ModalFullscreenMode FullscreenMode { get; set; } = ModalFullscreenMode.Never;

    /// <summary>
    ///     If set to true the background will not be clickable to dismiss the dialog
    /// </summary>
    public bool StaticBackdrop { get; set; } = false;

    /// <summary>
    ///     If set to true the modal will have the added class of modal-dialog-centered
    /// </summary>
    public bool VerticallyCentered { get; set; } = false;

    /// <summary>
    ///     If set to true the modal will have the added class of modal-dialog-scrollable
    /// </summary>
    public bool Scrollable { get; set; } = false;

    /// <summary>
    ///     Ensure that if we have a context item that we reset.  This is needed when you have multiple tag helpers on the same
    ///     page if the information is not shared
    /// </summary>
    /// <param name="context"></param>
    [ExcludeFromCodeCoverage]
    public override void Init(TagHelperContext context)
    {
        //Reset
        if (context.Items.ContainsKey(typeof(ModalContext)))
        {
            context.Items.Remove(typeof(ModalContext));
        }
    }

    /// <summary>
    ///     Renders the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Obtain the id value to add to the context
        var id = "";
        if (output.Attributes.ContainsName("id"))
        {
            id = output.Attributes["id"].Value.ToString();
        }

        //Add the id to the context
        var modalContext = new ModalContext { Id = id };
        context.Items[typeof(ModalContext)] = modalContext;

        //Get our child content before we mess with anything
        var body = (await output.GetChildContentAsync()).GetContent();
        body = body.Trim();

        output.TagName = "div";

        //Add classes to the existing tag, merging with custom ones added
        output.AddClass("modal", HtmlEncoder.Default);
        output.AddClass("fade", HtmlEncoder.Default);

        if (!string.IsNullOrEmpty(id))
        {
            output.Attributes.Add("aria-labelledby", $"{id}Label");
        }

        output.Attributes.Add("aria-hidden", "true");
        if (StaticBackdrop)
        {
            output.Attributes.Add("data-bs-backdrop", "static");
        }

        output.Attributes.Add("tabindex", "-1");
        var dialogWrapper = new TagBuilder("div");
        dialogWrapper.AddCssClass("modal-dialog");
        if (VerticallyCentered)
        {
            dialogWrapper.AddCssClass("modal-dialog-centered");
        }

        if (Scrollable)
        {
            dialogWrapper.AddCssClass("modal-dialog-scrollable");
        }

        var sizeClass = Size.ToClass();
        if (!string.IsNullOrEmpty(sizeClass))
        {
            dialogWrapper.AddCssClass(sizeClass);
        }

        var fullscreenClass = FullscreenMode.ToClass();
        if (!string.IsNullOrEmpty(fullscreenClass))
        {
            dialogWrapper.AddCssClass(fullscreenClass);
        }
        var dialogContent = new TagBuilder("div");
        dialogContent.AddCssClass("modal-content");
        dialogContent.InnerHtml.AppendHtml(body);
        dialogWrapper.InnerHtml.AppendHtml(dialogContent);

        output.Content.AppendHtml(dialogWrapper);
    }
}