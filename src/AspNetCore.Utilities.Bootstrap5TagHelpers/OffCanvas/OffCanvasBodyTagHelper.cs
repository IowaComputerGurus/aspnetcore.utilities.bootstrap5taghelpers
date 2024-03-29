﻿using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.OffCanvas;

/// <summary>
/// Tag helper for rendering the 
/// </summary>
[HtmlTargetElement("offcanvas-body", ParentTag = "offcanvas")]
public class OffCanvasBodyTagHelper : TagHelper
{
    /// <summary>
    ///     Renders the body element with the wrapping div and class
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("offcanvas-body", HtmlEncoder.Default);
        var body = (await output.GetChildContentAsync()).GetContent();
        body = body.Trim();
        output.Content.AppendHtml(body);
    }
}
