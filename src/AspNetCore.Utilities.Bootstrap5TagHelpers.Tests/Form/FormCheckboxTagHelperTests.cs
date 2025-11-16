using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.FromFramework;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Form;

[UsesVerify]
public sealed class FormCheckboxTagHelperTests : ModelTagHelperTest<FormCheckboxTagHelper, TestModel>
{
    public FormCheckboxTagHelperTests(ITestOutputHelper output) : base(output)
    {

    }

    [Fact]
    public async Task Renders()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: false, propertyName: nameof(TestModel.CheckboxField));
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_Inline()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: false, propertyName: nameof(TestModel.CheckboxField));
        tagHelper.IsInline = true;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_Switch()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: false, propertyName: nameof(TestModel.CheckboxField));
        tagHelper.IsSwitch = true;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_InlineSwitch()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: false, propertyName: nameof(TestModel.CheckboxField));
        tagHelper.IsInline = true;
        tagHelper.IsSwitch = true;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    internal override FormCheckboxTagHelper TagHelperFactory(IHtmlGenerator htmlGenerator, ModelExpression modelExpression, ViewContext viewContext)
        => new(htmlGenerator) { For = modelExpression, ViewContext = viewContext };
}