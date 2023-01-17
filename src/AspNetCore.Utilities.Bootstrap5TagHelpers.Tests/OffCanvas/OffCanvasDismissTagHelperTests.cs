using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.OffCanvas;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.OffCanvas;

public class OffCanvasDismissTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public void Should_Render_As_Button()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasDismissTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal("button", output.TagName);
    }

    [Theory]
    [InlineData("aria-label", "Close")]
    [InlineData("data-bs-dismiss", "offcanvas")]
    [InlineData("class", "btn-close")]
    [InlineData("type", "button")]
    public void Should_Set_Needed_Attributes(string expectedAttribute, string expectedValue)
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasDismissTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal(expectedValue, output.Attributes[expectedAttribute].Value);
    }
}