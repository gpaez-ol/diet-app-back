using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.WebAPI.Logic;

namespace AlgoFit.WebAPI.Controllers
{
    [Route("dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardLogic _dashboardLogic;

        public DashboardController(DashboardLogic dashboardLogic)
        {
            _dashboardLogic = dashboardLogic;
        }

        /// <summary>
        /// Get Dashboards
        /// </summary>
        /// <returns> Ok(DashboardDTO) </returns>
        [ProducesResponseType(typeof(DashboardDTO), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{userId}")]
        public async Task<ActionResult<DashboardDTO>> GetDashboard(Guid userId)
        {
            var dashboard = await _dashboardLogic.GetDashboard(userId);
            return Ok(dashboard);
        }
    }
}