using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.RazorExt
{
    /// <summary>
    /// Extensions to the IUrlHelper to add absolute urls.
    /// </summary>
    /// <remarks>
    /// Thanks to Muhammad Rehan Saeed at https://stackoverflow.com/questions/30755827/getting-absolute-urls-using-asp-net-core-mvc-6
    /// Changed to take IUrlHelper instead of UrlHelper
    /// </remarks>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Create an absolute content url.
        /// </summary>
        /// <param name="urlHelper">The urlhelper instance.</param>
        /// <param name="contentPath">The path to generate an absolute url for.</param>
        /// <param name="scheme">The scheme (http, https etc) to use, null means use current request scheme.</param>
        /// <returns></returns>
        public static string AbsoluteContent(this IUrlHelper urlHelper, string contentPath, string scheme = null)
        {
            var request = urlHelper.ActionContext.HttpContext.Request;
            if(scheme == null)
            {
                scheme = request.Scheme;
            }
            return new Uri(new Uri($"{scheme}://{request.Host.Value}"), urlHelper.Content(contentPath)).ToString();
        }
    }
}
