using Microsoft.AspNetCore.Mvc;
using AlgoFit.Data.DTO;
using AlgoFit.WebAPI.Logic;
using System.Threading.Tasks;

namespace AlgoFit.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly SessionLogic _sessionLogic;
        public LoginController(SessionLogic sessionLogic)
        {
            _sessionLogic = sessionLogic;
        }

        // POST api/login/
        /// <summary>
        /// This POST method returns a 200 Ok Response when the credentials are Ok
        /// </summary>
        /// <returns>Ok()</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProfileDTO), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Login(LoginDTO login)
        {
           var result = await _sessionLogic.Login(login);
        return Ok(result);
        }
    }
}
