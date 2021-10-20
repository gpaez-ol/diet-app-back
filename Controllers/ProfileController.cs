using Microsoft.AspNetCore.Mvc;
using AlgoFit.Data.DTO;
using AlgoFit.WebAPI.Logic;
using System.Threading.Tasks;
using System;

namespace AlgoFit.Controllers
{
    [ApiController]
    [Route("profile")]
    public class ProfileController : ControllerBase
    {
        private readonly UserLogic _userLogic;
        public ProfileController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        // GET api/login/
        /// <summary>
        /// This GET  method returns a ProfileDTO
        /// </summary>
        /// <returns>Ok()</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ProfileDTO), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> GetProfile(Guid userId)
        {
           var result = await _userLogic.GetProfile(userId);
        return Ok(result);
        }
    }
}
