using Microsoft.AspNetCore.Mvc;

namespace VersioningWithConventions.Features.Values
{
    /// <summary>
    /// Summary for /v2/values
    /// </summary>
    /// <remarks>
    /// Description for /v2/values
    /// </remarks>
    [ApiController]
    [Route("[controller]")]
    public class ValuesV2Controller : ControllerBase
    {
        /// <summary>
        /// Summary of POST /v2/values
        /// </summary>
        /// <remarks>
        /// Description of POST /v2/values
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Values from V2";
        }
    }
}
