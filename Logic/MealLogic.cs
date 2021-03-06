using System;
using System.Collections.Generic;
using System.Linq;
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
                meal = await _repositoryManager.MealRepository.AddAsync(meal);
                meal.Ingredients = new List<MealIngredient>();
                newMeal.MealIngredients.ForEach(mealIngredient =>
                {
                    var mIngredient = new MealIngredient
                    {
                        Notes = mealIngredient.Notes,
                        Amount = mealIngredient.Amount,
                        IngredientId = mealIngredient.IngredientId,
                        MealId = meal.Id
                    };
                    meal.Ingredients.Add(mIngredient);
                });
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
                    var ingredientsToDelete = oldMeal.Ingredients.Where(i => newMeal.MealIngredients.All(mi => mi.IngredientId != i.IngredientId)).ToList();
                    if(ingredientsToDelete.Count > 0)
                    {
                        foreach(var ingredientToDelete in ingredientsToDelete)
                        {
                            oldMeal.Ingredients.Remove(ingredientToDelete);
                        }
                     
                    }
                    var ingredientsToAdd = newMeal.MealIngredients.Where(mi => oldMeal.Ingredients.All(i => i.IngredientId != mi.IngredientId)).ToList();
                    if(ingredientsToAdd.Count > 0 )
                    {
                        foreach(var ingredientToAdd in ingredientsToAdd)
                        {
                            var newMealIngredient = new MealIngredient{
                                IngredientId = ingredientToAdd.IngredientId,
                                Amount = ingredientToAdd.Amount,
                                Notes = ingredientToAdd.Notes
                            };
                            oldMeal.Ingredients.Add(newMealIngredient);
                        }
                    }
                    oldMeal.Name = newMeal.Name;
                    oldMeal.Kilocalories = newMeal.Kilocalories;
                    oldMeal.Preparation = newMeal.Preparation;
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