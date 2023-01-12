using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Modal;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.OffCanvas;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.OffCanvas;

[UsesVerify]
public class OffCanvasTagHelperTests : LoggingTagHelperTest
{
    public OffCanvasTagHelperTests(ITestOutputHelper output) : base(output)
    {

    }

    [Fact]
    public async Task Should_Not_Error_With_Missing_Id_Attribute()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new OffCanvasTagHelper();
        Exception exception = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task Should_Render_Without_Id_Attribute()
    {
        var output = await (new OffCanvasTagHelper()).Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Should_Add_Context_Object_With_Provided_Id()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        var providedId = "testingOffCanvas";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("id", providedId) });
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new OffCanvasTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        var generatedContext = Assert.IsType<OffCanvasContext>(context.Items[typeof(OffCanvasContext)]);
        Assert.Equal(providedId, generatedContext.Id);
    }

    [Fact]
    public async Task Should_Render_With_StaticBackdrop()
    {
        var output = await (new OffCanvasTagHelper(){StaticBackdrop = true}).Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Should_Render_With_EnabledBodyScrolling()
    {
        var output = await (new OffCanvasTagHelper() { EnableBodyScrolling = true }).Render();
        await VerifyTagHelper(output);
    }

    [Theory]
    [InlineData(OffCanvasPlacement.End)]
    [InlineData(OffCanvasPlacement.Start)]
    [InlineData(OffCanvasPlacement.Top)]
    [InlineData(OffCanvasPlacement.Bottom)]
    public async void Should_Render_With_Placement(OffCanvasPlacement placement)
    {
        var output = await (new OffCanvasTagHelper() { Placement = placement}).Render();
        await VerifyTagHelper(output).UseParameters(placement);
    }
}
