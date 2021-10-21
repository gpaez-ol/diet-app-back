using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using AlgoFit.Utils.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AlgoFit.WebAPI.Logic;
using Outland.Utils.Pagination;

namespace AlgoFit.WebAPI.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryLogic _categoryLogic;

        public CategoryController(CategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        /// <summary>
        /// Get Categories implements pagination
        /// </summary>
        /// <returns> Ok({ totalPages: Total number of pages, pages: [] array of Categories }) </returns>
        [ProducesResponseType(typeof(IPaginationResult<CategoryDTO>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpGet]
        public ActionResult GetCategories([FromQuery] PaginationDataParams pagination)
        {
            var categories = _categoryLogic.GetCategories(pagination);
            return Ok(categories);
        }

        /// <summary>
        /// Get Category by Id
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(CategoryDTO), 400)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(Guid id)
        {
            var categoryDTO = await _categoryLogic.GetCategoryById(id);
            return Ok(categoryDTO);
        }


        /// <summary>
        /// Creates a new Category
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryCreateDTO newCategory)
        {
            await _categoryLogic.CreateCategory(newCategory);
            return Ok();
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(401)]
        [HttpPut("{categoryId}")]
        public async Task<ActionResult> UpdateCategory(CategoryCreateDTO category, Guid categoryId)
        {
            var updatedCategory = await _categoryLogic.UpdateCategory(category, categoryId);
            return Ok();
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <returns>Ok()</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            await _categoryLogic.DeleteCategory(categoryId);
            return Ok();
        }
    }
}