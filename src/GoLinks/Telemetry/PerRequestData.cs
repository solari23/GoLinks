// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace GoLinks.Telemetry
{
    /// <summary>
    /// A container for data recorded per-request.
    /// </summary>
    public class PerRequestData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerRequestData"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Grants access to the request HTTP context.</param>
        public PerRequestData(IHttpContextAccessor httpContextAccessor)
        {
            ArgCheck.NotNull(httpContextAccessor, nameof(httpContextAccessor));

            this.RequestId = httpContextAccessor.HttpContext.TraceIdentifier;
        }

        /// <summary>
        /// A unique identifier for the request.
        /// </summary>
        public string RequestId { get; }

        /// <summary>
        /// Tracks whether or not the request is successful.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// The action that was invoked.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Tracks how long the request took, in milliseconds.
        /// </summary>
        public long TimeTakenMs { get; set; }

        /// <summary>
        /// Tracks the HTTP status of the response.
        /// </summary>
        public int HttpStatus { get; set; }

        /// <summary>
        /// The path of the request URL.
        /// </summary>
        public string RequestPath { get; set; }
    }
}
