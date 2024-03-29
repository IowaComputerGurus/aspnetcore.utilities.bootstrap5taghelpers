﻿using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Drawing;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Modal;

public class ModalTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Not_Error_With_Missing_Id_Attribute()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalTagHelper();
        Exception exception = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task Should_Add_Context_Object_With_Provided_Id()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        var providedId = "testingModal";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("id", providedId) });
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        var generatedContext = Assert.IsType<ModalContext>(context.Items[typeof(ModalContext)]);
        Assert.Equal(providedId, generatedContext.Id);
    }

    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("modal fade", output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} modal fade";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("class", customClass) });
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Theory]
    [InlineData("", false, "")]
    [InlineData("testing", true, "testingLabel")]
    public async Task Should_Render_With_Aria_LabelledBy_When_Id_Provided(string providedId, bool expectedAttribute,
        string expectedAttributeValue)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("id", providedId) });
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        if (expectedAttribute)
        {
            Assert.Equal(expectedAttributeValue, output.Attributes["aria-labelledby"].Value.ToString());
        }
        else
        {
            Assert.Null(output.Attributes["aria-labelledby"]);
        }
    }

    [Theory]
    [InlineData(true, "static")]
    [InlineData(false, "")]
    public async Task Should_Render_Static_Backdrop_Attribute_When_Flagged(bool staticBackdrop,
        string expectedAttributeValue)
    {
        //Arrange
        var attributeName = "data-bs-backdrop";
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalTagHelper { StaticBackdrop = staticBackdrop };
        await helper.ProcessAsync(context, output);

        //Assert
        if (staticBackdrop)
        {
            Assert.Equal(expectedAttributeValue, output.Attributes[attributeName].Value.ToString());
        }
        else
        {
            Assert.Null(output.Attributes[attributeName]);
        }
    }

    
    [Fact]
    public async Task Should_Render_Proper_InnerHtmlStructure_WithNoChildContent()
    {
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");
        var expectedContent = "<div class=\"modal-dialog\"><div class=\"modal-content\"></div></div>";

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedContent, output.Content.GetContent());
    }

    [Fact]
    public async Task Should_Render_Centered_Class_When_Flagged()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");
        var expectedContent = "<div class=\"modal-dialog modal-dialog-centered\"><div class=\"modal-content\"></div></div>";

        //Act
        var helper = new ModalTagHelper { VerticallyCentered = true };
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedContent, output.Content.GetContent());
    }

    [Fact]
    public async Task Should_Render_Scrollable_Class_When_Flagged()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");
        var expectedContent = "<div class=\"modal-dialog modal-dialog-scrollable\"><div class=\"modal-content\"></div></div>";

        //Act
        var helper = new ModalTagHelper { Scrollable = true };
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedContent, output.Content.GetContent());
    }

    [Theory]
    [InlineData(ModalSize.Small, "modal-dialog modal-sm")]
    [InlineData(ModalSize.Default, "modal-dialog")]
    [InlineData(ModalSize.Large, "modal-dialog modal-lg")]
    [InlineData(ModalSize.ExtraLarge, "modal-dialog modal-xl")]
    public async Task Should_Render_ProperModalDialogClass_ForSize(ModalSize size, string expectedClass)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");
        var expectedContent = $"<div class=\"{expectedClass}\"><div class=\"modal-content\"></div></div>";

        //Act
        var helper = new ModalTagHelper { Size = size};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedContent, output.Content.GetContent());
    }

    [Theory]
    [InlineData(ModalFullscreenMode.Never, "modal-dialog")]
    [InlineData(ModalFullscreenMode.Always, "modal-dialog modal-fullscreen")]
    [InlineData(ModalFullscreenMode.BelowSmall, "modal-dialog modal-fullscreen-sm-down")]
    [InlineData(ModalFullscreenMode.BelowMedium, "modal-dialog modal-fullscreen-md-down")]
    [InlineData(ModalFullscreenMode.BelowLarge, "modal-dialog modal-fullscreen-lg-down")]
    [InlineData(ModalFullscreenMode.BelowXLarge, "modal-dialog modal-fullscreen-xl-down")]
    [InlineData(ModalFullscreenMode.BelowXXLarge, "modal-dialog modal-fullscreen-xxl-down")]
    public async Task Should_Render_ProperModalDialogClass_ForFullscreen(ModalFullscreenMode mode, string expectedClass)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");
        var expectedContent = $"<div class=\"{expectedClass}\"><div class=\"modal-content\"></div></div>";

        //Act
        var helper = new ModalTagHelper { FullscreenMode = mode};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedContent, output.Content.GetContent());
    }
}