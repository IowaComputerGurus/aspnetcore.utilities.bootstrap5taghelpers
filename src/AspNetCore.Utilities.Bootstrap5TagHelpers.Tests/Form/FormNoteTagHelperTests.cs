using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Form;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Form;

public class FormNoteTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Render_As_SpanTag_By_Default()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("span", output.TagName);
    }

    [Fact]
    public async Task Should_Render_As_CustomTag_If_Defined()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");
        var expectedTagName = "sup";

        //Act
        var helper = new FormNoteTagHelper {TagName = expectedTagName};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedTagName, output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_DefaultClasses_Added()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");
        var expectedClasses = "form-text";

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClasses, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} form-text";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Render_Without_Duplicate_Classes_IfManuallyAdded()
    {
        //Arrange
        var customClass = "form-text";
        var expectedClass = "form-text";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}