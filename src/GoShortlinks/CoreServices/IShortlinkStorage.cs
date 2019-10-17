// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace GoShortlinks.CoreServices
{
    /// <summary>
    /// Interface for a shortlink storage medium.
    /// </summary>
    public interface IShortlinkStorage
    {
        /// <summary>
        /// Retrieves shortlink data from storage.
        /// </summary>
        /// <param name="shortlinkId">The ID of the shortlink to retrieve data for.</param>
        /// <returns>
        /// The <see cref="ShortlinkData"/> associated with the given <paramref name="shortlinkId"/>>,
        /// or null if none found.
        /// </returns>
        Task<ShortlinkData> GetDataAsync(string shortlinkId);

        /// <summary>
        /// Writes shortlink data to storage.
        /// </summary>
        /// <param name="shortlinkData">The shortlink data to write.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task PutDataAsync(ShortlinkData shortlinkData);
    }
}
