// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace GoLinks.Telemetry
{
    /// <summary>
    /// Telemetry EventId definitions.
    /// </summary>
    public static class TelemetryEvents
    {
        /// <summary>
        /// Events with ID >= this value are informational.
        /// </summary>
        public const int InformationalEventIdBase = 10000;

        /// <summary>
        /// Events with ID >= this value are warnings.
        /// </summary>
        public const int WarningEventIdBase = 30000;

        /// <summary>
        /// Events with ID >= this value are errors.
        /// </summary>
        public const int ErrorEventIdBase = 50000;

        //
        // Informational Events
        //

        /// <summary>
        /// Logged to trace the per-request data payload.
        /// </summary>
        public static EventId PerRequestTrace { get; } = new EventId(InformationalEventIdBase + 0, nameof(PerRequestTrace));

        /// <summary>
        /// Logged by shortlink validation subroutines.
        /// </summary>
        public static EventId ShortlinkValidationResult { get; } = new EventId(InformationalEventIdBase + 1, nameof(ShortlinkValidationResult));

        //
        // Warning Events
        //

        // No warnings defined yet.

        //
        // Error Events
        //

        /// <summary>
        /// Logged when an exception is unhandled and caught by the top-level exception handler.
        /// </summary>
        public static EventId UnhandledException { get; } = new EventId(ErrorEventIdBase + 0, nameof(UnhandledException));
    }
}
