using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using AlgoFit.Utils.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.WebAPI.Logic;
using Outland.Utils.Pagination;

namespace AlgoFit.WebAPI.Controllers
{
    [Route("meal")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly MealLogic _mealLogic;

        public MealController(MealLogic mealLogic)
        {
            _mealLogic = mealLogic;
        }

        /// <summary>
        /// Get Meals implements pagination
        /// </summary>
        /// <returns> Ok({ totalPages: Total number of pages, pages: [] array of Meals }) </returns>
        [ProducesResponseType(typeof(IPaginationResult<MealItemDTO>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpGet]
        public ActionResult GetMeals([FromQuery] PaginationDataParams pagination)
        {
            var meals = _mealLogic.GetMeals(pagination);
            return Ok(meals);
        }

        /// <summary>
        /// GetMeal by Id
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(MealDTO), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMealById(Guid id)
        {
            var mealDTO = await _mealLogic.GetMealById(id);
            return Ok(mealDTO);
        }


        /// <summary>
        /// Creates a new Meal
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult> CreateMeal(MealCreateDTO newMeal)
        {
            await _mealLogic.CreateMeal(newMeal);
            return Ok();
        }

        /// <summary>
        /// Update Meal
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPut("{mealId}")]
        public async Task<ActionResult> UpdateMeal(MealCreateDTO meal, Guid mealId)
        {
            await _mealLogic.UpdateMeal(meal, mealId);
            return Ok();
        }

        /// <summary>
        /// Delete Meal
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpDelete("{mealId}")]
        public async Task<ActionResult> DeleteMeal(Guid mealId)
        {
            await _mealLogic.DeleteMeal(mealId);
            return Ok();
        }
    }
}