using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using AlgoFit.Utils.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NFact.WebAPI.Logic;
using Outland.Utils.Pagination;

namespace AlgoFit.WebAPI.Controllers
{
    [Route("ingredient")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IngredientLogic _ingredientLogic;

        public IngredientController(IngredientLogic ingredientLogic)
        {
            _ingredientLogic = ingredientLogic;
        }

        /// <summary>
        /// Get Ingredients implements pagination
        /// </summary>
        /// <returns> Ok({ totalPages: Total number of pages, pages: [] array of Ingredients }) </returns>
        [ProducesResponseType(typeof(IPaginationResult<IngredientDTO>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpGet]
        public ActionResult GetIngredients([FromQuery] PaginationDataParams pagination)
        {
            var ingredients = _ingredientLogic.GetIngredients(pagination);
            return Ok(ingredients);
        }

        /// <summary>
        /// Get Ingredient by Id
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IngredientDTO), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetIngredientById(Guid id)
        {
            var ingredientDTO = await _ingredientLogic.GetIngredientById(id);
            return Ok(ingredientDTO);
        }


        /// <summary>
        /// Creates a new Ingredient
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult> CreateIngredient(IngredientCreateDTO newIngredient)
        {
            await _ingredientLogic.CreateIngredient(newIngredient);
            return Ok();
        }

        /// <summary>
        /// Update Ingredient
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPut("{ingredientId}")]
        public async Task<ActionResult> UpdateIngredient(IngredientCreateDTO ingredient, Guid ingredientId)
        {
            var updatedIngredient = await _ingredientLogic.UpdateIngredient(ingredient, ingredientId);
            return Ok();
        }

        /// <summary>
        /// Delete Ingredient
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult> DeleteIngredient(Guid ingredientId)
        {
            await _ingredientLogic.DeleteIngredient(ingredientId);
            return Ok();
        }
    }
}