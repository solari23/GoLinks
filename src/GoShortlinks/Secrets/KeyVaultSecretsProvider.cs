// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Options;

namespace GoShortlinks.Secrets
{
    /// <summary>
    /// Implementation of <see cref="ISecretsProvider"/> that fetches secrets from Azure KeyVault.
    /// </summary>
    /// <remarks>
    /// This provider uses a Service Identity to authenticate to Azure KeyVault:
    /// https://docs.microsoft.com/en-us/azure/key-vault/service-to-service-authentication.
    /// </remarks>
    public class KeyVaultSecretsProvider : ISecretsProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultSecretsProvider"/> class.
        /// </summary>
        /// <param name="options">Configuration options for the <see cref="KeyVaultSecretsProvider"/>.</param>
        public KeyVaultSecretsProvider(IOptions<KeyVaultSecretsProviderOptions> options)
        {
            ArgCheck.NotNull(options, nameof(options));

            this.Options = options.Value;
            this.KeyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));
        }

        /// <summary>
        /// Gets the configuration options for the <see cref="KeyVaultSecretsProvider"/>.
        /// </summary>
        private KeyVaultSecretsProviderOptions Options { get; }

        /// <summary>
        /// Gets the <see cref="KeyVaultClient"/> used to communicate with Azure KeyVault.
        /// </summary>
        private KeyVaultClient KeyVaultClient { get; }

        /// <inheritdoc/>
        public async Task<string> GetSecretAsync(string name)
        {
            ArgCheck.NotEmpty(name, nameof(name));

            var secretBundle = await this.KeyVaultClient.GetSecretAsync(this.Options.KeyvaultBaseUrl, name).ConfigureAwait(false);
            return secretBundle.Value;
        }
    }
}
