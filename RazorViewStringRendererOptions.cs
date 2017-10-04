using System;
using System.Collections.Generic;
using System.Text;

namespace Threax.AspNetCore.RazorExt
{
    public class RazorViewStringRendererOptions
    {
        /// <summary>
        /// Set this to true to use the current http context when rendering views.
        /// This is the reccomended configuration so the built in razor tag helpers will
        /// work correctly. The default is true.
        /// </summary>
        public bool UseCurrentHttpContext { get; set; } = true;
    }
}
