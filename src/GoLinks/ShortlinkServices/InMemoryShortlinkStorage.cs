// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoLinks.ShortlinkServices
{
    /// <summary>
    /// A static in-memory storage for shortlinks, used for testing in development environments.
    /// </summary>
    public class InMemoryShortlinkStorage : IShortlinkStorage
    {
        /// <summary>
        /// Gets the underlying static in-memory shortlink data.
        /// </summary>
        private Dictionary<string, ShortlinkData> Data { get; } = new Dictionary<string, ShortlinkData>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "Test1",
                new ShortlinkData
                {
                    Id = "Test1",
                    LongUrl = "https://github.com/solari23/GoLinks",
                }
            },
        };

        /// <inheritdoc/>
        public Task<ShortlinkData> GetDataAsync(string shortlinkId)
        {
            ArgCheck.NotEmpty(shortlinkId, nameof(shortlinkId));

            if (this.Data.TryGetValue(shortlinkId, out var data))
            {
                return Task.FromResult(data);
            }

            return Task.FromResult<ShortlinkData>(null);
        }

        /// <inheritdoc/>
        public Task PutDataAsync(ShortlinkData shortlinkData)
        {
            ArgCheck.NotNull(shortlinkData, nameof(shortlinkData));

            this.Data[shortlinkData.Id] = shortlinkData;
            return Task.CompletedTask;
        }
    }
}
