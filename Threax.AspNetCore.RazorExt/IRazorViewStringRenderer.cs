using System.Threading.Tasks;

namespace Threax.AspNetCore.RazorExt
{
    public interface IRazorViewStringRenderer
    {
        Task<string> RenderAsync(string viewName);
        Task<string> RenderAsync<TModel>(string viewName, TModel model);
    }
}