﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;

internal interface IFormElementMixin
{
    ViewContext ViewContext { get; }
    ModelExpression For { get; }

    /// <summary>
    /// The Html Generator used to render this tag helper
    /// </summary>
    IHtmlGenerator HtmlGenerator { get; }
}

internal static class FormElementMixinExtensions
{
    public static void StartFormGroup(this IFormElementMixin element, TagHelperOutput output, string cssClass)
    {
        if (string.IsNullOrEmpty(cssClass))
        {
            output.PreElement.AppendHtml("<div>");
        }
        else
        {
            output.PreElement.AppendHtml($"<div class='{cssClass}'>");
        }
    }

    public static void EndFormGroup(this IFormElementMixin element, TagHelperOutput output)
        => output.PostElement.AppendHtml("</div>");

    public static void AddLabel(this IFormElementMixin element, TagHelperOutput output)
    {
        //Find out if required to add special class
        var isRequired = element.For.ModelExplorer.Metadata.ValidatorMetadata.Any(o => o is RequiredAttribute);
        var targetClass = isRequired ? "form-label required" : "form-label";
        //Generate our label
        var label = element.HtmlGenerator.GenerateLabel(
            element.ViewContext,
            element.For.ModelExplorer,
            element.For.Name, null,
            new { @class = targetClass });
        output.PreElement.AppendHtml(label);
    }

    public static void AddValidationMessage(this IFormElementMixin element, TagHelperOutput output)
    {
        var validationMsg = element.HtmlGenerator.GenerateValidationMessage(
            element.ViewContext,
            element.For.ModelExplorer,
            element.For.Name,
            null,
            element.ViewContext.ValidationMessageElement,
            new { @class = "text-danger" });
        output.PostElement.AppendHtml(validationMsg);
    }
}