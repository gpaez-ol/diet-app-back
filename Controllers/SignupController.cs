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
        public async Task<ActionResult> SignupAsync(SignUpDTO signup)
        {
            try
            {
                User createdUser = await _userLogic.CreateUser(signup);
                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
