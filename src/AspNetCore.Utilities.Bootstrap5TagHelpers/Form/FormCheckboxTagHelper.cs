using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualBasic;
using System;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;

/// <summary>
///     TagHelper for rending Bootstrap form compliant checkbox controls with support for ASP.NET Core model Binding.  Will
///     include Label, Field, and validation.
/// </summary>
[RestrictChildren("form-checkbox")]
public class FormCheckboxTagHelper : InputTagHelper, IFormElementMixin
{
    /// <inheritdoc />
    public IHtmlGenerator HtmlGenerator { get; }

    /// <summary>
    /// The CSS class that should be applied to the containing div, in addition to that of the form-check that is required
    /// </summary>
    public string ContainerClass { get; set; } = "mb-3";

    /// <summary>
    /// Indicator if the input should be rendered as disabled
    /// </summary>
    public bool Disabled { get; set; } = false;

    /// <summary>
    /// Controls if this should be rendered as a switch
    /// </summary>
    public bool IsSwitch { get; set; } = false;


    /// <summary>
    ///     Public constructor that will receive the incoming generator to leverage existing Microsoft Tag Helpers
    /// </summary>
    /// <param name="generator"></param>
    public FormCheckboxTagHelper(IHtmlGenerator generator) : base(generator)
    {
        HtmlGenerator = generator;
    }

    /// <summary>
    ///     Used to actually process the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Call our base implementation
        base.Process(context, output);
        
        //Set our tag name
        output.TagName = "input";

        //Add the form-control class
        if (Disabled)
        {
            output.Attributes.Add("disabled", "");
        }
        
        output.AddClass("form-check-input", HtmlEncoder.Default);

        //Add before div
        var groupClass = $"form-check {ContainerClass}";
        if (IsSwitch)
        {
            groupClass += " form-switch";
            output.Attributes.Add("role", "switch");
        }
        this.StartFormGroup(output, groupClass);
        
        //Generate our label if not inline
        this.AddLabel(output, "form-check-label");
        
        //Now, add validation message AFTER the field if it is not disabled
        if (!Disabled)
        {
            this.AddValidationMessage(output);
        }

        //Close wrapping div
        this.EndFormGroup(output);
    }
}