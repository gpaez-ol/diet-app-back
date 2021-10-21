using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AlgoFit.WebAPI.Logic;

namespace AlgoFit.Controllers
{
    [ApiController]
    [Route("signup")]
    public class SignupController : ControllerBase
    {
        private readonly UserLogic _userLogic;
        public SignupController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProfileDTO), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ProfileDTO>> SignupAsync(SignUpDTO signup)
        {
            try
            {
               ProfileDTO createdUser = await _userLogic.CreateUser(signup);
                return Ok(createdUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
