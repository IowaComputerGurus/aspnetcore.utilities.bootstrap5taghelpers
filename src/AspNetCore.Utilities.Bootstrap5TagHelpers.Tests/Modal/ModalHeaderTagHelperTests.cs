using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Modal;

public class ModalHeaderTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_ThrowException_WhenMissingContext()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        Exception exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<KeyNotFoundException>(exceptionResult);
    }

    [Fact]
    public async Task Should_ThrowException_WhenContextIsNull()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), null);
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        Exception exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<ArgumentException>(exceptionResult);
    }

    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("modal-header", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} modal-header";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("class", customClass) });
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_NotRender_InnerContent_When_Title_Missing()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("", output.Content.GetContent());
    }

    [Theory]
    [InlineData("My Title", "", "<h2 class=\"modal-title fs-5\">My Title</h2>")]
    [InlineData("My Title", "myModal", "<h2 class=\"modal-title fs-5\" id=\"myModalLabel\">My Title</h2>")]
    public async Task Should_Render_InnerContent_Title_When_Title_Provided(string title, string id, string expectedHtml)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext { Id = id });
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper { Title = title };
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedHtml, output.Content.GetContent());
    }

    [Theory]
    [InlineData("My Title", "", "", "<h2 class=\"modal-title fs-5\">My Title</h2>")]
    [InlineData("My Title", "myModal", "h5", "<h5 class=\"modal-title fs-5\" id=\"myModalLabel\">My Title</h5>")]
    public async Task Should_Render_InnerContent_Title_WithCustomTag_When_Title_Provided(string title, string id, string tag, string expectedHtml)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext { Id = id });
        TagHelperOutput output = MakeTagHelperOutput(" ");
        
        //Act
        var helper = new ModalHeaderTagHelper { Title = title };
        if (!string.IsNullOrEmpty(tag))
            helper.TitleTag = tag;
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedHtml, output.Content.GetContent());
    }
}