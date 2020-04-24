// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace GoLinks.Telemetry
{
    /// <summary>
    /// Middleware that records per-request telemetry. It should be placed early in the pipeline.
    /// </summary>
    public class PerRequestTelemetryMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerRequestTelemetryMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware delegate in the middleware pipeline.</param>
        /// <param name="logger">The service telemetry logger.</param>
        public PerRequestTelemetryMiddleware(RequestDelegate next, ILogger<PerRequestTelemetryMiddleware> logger)
        {
            ArgCheck.NotNull(next, nameof(next));
            ArgCheck.NotNull(logger, nameof(logger));

            this.Next = next;
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the next middleware delegate in the middleware pipeline.
        /// </summary>
        private RequestDelegate Next { get; }

        /// <summary>
        /// Gets the service telemetry logger.
        /// </summary>
        private ILogger<PerRequestTelemetryMiddleware> Logger { get; }

        /// <summary>
        /// Method invoked by the ASP.Net runtime to run this middleware.
        /// </summary>
        /// <param name="httpContext">The HTTP request context.</param>
        /// <param name="perRequestData">The per-request data for the current request (DI injected).</param>
        /// <returns>A task that completes on completion of the method.</returns>
        public async Task InvokeAsync(HttpContext httpContext, PerRequestData perRequestData)
        {
            ArgCheck.NotNull(httpContext, nameof(httpContext));
            ArgCheck.NotNull(perRequestData, nameof(perRequestData));

            var buildVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            // Add common response headers.
            httpContext.Response.Headers.Add("x-alker-requestId", perRequestData.RequestId);
            httpContext.Response.Headers.Add("x-alker-version", buildVersion);

            // Log common request properties.
            perRequestData.RequestPath = httpContext.Request.Path;

            var watch = Stopwatch.StartNew();

            try
            {
                httpContext.Response.OnStarting(() =>
                {
                    watch.Stop();
                    return Task.CompletedTask;
                });

                await this.Next(httpContext).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.Logger.LogError(TelemetryEvents.UnhandledException, e, "An unhandled exception was encountered.");
                perRequestData.Success = false;

                throw;
            }
            finally
            {
                perRequestData.Action = GetActionFromRequestContext(httpContext);
                perRequestData.TimeTakenMs = watch.ElapsedMilliseconds;
                perRequestData.HttpStatus = httpContext.Response.StatusCode;

                var perRequestTelemetry = JsonSerializer.Serialize(perRequestData);
                this.Logger.LogInformation(TelemetryEvents.PerRequestTrace, perRequestTelemetry);
            }
        }

        private static string GetActionFromRequestContext(HttpContext context)
            => context.Features.Get<IRoutingFeature>()?.RouteData?.Values["action"].ToString() ?? string.Empty;

    }
}
