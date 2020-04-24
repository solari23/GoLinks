// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using GoLinks;
using GoLinks.ShortlinkServices;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// A collection of extension methods designed to simplify adding core shortlink
    /// services to the DI container.
    /// </summary>
    public static class ShortlinkServiceExtensions
    {
        /// <summary>
        /// The name of the shortlink validating <see cref="IRouteConstraint"/>.
        /// </summary>
        public const string ShortlinkRouteConstraintName = "validShortlink";

        /// <summary>
        /// Adds the core shortlink services to the DI container.
        /// </summary>
        /// <param name="services">The DI container.</param>
        /// <param name="config">The service configuration.</param>
        /// <returns>The DI container, for chaining.</returns>
        public static IServiceCollection AddCoreShortlinkServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            ArgCheck.NotNull(services, nameof(services));
            ArgCheck.NotNull(config, nameof(config));

            services.AddSingleton<IShortlinkStorage, InMemoryShortlinkStorage>();
            services.AddSingleton<ShortlinkRepository>();
            return services;
        }

        /// <summary>
        /// Adds a custom <see cref="IRouteConstraint"/> called "validShortlink"
        /// which can be used to match routes containing valid shortlinks.
        /// </summary>
        /// <param name="routeOptions">The <see cref="RouteOptions"/> configuration object.</param>
        public static void AddShortlinkRouteConstraint(this RouteOptions routeOptions)
        {
            ArgCheck.NotNull(routeOptions, nameof(routeOptions));

            routeOptions.ConstraintMap.Add(ShortlinkRouteConstraintName, typeof(ShortlinkValidator));
        }
    }
}
