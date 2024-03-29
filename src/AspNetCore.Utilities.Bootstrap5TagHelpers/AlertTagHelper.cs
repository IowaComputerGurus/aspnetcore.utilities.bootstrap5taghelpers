﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers;

/// <summary>
///     Tag helper for generating Bootstrap alerts
/// </summary>
public class AlertTagHelper : TagHelper
{
    /// <summary>
    ///     What style of alert should this be
    /// </summary>
    public BootstrapColor AlertColor { get; set; } = BootstrapColor.Info;

    /// <summary>
    ///     If set to true the element will not be shown
    /// </summary>
    public bool HideDisplay { get; set; }

    /// <summary>
    ///     Is this an alert that is dismissible
    /// </summary>
    public bool Dismissible { get; set; } = false;

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Stop render if hidden
        if (HideDisplay)
        {
            output.SuppressOutput();
            return;
        }

        //Add
        output.TagName = "div";
        output.AddClass("alert", HtmlEncoder.Default);
        output.AddClass($"alert-{AlertColor.ToString().ToLower()}", HtmlEncoder.Default);
        if (Dismissible)
        {
            output.AddClass("alert-dismissible", HtmlEncoder.Default);
            output.AddClass("fade", HtmlEncoder.Default);
            output.AddClass("show", HtmlEncoder.Default);
        }
        output.Attributes.Add("role", "alert");

        if (!Dismissible)
            return;

        var buttonBuilder = new TagBuilder("button");
        buttonBuilder.Attributes.Add("type", "button");
        buttonBuilder.AddCssClass("btn-close");
        buttonBuilder.Attributes.Add("data-bs-dismiss", "alert");
        buttonBuilder.Attributes.Add("aria-label", "Close");

        //Get existing content
        var existing = await output.GetChildContentAsync();
        output.Content.AppendHtml(existing.GetContent());
        output.Content.AppendHtml(buttonBuilder);
    }
}