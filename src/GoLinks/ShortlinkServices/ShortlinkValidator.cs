// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GoLinks.Telemetry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace GoLinks.ShortlinkServices
{
    /// <summary>
    /// Helper that implements shortlink validation logic.
    /// </summary>
    public class ShortlinkValidator : IRouteConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortlinkValidator"/> class.
        /// </summary>
        /// <param name="logger">The service telemetry logger.</param>
        public ShortlinkValidator(ILogger<ShortlinkValidator> logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the service telemetry logger.
        /// </summary>
        public ILogger<ShortlinkValidator> Logger { get; }

        /// <summary>
        /// Gets a set of knowns static files that cannot be shortlinks.
        /// </summary>
        private static HashSet<string> KnownStaticFiles { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "manifest.json",
            "index.html",
            "favicon.ico",
        };

        /// <summary>
        /// Gets a set of known directories that are serving SPA assets.
        /// </summary>
        private static HashSet<string> KnownSpaDirectories { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "static",
            "sockjs-node",
        };

        /// <summary>
        /// Checks if the given shortlink candidate is of a valid shortlink format.
        /// </summary>
        /// <param name="candidate">The string to check.</param>
        /// <param name="logger">The service telemetry logger.</param>
        /// <returns>True if the given candidate is a valid shortlink, false otheriwse.</returns>
        public static bool IsValidShortlink(string candidate, ILogger logger = null)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                logger.LogInformation(TelemetryEvents.ShortlinkValidationResult, $"'{candidate}' is rejected because it is empty.");
                return false;
            }

            if (KnownStaticFiles.Contains(candidate))
            {
                logger.LogInformation(TelemetryEvents.ShortlinkValidationResult, $"'{candidate}' is rejected because it is a known static file.");
                return false;
            }

            var topDirectory = candidate.Split('/').First();
            if (KnownSpaDirectories.Contains(topDirectory))
            {
                logger.LogInformation(TelemetryEvents.ShortlinkValidationResult, $"'{candidate}' is rejected because it is a known static directory.");
                return false;
            }

            if (candidate.EndsWith("hot-update.js", StringComparison.OrdinalIgnoreCase))
            {
                // These are webpack artifacts that allow for hot re-compilation during development.
                logger.LogInformation(TelemetryEvents.ShortlinkValidationResult, $"'{candidate}' is rejected a valid shortlink because it is a webpack file.");
                return false;
            }

            logger.LogInformation(TelemetryEvents.ShortlinkValidationResult, $"'{candidate}' is good to go!");
            return true;
        }

        /// <inheritdoc />
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            ArgCheck.NotNull(httpContext, nameof(httpContext));
            ArgCheck.NotNull(route, nameof(route));
            ArgCheck.NotEmpty(routeKey, nameof(routeKey));
            ArgCheck.NotNull(values, nameof(values));

            if (values.TryGetValue(routeKey, out object rawRouteValue))
            {
                var routeValue = Convert.ToString(rawRouteValue, CultureInfo.InvariantCulture);
                return IsValidShortlink(routeValue, this.Logger);
            }

            return false;
        }
    }
}