// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace GoShortlinks.Controllers
{
    /// <summary>
    /// Controller that implements the shortlink management CRUD operations.
    /// </summary>
    [Route("~/api/v1")]
    public class ManagementApiController : Controller
    {
        /// <summary>
        /// A temporary placeholder API to validate that the service is running.
        /// </summary>
        /// <returns>A basic message.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            await Task.Yield();
            return this.Content("Boo!");
        }
    }
}
