// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace GoShortlinks.Secrets
{
    /// <summary>
    /// Common interface for a provider that fetches secrets from a secure storage.
    /// </summary>
    public interface ISecretsProvider
    {
        /// <summary>
        /// Retrieves a secret value from secure storage.
        /// </summary>
        /// <param name="name">The name of the secret to retrieve.</param>
        /// <returns>The secret value.</returns>
        Task<string> GetSecretAsync(string name);
    }
}
