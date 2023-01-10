using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;

/// <summary>
///     TagHelper for rending Bootstrap form compliant input controls with support for ASP.NET Core model Binding.  Will
///     include Label, Field, and validation.
/// </summary>
[RestrictChildren("form-note")]
public class FormInputTagHelper : InputTagHelper, IFormElementMixin
{
    /// <inheritdoc />
    public IHtmlGenerator HtmlGenerator { get; }

    /// <summary>
    ///     What size of input should this be
    /// </summary>
    public BootstrapFormControlSize InputSize { get; set; } = BootstrapFormControlSize.Standard;

    /// <summary>
    /// The CSS class that should be applied to the containing div
    /// </summary>
    public string ContainerClass { get; set; } = "mb-3";

    /// <summary>
    /// Indicator if the input should be rendered as plain-text/readonly
    /// </summary>
    public bool PlainTextReadOnly { get; set; } = false;

    /// <summary>
    ///     Public constructor that will receive the incoming generator to leverage existing Microsoft Tag Helpers
    /// </summary>
    /// <param name="generator"></param>
    public FormInputTagHelper(IHtmlGenerator generator) : base(generator)
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
        if (PlainTextReadOnly)
        {
            output.AddClass("form-control-plaintext", HtmlEncoder.Default);
            output.Attributes.Add("readonly", "readonly");
        }
        else
        {
            output.AddClass("form-control", HtmlEncoder.Default);
        }

        if (InputSize != BootstrapFormControlSize.Standard)
        {
            output.AddClass($"form-control-{InputSize.ToString().ToLower()}", HtmlEncoder.Default);
        }

        //Add before div
        this.StartFormGroup(output, ContainerClass);

        //Generate our label if not inline
        this.AddLabel(output);

        //Now, add validation message AFTER the field if it is not plain text
        if(!PlainTextReadOnly)
            this.AddValidationMessage(output);

        //Close wrapping div
        this.EndFormGroup(output);
    }
}