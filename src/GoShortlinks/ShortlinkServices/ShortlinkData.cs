// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace GoShortlinks.ShortlinkServices
{
    /// <summary>
    /// Data structure that holds information about a shortlink.
    /// </summary>
    public class ShortlinkData
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shortlink.
        /// </summary>
        /// <remarks>
        /// This is the short/vanity name used in the shortlink URL path
        /// that resolves to the full URL that the shortlink points to.
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the full URL that the shortlink redirects to.
        /// </summary>
        [SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Maintained as a string to simplify storage operations.")]
        public string LongUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the redirection preserves the HTTP method and body.
        /// </summary>
        /// <remarks>
        /// In practice, this allows the service to determine whether to respond with a 302 or a 307.
        /// </remarks>
        public bool PreserveHttpMethod { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the shortlink is currently active.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
