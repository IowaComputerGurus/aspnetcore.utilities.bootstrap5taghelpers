using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests;

[UsesVerify]
public class ProgressTagHelperTests : LoggingTagHelperTest
{
    public ProgressTagHelperTests(ITestOutputHelper output) : base(output)
    {
        
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public async Task Throws_Error_If_Invalid_ProgressValue(int value)
    {
        //Arrange
        var helper = new ProgressTagHelper { ProgressValue = value };

        //Act/Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await helper.Render());
    }

    [Theory]
    [InlineData(null)]
    [InlineData(BootstrapColor.Info)]
    [InlineData(BootstrapColor.Success)]
    [InlineData(BootstrapColor.Danger)]
    [InlineData(BootstrapColor.Warning)]
    public async Task Properly_Sets_BackgroundColor_IfProvided(BootstrapColor? color)
    {
        var output = await (new ProgressTagHelper() { BackgroundColor = color }).Render();
        await VerifyTagHelper(output).UseParameters(color);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Test Label")]
    public async Task Properly_Adds_AriaLabel_IfProvided(string ariaLabel)
    {
        var output = await (new ProgressTagHelper() { AriaLabel = ariaLabel }).Render();
        await VerifyTagHelper(output).UseParameters(ariaLabel);
    }

    [Theory]
    [InlineData("")]
    [InlineData("25%")]
    public async Task Properly_Adds_ProgressDisplayLabel_IfProvided(string progressLabel)
    {
        var output = await (new ProgressTagHelper() { ProgressDisplayLabel = progressLabel }).Render();
        await VerifyTagHelper(output).UseParameters(progressLabel);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(15)]
    public async Task Properly_Adds_ProgressValue(int progressValue)
    {
        var output = await (new ProgressTagHelper() { ProgressValue = progressValue }).Render();
        await VerifyTagHelper(output).UseParameters(progressValue);
    }

    [Fact]
    public async Task Properly_Adds_StripedClass()
    {
        var output = await (new ProgressTagHelper() { IsStriped = true}).Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Properly_Adds_AnimatedClass()
    {
        var output = await (new ProgressTagHelper() { IsAnimated = true }).Render();
        await VerifyTagHelper(output);
    }
}
