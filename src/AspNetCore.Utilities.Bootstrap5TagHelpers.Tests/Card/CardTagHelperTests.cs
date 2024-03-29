﻿using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Card;
using ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Card;

public class CardTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Not_Error_With_Missing_Id_Attribute()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardTagHelper();
        var exception = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task Should_Add_Context_Object_Without_Id_If_Missing()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        var generatedContext = Assert.IsType<CardContext>(context.Items[typeof(CardContext)]);
        Assert.Null(generatedContext.Id);
    }

    [Fact]
    public async Task Should_Add_Context_Object_With_Provided_Id()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var providedId = "testingCard";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("id", providedId)});
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new CardTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        var generatedContext = Assert.IsType<CardContext>(context.Items[typeof(CardContext)]);
        Assert.Equal(providedId, generatedContext.Id);
    }

    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("card", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} card";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new CardTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}