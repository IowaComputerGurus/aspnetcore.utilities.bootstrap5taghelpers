using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;

/// <summary>
///     A high-level wrapper Tag Helper for rendering a bootstrap Modal
/// </summary>
[RestrictChildren("modal-body", "modal-header", "modal-footer")]
public class ModalTagHelper : TagHelper
{
    /// <summary>
    ///     If set to true the background will not be clickable to dismiss the dialog
    /// </summary>
    public bool StaticBackdrop { get; set; } = false;

    /// <summary>
    /// If set to true the modal will have the added class of modal-dialog-centered
    /// </summary>
    public bool VerticallyCentered { get; set; } = false;

    /// <summary>
    /// If set to true the modal will have the added class of modal-dialog-scrollable
    /// </summary>
    public bool Scrollable { get; set; } = false;

    public override void Init(TagHelperContext context)
    {
        //Reset
        if(context.Items.ContainsKey(typeof(ModalContext)))
            context.Items.Remove(typeof(ModalContext));
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
            id = output.Attributes["id"].Value.ToString();

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
        
        if(!string.IsNullOrEmpty(id))
            output.Attributes.Add("aria-labelledby", $"{id}Label");
        output.Attributes.Add("aria-hidden", "true");
        if (StaticBackdrop)
            output.Attributes.Add("data-bs-backdrop", "static");
        output.Attributes.Add("tabindex", "-1");
        var dialogWrapper = new TagBuilder("div");
        dialogWrapper.AddCssClass("modal-dialog");
        if (VerticallyCentered)
            dialogWrapper.AddCssClass("modal-dialog-centered");
        if (Scrollable)
            dialogWrapper.AddCssClass("modal-dialog-scrollable");
        var dialogContent = new TagBuilder("div");
        dialogContent.AddCssClass("modal-content");
        dialogContent.InnerHtml.AppendHtml(body);
        dialogWrapper.InnerHtml.AppendHtml(dialogContent);

        output.Content.AppendHtml(dialogWrapper);
    }
}