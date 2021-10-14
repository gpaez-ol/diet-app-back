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
namespace NFact.WebAPI.Logic
{
    public class MealLogic
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public MealLogic(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IPaginationResult<MealItemDTO> GetMeals(PaginationDataParams pagination)
        {
            var query = _repositoryManager.MealRepository.GetAllAsDTOs();
            return query.ToPagination(pagination.Page, pagination.PageSize);
        }
        public async Task<MealDTO> GetMealById(Guid id)
        {
            var meal = await _repositoryManager.MealRepository.GetByIdAsync(id);
            if (meal == null)
            {
                throw new AlgoFitError(404, "Meal Not Found");
            }
            var mealDTO = _mapper.Map<MealDTO>(meal);
            return mealDTO;
        }
        public async Task CreateMeal(MealCreateDTO newMeal)
        {
            try
            {
                var meal = new Meal
                        {Name = newMeal.Name,
                        Preparation = newMeal.Preparation,
                        Kilocalories = newMeal.Kilocalories,
                        ImageRef = "https://thumbs.dreamstime.com/b/funny-chef-tomato-avatar-character-vector-illustration-design-94050856.jpg"
                        };
                await _repositoryManager.MealRepository.AddAsync(meal);
                _repositoryManager.Save();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<MealDTO> UpdateMeal(MealCreateDTO newMeal,Guid mealId)
        {
            try
            {
                var oldMeal = await _repositoryManager.MealRepository.GetByIdAsync(mealId);
                if (oldMeal != null)
                {
                    _mapper.Map(newMeal, oldMeal);
                    Meal updatedMeal = await _repositoryManager.MealRepository.UpdateAsync(oldMeal);
                    _repositoryManager.Save();
                    return _mapper.Map<MealDTO>(updatedMeal);
                }
                throw new AlgoFitError(404, "Meal Not Found");;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task DeleteMeal(Guid mealId)
        {
            var meal = await _repositoryManager.MealRepository.GetByIdAsync(mealId);
            if (meal == null)
            {
                throw new AlgoFitError(404, "Meal Not Found");
            }
            await _repositoryManager.MealRepository.DeleteAsync(meal);
            _repositoryManager.Save();
        }
    }
}