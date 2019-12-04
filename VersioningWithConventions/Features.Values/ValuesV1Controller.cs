using Microsoft.AspNetCore.Mvc;

namespace VersioningWithConventions.Features.Values
{
    /// <summary>
    /// Summary for /v1/values
    /// </summary>
    /// <remarks>
    /// Description for /v1/values
    /// </remarks>
    [ApiController]
    [Route("[controller]")]
    public class ValuesV1Controller : ControllerBase
    {
        /// <summary>
        /// Summary of POST /v1/values
        /// </summary>
        /// <remarks>
        /// Description of POST /v1/values
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Values from V1";
        }
    }
}
