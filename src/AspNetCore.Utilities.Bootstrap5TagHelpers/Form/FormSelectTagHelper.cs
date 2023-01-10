using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;

/// <summary>
/// Custom implementation of the select tag helper
/// </summary>
public class FormSelectTagHelper : SelectTagHelper, IFormElementMixin
{
    /// <inheritdoc />
    public IHtmlGenerator HtmlGenerator { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="generator">Html Generator for field generation</param>
    public FormSelectTagHelper(IHtmlGenerator generator) : base(generator)
    {
        HtmlGenerator = generator;
    }

    /// <summary>
    /// Allows the addition of a note to the field
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    /// The class to be applied to the container
    /// </summary>
    public string ContainerClass { get; set; } = "mb-3";
    
    /// <summary>
    ///     What size of input should this be
    /// </summary>
    public BootstrapFormControlSize InputSize { get; set; } = BootstrapFormControlSize.Standard;

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
        output.TagName = "select";

        //Add the form-control class
        output.AddClass("form-control", HtmlEncoder.Default);

        if (InputSize != BootstrapFormControlSize.Standard)
        {
            output.AddClass($"form-control-{InputSize.ToString().ToLower()}", HtmlEncoder.Default);
        }

        //Add before div
        this.StartFormGroup(output, ContainerClass);

        //Generate our label
        this.AddLabel(output);

        //Now, add validation message AFTER the field
        this.AddValidationMessage(output);

        if (!string.IsNullOrEmpty(Note))
            output.PostElement.AppendHtml($"<span class=\"form-text\">{Note}</small>");

        this.EndFormGroup(output);
    }
}
