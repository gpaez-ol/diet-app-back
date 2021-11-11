using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.WebAPI.Logic;

namespace AlgoFit.WebAPI.Controllers
{
    [Route("supermarket-list")]
    [ApiController]
    public class SupermarketController : ControllerBase
    {
        private readonly DietLogic _dietLogic;

        public SupermarketController(DietLogic dietLogic)
        {
            _dietLogic = dietLogic;
        }

        /// <summary>
        /// GetD Diet Ingredients
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(DietDTO), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetSupermarketList(Guid userId)
        {
            var dietDTO = await _dietLogic.GetSupermarketList(userId);
            return Ok(dietDTO);
        }

    }
}