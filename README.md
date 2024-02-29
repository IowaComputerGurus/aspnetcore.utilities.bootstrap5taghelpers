# AspNetCore Bootstrap5 Tag Helpers ![](https://img.shields.io/github/license/iowacomputergurus/aspnetcore.utilities.bootstrap5taghelpers.svg)

A collection of TagHelpers for ASP.NET Core that make utilizing the Bootstrap 5.x library easier to use for developers.  Designed to reduce code effort substantially

![Build Status](https://github.com/IowaComputerGurus/aspnetcore.utilities.bootstrap5taghelpers/actions/workflows/ci-build.yml/badge.svg)

![](https://img.shields.io/nuget/v/icg.aspnetcore.utilities.bootstrap5taghelpers.svg) ![](https://img.shields.io/nuget/dt/icg.aspnetcore.utilities.bootstrap5taghelpers.svg)

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6bfab3e64b8d4138aefc6152d39bd753)](https://app.codacy.com/gh/IowaComputerGurus/aspnetcore.utilities.bootstrap5taghelpers/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

## Usage Expectations

These tag helpers are only for markup display, your web project must properly include references to Bootstrap and must abide by all license and other requirements of Bootstrap for the functionality to be utilized here.  For more on how to include Bootstrap within your project please reference their documentation.


## Setup - Registering TagHelpers

You must modify your `_viewimports.cshtml` file by adding

``` html+razor
@addTagHelper *, ICG.AspNetCore.Utilities.Bootstrap5TagHelpers
```

## Usage

The goal of these tag helpers is to reduce the redundant coding, and compliance with various features of not only the Bootstrap library but form patterns.  Within the "Sample" project you will find examples of all tag helper usage.  However, the below shows a quick example of the power of these tag helpers.

### Before Bootstrap 5 Tag Helper

The following markup is how you would output a model-bound field for a password field, including a note on complexity and validation.

``` razor
<div class="mb-3">
    <label asp-for="Password" class="form-label"></label>
    <input asp-for="Password" class="form-control" />
    <span asp-validation-for="Password" class="text-danger"></span>
    <div class="form-text text-muted">Must be 8 characters with letters & numbers</div>
</div>
```

This is a total of *306* characters with spaces or *268* without.  Granted we get some help with auto-complete etc.

### After Using Bootstrap 5 Tag Helper

You can take the entire above example and simplify it to the following

``` razor
<form-text-input asp-for="Password">
    <form-note>Must be 8 characters with letters & numbers</form-note>
</form-text-input>
```

This is a total of *126* characters with spaces or *112* without.  With intellisense, the actual typing characters are much less.  Your form view is also substantially reduced, making lines of code per form reduced.  For forms without notes the markup improvement is even better.


## Included Tag Helpers

At this time tag helpers have been implemented for the following elements.

| Element | Description of Implementation |
| --- | --- |
| Accordion | Full support for implementation of accordion, including stay-open modes |
| Alerts | Full support for implementation of alerts, including dismissible alerts |
| Badges | Full support for implementation of badges of all Bootstrap color variations |
| Buttons | Full support for implementation of buttons of all Bootstrap color variations, including outlines |
| Cards | Support for Card, Card Header, Card Header Actions, and Card body elements |
| Environment Alert | An extension of the `<environment>` tag helper to render as an alert style |
| Input | Support for Form input controls for anything tied to the `<input>` tag including ASP.NET Code Model Binding & Validation |
| Modals | Support for modal dialogs, including Modal Body, header, footer, dismiss, and toggles |
| Offcanvas | Full support for implementation of offcanvas display, including sizing/placement |
| TextArea | Support for Form input controls tied to the `<textarea>` tag including ASP.NET Core Model Binding & Validation | 

If you find that we are missing a particular tag helper, we welcome contributions!

## Special Features

The tag helpers automatically inspect form elements and add a "required" css class in addition to the `form-label` class default allowing for indication of required fields easily with CSS such as the following.

```` css
label.required:after {
    content: " *";
    color: red;
}
````