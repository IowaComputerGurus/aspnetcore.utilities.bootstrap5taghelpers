﻿using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;

/// <summary>
///     Tag helper for the addition of a note to a form field
/// </summary>
public class FormNoteTagHelper : TagHelper
{
    /// <summary>
    ///     What type of tag should be rendered, by default it is a button
    /// </summary>
    public string TagName { get; set; } = "span";

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = TagName;
        output.AddClass("form-text", HtmlEncoder.Default);
    }
}