using Microsoft.AspNetCore.Mvc;
using AlgoFit.Data.DTO;
using AlgoFit.WebAPI.Logic;
using System.Threading.Tasks;
using AlgoFit.Security.Utils;

namespace AlgoFit.Controllers
{
    [ApiController]
    [Route("logout")]
    public class LogoutController : ControllerBase
    {
        public LogoutController()
        {
        }

         // GET api/logout/
        /// <summary>
        /// This GET method returns a 200 Ok Response and remove the cookie with the JWT 
        /// </summary>
        /// <returns>Ok()</returns>
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(401)]
        public ActionResult Logout()
        {
            TokenHandler.RemoveTokenToCookie(Response);
            return Ok();
        }
    }
}
