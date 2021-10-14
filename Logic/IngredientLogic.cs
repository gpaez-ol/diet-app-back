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
    public class IngredientLogic
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public IngredientLogic(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IPaginationResult<IngredientDTO> GetIngredients(PaginationDataParams pagination)
        {
            var query = _repositoryManager.IngredientRepository.GetAllAsDTOs();
            return query.ToPagination(pagination.Page, pagination.PageSize);
        }

        public async Task CreateIngredient(IngredientCreateDTO newIngredient)
        {
            try
            {
                var ingredient = new Ingredient{Name = newIngredient.Name};
                await _repositoryManager.IngredientRepository.AddAsync(ingredient);
                _repositoryManager.Save();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<IngredientDTO> UpdateIngredient(IngredientCreateDTO newIngredient,Guid ingredientId)
        {
            try
            {
                var oldIngredient = await _repositoryManager.IngredientRepository.GetByIdAsync(ingredientId);
                if (oldIngredient != null)
                {
                    _mapper.Map(newIngredient, oldIngredient);
                    Ingredient updatedIngredient = await _repositoryManager.IngredientRepository.UpdateAsync(oldIngredient);
                    _repositoryManager.Save();
                    return _mapper.Map<IngredientDTO>(updatedIngredient);
                }
                throw new AlgoFitError(404, "Ingredient Not Found");;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task DeleteIngredient(Guid ingredientId)
        {
            var ingredient = await _repositoryManager.IngredientRepository.GetByIdAsync(ingredientId);
            if (ingredient == null)
            {
                throw new AlgoFitError(404, "Ingredient Not Found");
            }
            await _repositoryManager.IngredientRepository.DeleteAsync(ingredient);
            _repositoryManager.Save();
        }
        public async Task<IngredientDTO> GetIngredientById(Guid id)
        {
            var ingredient = await _repositoryManager.IngredientRepository.GetByIdAsync(id);
            if (ingredient == null)
            {
                throw new AlgoFitError(404, "Ingredient Not Found");
            }
            var ingredientDTO = _mapper.Map<IngredientDTO>(ingredient);
            return ingredientDTO;
        }
    }
}