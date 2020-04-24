// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using GoLinks.ShortlinkServices;
using Microsoft.AspNetCore.Mvc;

namespace GoLinks.Controllers
{
    /// <summary>
    /// Controller responsible for shortlinks resolution and redirection.
    /// </summary>
    public class RedirectionController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectionController"/> class.
        /// </summary>
        /// <param name="shortlinkRepository">The repository of shortlinks that can be resolved.</param>
        public RedirectionController(ShortlinkRepository shortlinkRepository)
        {
            this.ShortlinkRepository = shortlinkRepository ?? throw new ArgumentNullException(nameof(shortlinkRepository));
        }

        /// <summary>
        /// Gets the shortlink repository.
        /// </summary>
        private ShortlinkRepository ShortlinkRepository { get; }

        /// <summary>
        /// Implements the main functionality of the service where shortlinks are resolved and a redirection
        /// response is issued.
        /// </summary>
        /// <param name="shortlinkId">The ID of the requested shortlink.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [ActionName("ResolveAndRedirect")]
        public async Task<IActionResult> ResolveAndRedirectAsync(string shortlinkId)
        {
            var shortlinkData = await this.ShortlinkRepository.ResolveShortlinkAsync(shortlinkId).ConfigureAwait(false);

            if (shortlinkData is null)
            {
                // TODO: Configure the default redirection for when a shortlink doesn't resolve.
                return this.Redirect("https://reddit.com");
            }

            return this.RedirectPreserveMethod(shortlinkData.LongUrl);
        }
    }
}
