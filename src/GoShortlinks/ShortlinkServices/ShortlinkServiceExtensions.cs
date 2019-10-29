// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using GoShortlinks;
using GoShortlinks.ShortlinkServices;
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
    }
}
