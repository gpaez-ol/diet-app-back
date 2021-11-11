using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.WebAPI.Logic;
using System.Collections.Generic;

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
        /// Get Supermarket Ingredient List
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(List<SupermarketItemDTO>), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetSupermarketList(Guid userId)
        {
            var dietDTO = await _dietLogic.GetSupermarketList(userId);
            return Ok(dietDTO);
        }

    }
}