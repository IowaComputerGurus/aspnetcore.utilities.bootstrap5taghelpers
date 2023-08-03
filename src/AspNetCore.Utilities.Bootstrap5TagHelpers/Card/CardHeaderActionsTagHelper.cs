using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Card;

/// <summary>
/// Helper for rendering actions within the header of a card
/// </summary>
[RestrictChildren("button", "a")]
public class CardHeaderActionsTagHelper : TagHelper
{
    /// <summary>
    /// Renders the control
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Get the context information
        var cardContext = context.Items[typeof(CardContext)] as CardContext;
        if (cardContext == null)
            throw new ArgumentException("CardContext is not specified in context parameter");

        return ProcessAsyncInternal(output);
    }

    /// <summary>
    /// Internal implementation
    /// </summary>
    /// <param name="output"></param>
    /// <returns></returns>
    private static async Task ProcessAsyncInternal(TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("d-flex", HtmlEncoder.Default);
        output.AddClass("flex-nowrap", HtmlEncoder.Default);
        output.AddClass("mt-2", HtmlEncoder.Default);
        output.AddClass("mt-sm-0", HtmlEncoder.Default);

        var content = (await output.GetChildContentAsync()).GetContent();

        output.Content.AppendHtml(content);
    }
}