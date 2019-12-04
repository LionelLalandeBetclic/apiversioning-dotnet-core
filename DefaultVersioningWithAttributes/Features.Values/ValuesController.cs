using Api.Conventions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DefaultVersioningInHeaders.Features.Values
{
    /// <summary>
    /// Summary for /values
    /// </summary>
    /// <remarks>
    /// Description for /values
    /// </remarks>
    // Remove the "api" prefix if base URL already contains it in Development, Staging, Production environments!
    // Define ONLY 1 routing solution! NOT BOTH.
    //[Route("api/[controller]")] // for query string routing and/or header routing
    [Route("api/v{version:apiVersion}/[controller]")] // for URL routing
    [ApiController]
    [ApiVersion("1", Deprecated = true)]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1", Deprecated = true)]
    [ApiVersion("1.2", Deprecated = true)]
    [ApiVersion("2")]
    [ApiVersion("2.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    // Can add more versions here...
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Summary of GET /v1/values
        /// </summary>
        /// <remarks>
        /// Description of GET /v1/values
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Obsolete]
        [MapToApiVersion("1")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult<string> GetV1()
        {
            return "Values from V1";
        }

        /// <summary>
        /// Summary of GET /v1.2/values
        /// </summary>
        /// <remarks>
        /// Description of GET /v1.2/values
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Obsolete]
        [MapToApiVersion("1.2")]
        public ActionResult<string> GetV12()
        {
            return "Values from V1.2";
        }

        /// <summary>
        /// Summary of GET /v2/values
        /// </summary>
        /// <remarks>
        /// Description of GET /v2/values
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("2")]
        [MapToApiVersion("2.0")]
        public ActionResult<string> GetV2(Pagination pagination)
        {
            return $"Values from V2 with pagination: [{pagination.Offset}, {pagination.Limit}]";
        }

        /// <summary>
        /// Summary of POST /v2/values
        /// </summary>
        /// <remarks>
        /// Description of POST /v2/values
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("2")]
        [MapToApiVersion("2.0")]
        public ActionResult<Request> PostV2(Request request)
        {
            return request;
        }
    }
    /// <summary>
    /// My request.
    /// </summary>
    public sealed class Request
    {
        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public Value Value { get; set; }
    }
    /// <summary>
    /// Values.
    /// </summary>
    public enum Value
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// Foo
        /// </summary>
        Foo,

        /// <summary>
        /// Bar
        /// </summary>
        Bar,

        /// <summary>
        /// FooBar
        /// </summary>
        FooBar,

        /// <summary>
        /// MyVeryLongNameWhichDoesNothing
        /// </summary>
        [Obsolete]
        MyVeryLongNameWhichDoesNothing
    }
}
