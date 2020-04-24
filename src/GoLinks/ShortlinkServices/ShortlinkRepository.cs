// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace GoLinks.ShortlinkServices
{
    /// <summary>
    /// The core service that provides shortlink access to shortlink data.
    /// </summary>
    public class ShortlinkRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortlinkRepository"/> class.
        /// </summary>
        /// <param name="storageProvider">The shortlink storage provider for the service.</param>
        public ShortlinkRepository(
            IShortlinkStorage storageProvider)
        {
            ArgCheck.NotNull(storageProvider, nameof(storageProvider));

            this.StorageProvider = storageProvider;
        }

        /// <summary>
        /// Gets the shortlink storage provider.
        /// </summary>
        private IShortlinkStorage StorageProvider { get; }

        /// <summary>
        /// Retrieves information about the shortlink given its ID.
        /// </summary>
        /// <param name="shortlinkId">The ID of the shortlink to resolve.</param>
        /// <returns>The requested shortlink data, or null if the ID does not resolve.</returns>
        public async Task<ShortlinkData> ResolveShortlinkAsync(string shortlinkId)
        {
            ArgCheck.NotEmpty(shortlinkId, nameof(shortlinkId));

            // TODO: Add in-memory caching.
            return await this.StorageProvider.GetDataAsync(shortlinkId).ConfigureAwait(false);
        }
    }
}
