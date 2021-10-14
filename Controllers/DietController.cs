using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using AlgoFit.Utils.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.WebAPI.Logic;
using Outland.Utils.Pagination;

namespace AlgoFit.WebAPI.Controllers
{
    [Route("diet")]
    [ApiController]
    public class DietController : ControllerBase
    {
        private readonly DietLogic _dietLogic;

        public DietController(DietLogic dietLogic)
        {
            _dietLogic = dietLogic;
        }

        /// <summary>
        /// Get Diets implements pagination
        /// </summary>
        /// <returns> Ok({ totalPages: Total number of pages, pages: [] array of Diets }) </returns>
        [ProducesResponseType(typeof(IPaginationResult<DietItemDTO>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpGet]
        public ActionResult GetDiets([FromQuery] PaginationDataParams pagination)
        {
            var diets = _dietLogic.GetDiets(pagination);
            return Ok(diets);
        }

        /// <summary>
        /// GetDiet by Id
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(DietDTO), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDietById(Guid id)
        {
            var dietDTO = await _dietLogic.GetDietById(id);
            return Ok(dietDTO);
        }


        /// <summary>
        /// Creates a new Diet
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult> CreateDiet(DietCreateDTO newDiet)
        {
            await _dietLogic.CreateDiet(newDiet);
            return Ok();
        }

        /// <summary>
        /// Update Diet
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPut("{dietId}")]
        public async Task<ActionResult> UpdateDiet(DietCreateDTO diet, Guid dietId)
        {
            await _dietLogic.UpdateDiet(diet, dietId);
            return Ok();
        }

        /// <summary>
        /// Delete Diet
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpDelete("{dietId}")]
        public async Task<ActionResult> DeleteDiet(Guid dietId)
        {
            await _dietLogic.DeleteDiet(dietId);
            return Ok();
        }
    }
}