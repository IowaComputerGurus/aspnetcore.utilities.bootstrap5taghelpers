using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.FromFramework;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Form;

[UsesVerify]
public sealed class FormInputTagHelperTests : ModelTagHelperTest<FormInputTagHelper, TestModel>
{
    public FormInputTagHelperTests(ITestOutputHelper output) : base(output)
    {

    }

    [Fact]
    public async Task Renders()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_Large()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        tagHelper.InputSize = BootstrapFormControlSize.Lg;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_Small()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        tagHelper.InputSize = BootstrapFormControlSize.Sm;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_CustomContainerCssClass()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        tagHelper.ContainerClass = "col";
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_WithNoCustomContainerCssClass()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        tagHelper.ContainerClass = string.Empty;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_PlainTextReadOnly()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        tagHelper.PlainTextReadOnly = true;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_RequiredClassWhenNeeded()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: new TestModel(), propertyName: nameof(TestModel.RequiredIntField));
        tagHelper.PlainTextReadOnly = true;
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);
    }

    internal override FormInputTagHelper TagHelperFactory(IHtmlGenerator htmlGenerator, ModelExpression modelExpression, ViewContext viewContext)
        => new(htmlGenerator) { For = modelExpression, ViewContext = viewContext };
}

