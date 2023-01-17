using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.OffCanvas;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.OffCanvas;
public class OffCanvasHeaderTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_ThrowException_WhenMissingContext()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasHeaderTagHelper();
        Exception exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<KeyNotFoundException>(exceptionResult);
    }

    [Fact]
    public async Task Should_ThrowException_WhenContextIsNull()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(OffCanvasContext), null);
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasHeaderTagHelper();
        Exception exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<ArgumentException>(exceptionResult);
    }

    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(OffCanvasContext), new OffCanvasContext());
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(OffCanvasContext), new OffCanvasContext());
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("offcanvas-header", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} offcanvas-header";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("class", customClass) });
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(OffCanvasContext), new OffCanvasContext());
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new OffCanvasHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_NotRender_InnerContent_When_Title_Missing()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(OffCanvasContext), new OffCanvasContext());
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("", output.Content.GetContent());
    }

    [Theory]
    [InlineData("My Title", "", "<h5 class=\"offcanvas-title\">My Title</h5>")]
    [InlineData("My Title", "myOffcanvas", "<h5 class=\"offcanvas-title\" id=\"myOffcanvasLabel\">My Title</h5>")]
    public async Task Should_Render_InnerContent_Title_When_Title_Provided(string title, string id, string expectedHtml)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(OffCanvasContext), new OffCanvasContext { Id = id });
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasHeaderTagHelper { Title = title };
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedHtml, output.Content.GetContent());
    }

    [Theory]
    [InlineData("My Title", "", "", "<h5 class=\"offcanvas-title\">My Title</h5>")]
    [InlineData("My Title", "myOffcanvas", "h4", "<h4 class=\"offcanvas-title\" id=\"myOffcanvasLabel\">My Title</h4>")]
    public async Task Should_Render_InnerContent_Title_WithCustomTag_When_Title_Provided(string title, string id, string tag, string expectedHtml)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(OffCanvasContext), new OffCanvasContext { Id = id });
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasHeaderTagHelper { Title = title };
        if (!string.IsNullOrEmpty(tag))
            helper.TitleTag = tag;
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedHtml, output.Content.GetContent());
    }
}