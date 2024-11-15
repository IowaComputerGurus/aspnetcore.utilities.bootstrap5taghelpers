using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Spinner;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Spinner;

[UsesVerify]
public class SpinnerTagHelperTests(ITestOutputHelper output) : LoggingTagHelperTest(output)
{
    [Fact]
    public async Task Should_Render_BorderSpinner_Default()
    {
        var output = await (new SpinnerTagHelper()).Render();
        output.AssertContainsClass("spinner-border");
        await VerifyTagHelper(output);
    }

    [Theory]
    [InlineData(SpinnerMode.Border, "spinner-border")]
    [InlineData(SpinnerMode.Grow, "spinner-grow")]
    public async Task Properly_Sets_Class_For_Spinner(SpinnerMode mode, string expected)
    {
        var output = await (new SpinnerTagHelper() { SpinnerMode = mode }).Render();
        output.AssertContainsClass(expected);
        await VerifyTagHelper(output).UseParameters(mode);
    }

    [Theory]
    [InlineData(SpinnerMode.Border, "spinner-border-sm")]
    [InlineData(SpinnerMode.Grow, "spinner-grow-sm")]
    public async Task Properly_Sets_Class_For_Spinner_Small(SpinnerMode mode, string expected)
    {
        var output = await (new SpinnerTagHelper() { SpinnerMode = mode, IsSmall = true}).Render();
        output.AssertContainsClass(expected);
        await VerifyTagHelper(output).UseParameters(mode);
    }

    [Theory]
    [InlineData(BootstrapColor.Info, "text-info")]
    [InlineData(BootstrapColor.Success, "text-success")]
    [InlineData(BootstrapColor.Danger, "text-danger")]
    [InlineData(BootstrapColor.Warning, "text-warning")]
    [InlineData(BootstrapColor.Primary, "text-primary")]
    [InlineData(BootstrapColor.Secondary, "text-secondary")]
    [InlineData(BootstrapColor.Light, "text-light")]
    [InlineData(BootstrapColor.Dark, "text-dark")]
    public async Task Properly_Sets_Class_For_TextColor(BootstrapColor color, string expected)
    {
        var output = await (new SpinnerTagHelper() { Color = color}).Render();
        output.AssertContainsClass(expected);
        await VerifyTagHelper(output).UseParameters(color);
    }

    [Fact]
    public async Task Should_Render_Aria_Hidden_WhenSet()
    {
        var output = await (new SpinnerTagHelper() { AriaHidden = true }).Render();
        await VerifyTagHelper(output);
    }
}
