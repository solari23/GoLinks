// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GoShortlinks.Controllers
{
    /// <summary>
    /// Controller responsible for shortlinks resolution and redirection.
    /// </summary>
    public class RedirectionController : Controller
    {
        /// <summary>
        /// Implements the main functionality of the service where shortlinks are resolved and a redirection
        /// response is issued.
        /// </summary>
        /// <param name="shortlinkId">The ID of the requested shortlink.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [ActionName("ResolveAndRedirect")]
        public async Task<IActionResult> ResolveAndRedirectAsync(string shortlinkId)
        {
            await Task.Yield();
            return this.Ok($"Redirecting for shortlink '{shortlinkId}' with method '{this.Request.Method}'");
        }
    }
}
