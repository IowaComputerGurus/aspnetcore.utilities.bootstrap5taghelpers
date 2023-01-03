using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Modal;

public class ModalBodyTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalBodyTagHelper();
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
        var helper = new ModalBodyTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("modal-body", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} modal-body";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("class", customClass) });
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalBodyTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}