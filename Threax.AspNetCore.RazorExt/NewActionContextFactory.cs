using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using System;

namespace Threax.AspNetCore.RazorExt
{
    /// <summary>
    /// This helper will create an action context based on a new HttpContext instance. Some of the built in
    /// mvc tag helpers will not work if this factory is used.
    /// </summary>
    class NewActionContextFactory : IViewRendererActionContextFactory
    {
        private IServiceProvider _serviceProvider;
        private IHttpContextAccessor _httpContextAccessor;

        public NewActionContextFactory(
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = _serviceProvider;
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}
