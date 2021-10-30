# Threax.AspNetCore.RazorExt

This library makes rendering a razor view to a string easier.

## Add Service
Add the service by calling `services.AddRazorViewStringRenderer();`.

# Email Example

## Setup E-Mail Layout
First, go into the Shared folder and create a file called `_EmailLayout.cshtml`. This will serve as the master layout template for emails like `_Layout.cshtml` does for pages. The minimal version of this will resemble the following:

```
@using Threax.AspNetCore.RazorExt
<html>
<head>
    <style type=text/css>
    </style>
</head>
<body>
    @RenderBody()
</body>
</html>
```

This will import some additional extensions from the Threax.AspNetCore.RazorExt library. The most helpful here thing is an extension for Url called AbsoluteContent, which will render content URLs using the Razor URL helper to automatically resolve the host and create a full URL (since the view is in an email and not your app's website). Using this in your page looks like the following:

`@Url.AbsoluteContent("images/email-banner.jpg", "https")`

Ideally, use https here (so browser-based emails will load the images without user warnings).

Furthermore, you can add whatever styles and other template info you want to include in all your emails.

## Creating Individual Email Views

In the Views folder, create an Emails folder for your email templates to live in. These individual email templates will work like most any other Razor view. You can declare a model and a layout, which should be set to _EmailLayout. The most basic version of this should resemble the following:

```
@model ModelClass
@{
    Layout = "_EmailLayout";
}
```

This sets the model class and the layout to the appropriate values. From here you can write your HTML below the closing brace.

You should be able to use most of the built-in tag helpers to help you create URLs in your emails. If you want to use the anchor tag helper (a) then specifying the asp-protocol should provide you an absolute URL. This will resemble the following: 

```
<a asp-controller="Home" asp-action="Index" asp-protocol="https">An Absolute Url to Home.Index starting with https</a>
```

This is everything required to have Razor render emails. This is much easier than building strings by hand. Additionally, you will be able to use all of the Razor features such as layouts, partial pages, URL generation, and models.

## Render String
Render your e-mails by calling
```
var body = await viewRenderer.RenderAsync("Views/Emails/TheEmail.cshtml", modelHere);
```
where viewRenderer is a `IRazorViewStringRenderer`.