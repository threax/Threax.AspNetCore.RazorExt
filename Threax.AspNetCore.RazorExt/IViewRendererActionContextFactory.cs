using Microsoft.AspNetCore.Mvc;

namespace Threax.AspNetCore.RazorExt
{
    /// <summary>
    /// This interface defines a factory that returns ActionContexts for rendering views.
    /// </summary>
    public interface IViewRendererActionContextFactory
    {
        ActionContext GetActionContext();
    }
}