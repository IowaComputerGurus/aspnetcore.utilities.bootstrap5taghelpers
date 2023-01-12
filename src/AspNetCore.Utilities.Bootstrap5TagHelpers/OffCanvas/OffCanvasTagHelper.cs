using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.OffCanvas;

/// <summary>
/// Controls the placement of the off canvas element
/// </summary>
public enum OffCanvasPlacement
{
    /// <summary>
    /// Places it to the left o the viewport
    /// </summary>
    Start = 1,
    /// <summary>
    /// Places it to the right of the viewport
    /// </summary>
    End = 2,
    /// <summary>
    /// Places it to the top of the viewport
    /// </summary>
    Top = 3,
    /// <summary>
    /// Places it at the bottom
    /// </summary>
    Bottom = 4
}

/// <summary>
///     A high-level wrapper Tag Helper for rendering a bootstrap Modal
/// </summary>
[RestrictChildren("offcanvas-body", "offcanvas-header")]
[HtmlTargetElement("offcanvas")]
public class OffCanvasTagHelper : TagHelper
{
    /// <summary>
    /// Where should the item display
    /// </summary>
    public OffCanvasPlacement Placement { get; set; } = OffCanvasPlacement.Start;

    /// <summary>
    /// Should it be rendered as a static backdrop
    /// </summary>
    public bool StaticBackdrop { get; set; }

    /// <summary>
    /// If set to true the body of the page will be scrollable
    /// </summary>
    public bool EnableBodyScrolling { get; set; }

    /// <summary>
    ///     Ensure that if we have a context item that we reset.  This is needed when you have multiple tag helpers on the same
    ///     page if the information is not shared
    /// </summary>
    /// <param name="context"></param>
    [ExcludeFromCodeCoverage]
    public override void Init(TagHelperContext context)
    {
        //Reset
        if (context.Items.ContainsKey(typeof(OffCanvasContext)))
        {
            context.Items.Remove(typeof(OffCanvasContext));
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
        var canvasContext = new OffCanvasContext { Id = id };
        context.Items[typeof(OffCanvasContext)] = canvasContext;

        //Get our child content before we mess with anything
        var body = (await output.GetChildContentAsync()).GetContent();
        body = body.Trim();

        output.TagName = "div";

        //Add classes to the existing tag, merging with custom ones added
        output.AddClass("offcanvas", HtmlEncoder.Default);
        output.AddClass($"offcanvas-{Placement.ToString().ToLower()}", HtmlEncoder.Default);
        output.Attributes.Add("tabindex", "-1");

        if (StaticBackdrop)
        {
            output.Attributes.Add("data-bs-backdrop", "static");
        }

        if (EnableBodyScrolling)
        {
            output.Attributes.Add("data-bs-scroll", "true");
        }

        if (!string.IsNullOrEmpty(id))
        {
            output.Attributes.Add("aria-labelledby", $"{id}Label");
        }

        output.Content.AppendHtml(body);
    }
}