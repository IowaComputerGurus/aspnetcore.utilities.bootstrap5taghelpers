using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Modal;

public class ModalHeaderDismissTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public void Should_Render_As_Button()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderDismissTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal("button", output.TagName);
    }

    [Theory]
    [InlineData("aria-label", "Close")]
    [InlineData("data-bs-dismiss", "modal")]
    [InlineData("class", "btn-close")]
    [InlineData("type", "button")]
    public void Should_Set_Needed_Attributes(string expectedAttribute, string expectedValue)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderDismissTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal(expectedValue, output.Attributes[expectedAttribute].Value);
    }
}