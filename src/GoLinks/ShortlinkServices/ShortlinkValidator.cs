// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace GoLinks.ShortlinkServices
{
    /// <summary>
    /// Helper that implements shortlink validation logic.
    /// </summary>
    public class ShortlinkValidator : IRouteConstraint
    {
        /// <summary>
        /// A set of knowns static files that cannot be shortlinks.
        /// </summary>
        private static HashSet<string> KnownStaticFiles { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "manifest.json",
            "index.html",
            "favicon.ico",
        };

        /// <summary>
        /// A set of known directories that are serving SPA assets.
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
        /// <returns>True if the given candidate is a valid shortlink, false otheriwse.</returns>
        public static bool IsValidShortlink(string candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                return false;
            }

            if (KnownStaticFiles.Contains(candidate))
            {
                return false;
            }

            var topDirectory = candidate.Split('/').First();
            if (KnownSpaDirectories.Contains(topDirectory))
            {
                return false;
            }

            if (candidate.EndsWith("hot-update.js", StringComparison.OrdinalIgnoreCase))
            {
                // These are webpack artifacts that allow for hot re-compilation during development.
                return false;
            }

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
                return IsValidShortlink(routeValue);
            }

            return false;
        }
    }
}