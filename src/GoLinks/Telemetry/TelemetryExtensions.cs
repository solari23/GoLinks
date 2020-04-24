// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using GoLinks;
using GoLinks.Telemetry;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// A collection of extension methods designed to simplify adding shortlink telemetry
    /// services to the DI container.
    /// </summary>
    public static class TelemetryExtensions
    {
        /// <summary>
        /// Adds the shortlink telemetry services to the DI container.
        /// </summary>
        /// <param name="services">The DI container.</param>
        /// <param name="config">The service configuration.</param>
        /// <returns>The DI container, for chaining.</returns>
        public static IServiceCollection AddShortlinkTelemetryHelpers(
            this IServiceCollection services,
            IConfiguration config)
        {
            ArgCheck.NotNull(services, nameof(services));
            ArgCheck.NotNull(config, nameof(config));

            services.AddHttpContextAccessor();
            services.AddScoped<PerRequestData>();

            return services;
        }

        /// <summary>
        /// Adds shortlink telemetry middleware to the pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The application builder, for chaining.</returns>
        public static IApplicationBuilder UseShortlinkTelemetryHelpers(
            this IApplicationBuilder app)
        {
            ArgCheck.NotNull(app, nameof(app));

            app.UseMiddleware<PerRequestTelemetryMiddleware>();

            return app;
        }
    }
}
