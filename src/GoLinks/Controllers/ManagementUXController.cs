// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace GoLinks.Controllers
{
    /// <summary>
    /// Controller that implements the shortlink management UX.
    /// </summary>
    [Route("~/")]
    public class ManagementUXController : Controller
    {
        /// <summary>
        /// A temporary placeholder for the management UX.
        /// </summary>
        /// <returns>A basic message rendered in an HTML page.</returns>
        [HttpGet]
        public async Task<IActionResult> HomeAsync()
        {
            await Task.Yield();
            return this.Content("<html><body><h1>Home Page!</h1></body></html>", "text/html");
        }
    }
}
