using Xunit;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests;

public class BadgeTagHelperTests : AbstractTagHelperTest
{
    [Theory]
    [InlineData(BootstrapColor.Primary, false, "badge text-bg-primary")]
    [InlineData(BootstrapColor.Secondary, false, "badge text-bg-secondary")]
    [InlineData(BootstrapColor.Success, false, "badge text-bg-success")]
    [InlineData(BootstrapColor.Warning, false, "badge text-bg-warning")]
    [InlineData(BootstrapColor.Info, false, "badge text-bg-info")]
    [InlineData(BootstrapColor.Danger, false, "badge text-bg-danger")]
    [InlineData(BootstrapColor.Info, true, "badge text-bg-info rounded-pill")]
    [InlineData(BootstrapColor.Danger, true, "badge text-bg-danger rounded-pill")]
    public void Should_Render_ProperClass(BootstrapColor color, bool asPill, string expectedClass)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new BadgeTagHelper {BadgeColor = color, DisplayAsPill = asPill};
        helper.Process(context, output);

        //Assert
        Assert.Equal("span", output.TagName);
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public void Should_NotRender_If_Display_Is_Hidden()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new BadgeTagHelper {HideDisplay = true};
        helper.Process(context, output);

        //Assert
        Assert.True(output.Content.IsEmptyOrWhiteSpace);
    }
}