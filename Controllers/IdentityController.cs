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
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        public IdentityController()
        {
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ProfileDTO), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        public ActionResult Login(LoginDTO login)
        {
            // TODO: Add logic to do an actual login
            var result = new ProfileDTO{
                FirstName = "John",
                LastName = "Doe",
                Avatar = "https://pbs.twimg.com/profile_images/1442676072831537155/56uDoaxL_400x400.jpg",
                Type = EnumHelper.GetEnumText(UserType.Customer)
            };
            return Ok(result);
        }
    }
}
