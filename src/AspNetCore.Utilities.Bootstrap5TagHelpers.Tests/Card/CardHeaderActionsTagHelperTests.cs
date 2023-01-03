using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Card;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Card;

public class CardHeaderActionsTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_ThrowException_WhenMissingContext()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderActionsTagHelper();
        Exception exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<KeyNotFoundException>(exceptionResult);
    }

    [Fact]
    public async Task Should_ThrowException_WhenContextIsNull()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), null);
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderActionsTagHelper();
        Exception exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<ArgumentException>(exceptionResult);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext());
        TagHelperOutput output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderActionsTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("ml-auto", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} ml-auto";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute> { new("class", customClass) });
        TagHelperContext context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext());
        TagHelperOutput output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new CardHeaderActionsTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}