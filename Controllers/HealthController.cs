using System;
using Microsoft.AspNetCore.Mvc;
namespace AlgoFit.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        public HealthController()
        {
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        public ActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
