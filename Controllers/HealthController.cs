using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AlgoFit.Utils.Enums;
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
