using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace Threax.AspNetCore.RazorExt
{
    /// <summary>
    /// This factory creates an ActionContext based on the current HttpContext. This allows the
    /// rendered views to use the url tag helpers, which do not work with a fresh context.
    /// </summary>
    class CurrentContextActionContextFactory : IViewRendererActionContextFactory
    {
        private IHttpContextAccessor httpContextAccessor;

        public CurrentContextActionContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public ActionContext GetActionContext()
        {
            var httpContext = httpContextAccessor.HttpContext;
            return new ActionContext(httpContext, httpContext.GetRouteData(), new ActionDescriptor());
        }
    }
}
