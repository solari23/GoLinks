// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoLinks
{
    /// <summary>
    /// Initialization subroutines for the web service.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        /// <param name="env">Hosting environment metadata.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.Configuration = configuration;
            this.WebHostEnvironment = env;
        }

        /// <summary>
        /// Gets the service configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the hosting environment metadata.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Initializes the DI container with dependency injected services.
        /// </summary>
        /// <param name="services">The DI services container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCoreShortlinkServices(this.Configuration);
            services.AddShortlinkTelemetryHelpers(this.Configuration);

            services.AddRouting(options =>
            {
                options.AddShortlinkRouteConstraint();
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        /// <summary>
        /// Configures the ASP.Net Core middleware pipeline.
        /// </summary>
        /// <param name="app">The builder for the web app.</param>
        public void Configure(IApplicationBuilder app)
        {
            if (this.WebHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseShortlinkTelemetryHelpers();

            app.UseRouting();

            // TODO: Add authN/Z middleware here.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ShortlinkRedirection",
                    pattern: "{**shortlinkId:minlength(1):validShortlink}",
                    defaults: new
                    {
                        controller = "Redirection",
                        action = "ResolveAndRedirect",
                    });
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (this.WebHostEnvironment.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
