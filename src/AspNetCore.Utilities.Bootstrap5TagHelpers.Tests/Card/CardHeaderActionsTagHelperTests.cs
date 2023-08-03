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
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderActionsTagHelper();
        var exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<KeyNotFoundException>(exceptionResult);
    }

    [Fact]
    public async Task Should_ThrowException_WhenContextIsNull()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), null);
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderActionsTagHelper();
        var exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<ArgumentException>(exceptionResult);
    }

    [Theory]
    [InlineData("d-flex")]
    [InlineData("flex-nowrap")]
    [InlineData("mt-2")]
    [InlineData("mt-sm-0")]
    public async Task Should_Render_With_ClassAdded(string expectedClass)
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext());
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderActionsTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        var classValue = output.Attributes["class"].Value;
        Assert.NotNull(classValue);
        var classString = classValue.ToString();
        Assert.True(classString?.Contains(expectedClass));
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} d-flex flex-nowrap mt-2 mt-sm-0";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext());
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new CardHeaderActionsTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}