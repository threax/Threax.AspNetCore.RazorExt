using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Threax.AspNetCore.RazorExt
{
    /// <summary>
    /// This class will render a view to a string.
    /// </summary>
    /// <remarks>
    /// Modified from https://github.com/aspnet/Entropy/blob/dev/samples/Mvc.RenderViewToString/RazorViewToStringRenderer.cs
    /// Modified to abstract how the ActionContext is created.
    /// </remarks>
    class RazorViewStringRenderer : IRazorViewStringRenderer
    {
        private IRazorViewEngine viewEngine;
        private ITempDataProvider tempDataProvider;
        private IViewRendererActionContextFactory viewRendererFactory;

        public RazorViewStringRenderer(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IViewRendererActionContextFactory viewRendererFactory)
        {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.viewRendererFactory = viewRendererFactory;
        }

        public Task<string> RenderAsync(string viewName)
        {
            return RenderAsync(viewName, false);
        }

        public async Task<string> RenderAsync<TModel>(string viewName, TModel model)
        {
            var actionContext = viewRendererFactory.GetActionContext();
            var view = FindView(actionContext, viewName);

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext,
                        tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                return output.ToString();
            }
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = viewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations)); ;

            throw new InvalidOperationException(errorMessage);
        }
    }
}