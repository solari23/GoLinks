// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace GoLinks.Secrets
{
    /// <summary>
    /// Configuration options for the <see cref="KeyVaultSecretsProvider"/>.
    /// </summary>
    public class KeyVaultSecretsProviderOptions
    {
        /// <summary>
        /// Gets or sets the base URL to the Azure Keyvault instance.
        /// </summary>
        [SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Keyvault client takes this as a string.")]
        public string KeyvaultBaseUrl { get; set; }
    }
}
