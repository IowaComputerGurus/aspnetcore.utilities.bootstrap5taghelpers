using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.OffCanvas;

/// <summary>
///     Renders a modal header content
/// </summary>
[HtmlTargetElement("offcanvas-header", ParentTag = "offcanvas")]
[RestrictChildren("offcanvas-dismiss")]
public class OffCanvasHeaderTagHelper : TagHelper
{
    /// <summary>
    ///     The optional title to render for this particular title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The title tag to be utilized
    /// </summary>
    public string TitleTag { get; set; } = "h5";

    /// <summary>
    ///     Completes the actual rendering of the Tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Get the context information
        var offCanvasContext = context.Items[typeof(OffCanvasContext)] as OffCanvasContext;
        if (offCanvasContext == null)
            throw new ArgumentException("OffCanvasContext not present");

        return ProcessAsyncInternal(output, offCanvasContext);
    }

    private async Task ProcessAsyncInternal(TagHelperOutput output, OffCanvasContext context)
    {
        //Setup basic tag information
        output.TagName = "div";
        output.AddClass("offcanvas-header", HtmlEncoder.Default);

        //Add the title
        if (!string.IsNullOrEmpty(Title))
        {
            var titleTag = new TagBuilder(TitleTag);
            titleTag.Attributes.Add("class", "offcanvas-title");
            if (!string.IsNullOrEmpty(context.Id))
                titleTag.Attributes.Add("id", $"{context.Id}Label");
            titleTag.InnerHtml.Append(Title);
            output.Content.AppendHtml(titleTag);
        }

        //Append other items, such as the dismiss button etc
        var body = (await output.GetChildContentAsync()).GetContent();
        body = body.Trim();
        output.Content.AppendHtml(body);
    }
}