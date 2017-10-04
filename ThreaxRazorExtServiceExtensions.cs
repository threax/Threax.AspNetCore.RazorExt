using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Threax.AspNetCore.RazorExt;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ThreaxRazorExtServiceExtensions
    {
        /// <summary>
        /// Add the ability to render razor views to strings. This is handy for generating things like e-mails using
        /// razor views instead of writing them manually with string builder. Inject an IRazorViewStringRenderer into
        /// your classes to use this.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <param name="configOptions">A callback to configure the options for the renderer, can be null to use the defaults.</param>
        /// <returns></returns>
        public static IServiceCollection AddRazorViewStringRenderer(this IServiceCollection services, Action<RazorViewStringRendererOptions> configOptions = null)
        {
            var options = new RazorViewStringRendererOptions();

            if(configOptions != null)
            {
                configOptions.Invoke(options);
            }

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            if (options.UseCurrentHttpContext)
            {
                services.TryAddScoped<IViewRendererActionContextFactory, CurrentContextActionContextFactory>();
            }
            else
            {
                services.TryAddScoped<IViewRendererActionContextFactory, NewActionContextFactory>();
            }
            services.TryAddScoped<IRazorViewStringRenderer, RazorViewStringRenderer>();

            return services;
        }
    }
}
