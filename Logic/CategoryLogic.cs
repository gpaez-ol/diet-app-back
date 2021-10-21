using System;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AlgoFit.Errors;
using AlgoFit.Repositories.Manager;
using AlgoFit.Utils.Pagination;
using AlgoFit.Utils.Pagination.Interfaces;
using AutoMapper;
using Outland.Utils.Pagination;
namespace AlgoFit.WebAPI.Logic
{
    public class CategoryLogic
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CategoryLogic(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IPaginationResult<CategoryDTO> GetCategories(PaginationDataParams pagination)
        {
            var query = _repositoryManager.CategoryRepository.GetAllAsDTOs();
            return query.ToPagination(pagination.Page, pagination.PageSize);
        }
        public async Task<CategoryDTO> GetCategoryById(Guid id)
        {
            var category = await _repositoryManager.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new AlgoFitError(404, "Category Not Found");
            }
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }
        public async Task CreateCategory(CategoryCreateDTO newCategory)
        {
            try
            {
                var category = new Category
                        {Name = newCategory.Name,
                        Description =  newCategory.Description
                        };
                await _repositoryManager.CategoryRepository.AddAsync(category);
                _repositoryManager.Save();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<CategoryDTO> UpdateCategory(CategoryCreateDTO newCategory,Guid categoryId)
        {
            try
            {
                var oldCategory = await _repositoryManager.CategoryRepository.GetByIdAsync(categoryId);
                if (oldCategory != null)
                {
                    _mapper.Map(newCategory, oldCategory);
                    Category updatedCategory = await _repositoryManager.CategoryRepository.UpdateAsync(oldCategory);
                    _repositoryManager.Save();
                    return _mapper.Map<CategoryDTO>(updatedCategory);
                }
                throw new AlgoFitError(404, "Category Not Found");;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await _repositoryManager.CategoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new AlgoFitError(404, "Category Not Found");
            }
            await _repositoryManager.CategoryRepository.DeleteAsync(category);
            _repositoryManager.Save();
        }
    }
}