// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoShortlinks
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
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the service configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes the DI container with dependency injected services.
        /// </summary>
        /// <param name="services">The DI services container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: Temporary hack to appease analyzers.
            this.Configuration.ToString();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// Configures the ASP.Net Core middleware pipeline.
        /// </summary>
        /// <param name="app">The builder for the web app.</param>
        /// <param name="env">Hosting environment metadata.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // TODO: Temporary hack to appease analyzers.
            this.Configuration.ToString();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
