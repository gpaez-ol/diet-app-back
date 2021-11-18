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
    public class DietLogic
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public DietLogic(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<List<SupermarketItemDTO>> GetSupermarketList(Guid userId) 
        {
            var user = await _repositoryManager.UserRepository.GetUserByIdAsync(userId);
            var supermarketItemList = new List<SupermarketItemDTO>();
            if(user.Diet != null && (user.Diet.Meals != null || user.Diet.Meals.Count > 0))
            {
                var totalDietMeals = user.Diet.Meals.Count; 
                var passedDays = (DateTime.Today - user.DietStartedAt.GetValueOrDefault()).Days;
                var eatenMeals = passedDays * 3;
                if(eatenMeals > totalDietMeals)
                {
                    user.DietStartedAt = DateTime.Today;
                    eatenMeals = 0;
                    await _repositoryManager.UserRepository.UpdateAsync(user);
                }
                var supermarketItems = user.Diet.Meals
                                .OrderBy(dm => dm.MealNumber)
                                .Skip(eatenMeals)
                                .Take(3*7)
                                .SelectMany(dm => dm.Meal.Ingredients
                                    .Select(mi => 
                                    new SupermarketItemDTO
                                    {
                                      Id = mi.IngredientId,
                                      Name = mi.Ingredient.Name,
                                      Amount = mi.Amount
                                    }).ToList()).ToList();
                foreach(var supermarketItem in supermarketItems.GroupBy(si => si.Id).ToList().Select(si => si.First()))
                {
                    supermarketItemList.Add(new SupermarketItemDTO
                    {
                        Id = supermarketItem.Id,
                        Name = supermarketItem.Name,
                        Amount = supermarketItems.Where(si => si.Id == supermarketItem.Id).Sum(si => si.Amount)
                    });
                }
            }
                            return supermarketItemList;
        }
        public IPaginationResult<DietItemDTO> GetDiets(PaginationDataParams pagination,List<Guid> categoryIds,string searchText)
        {
            IQueryable<Diet> query = _repositoryManager.DietRepository.GetAllAsQueryable();
           // Conditions
            
             
           if (!(searchText == null || searchText.Length < 0))
           {
               var predicate = LinqKit.PredicateBuilder.New<Diet>();
               predicate.Or(d => d.Name.Contains(searchText));
               predicate.Or(d => d.Description.Contains(searchText));
               query = query.Where(predicate);
           }
           if (categoryIds != null && categoryIds.Count > 0)
            {   var predicate = LinqKit.PredicateBuilder.New<Diet>(); 
                foreach(var categoryId in categoryIds)
                predicate.Or(diet => diet.Categories.Any(dc => dc.CategoryId == categoryId));
                query = query.Where(predicate);
            }

            IQueryable<DietItemDTO> results = query.Select(diet => new DietItemDTO
            {
                Id = diet.Id,
                Name = diet.Name,
                ImageRef = diet.ImageRef,
                CategoryIds = diet.Categories.Select(c => c.CategoryId.GetValueOrDefault()).ToList()
            });
            return results.ToPagination(pagination.Page, pagination.PageSize);
        }

        public async Task CreateDiet(DietCreateDTO newDiet)
        {
            try
            {
                var diet = new Diet
                    {
                        Id = new Guid(),
                        Name = newDiet.Name,
                        Description = newDiet.Description,
                        Type = newDiet.Type,
                        ImageRef = "https://algofit-assets.s3.amazonaws.com/diet-template.jfif"
                    };
                diet = await _repositoryManager.DietRepository.AddAsync(diet);
                diet.Categories = new List<DietCategory>();
                newDiet.CategoryIds.ForEach(categoryId =>
                {
                    var dCategory = new DietCategory
                    {
                        CategoryId = categoryId,
                        DietId = diet.Id

                    };
                    diet.Categories.Add(dCategory);
                });
                diet.Meals = new List<DietMeal>();
                newDiet.DietMeals.ForEach(dietMeal =>
                {
                    var dMeal = new DietMeal
                    {
                        MealId = dietMeal.Id,
                        DietId = diet.Id,
                        MealNumber = dietMeal.MealNumber

                    };
                    diet.Meals.Add(dMeal);
                });
                _repositoryManager.Save();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<DietDTO> UpdateDiet(DietCreateDTO newDiet,Guid dietId)
        {
            try
            {
                var oldDiet = await _repositoryManager.DietRepository.GetByIdAsync(dietId);
                if (oldDiet != null)
                {
                     
                    var categoriesToDelete = oldDiet.Categories.Where(c => newDiet.CategoryIds.All(ci => c.CategoryId != ci)).ToList();
                    if(categoriesToDelete.Count > 0)
                    {
                        foreach(var categoryToDelete in categoriesToDelete)
                        {
                            oldDiet.Categories.Remove(categoryToDelete);
                        }
                     
                    }
                    var categoriesToAdd = newDiet.CategoryIds.Where(ci => oldDiet.Categories.All(c => c.CategoryId != ci)).ToList();
                    if(categoriesToAdd.Count > 0 )
                    {
                        foreach(var categoryToAdd in categoriesToAdd)
                        {
                            var newDietCategory = new DietCategory{
                                CategoryId = categoryToAdd
                            };
                            oldDiet.Categories.Add(newDietCategory);
                        }
                    }
                    var mealsToDelete = oldDiet.Meals.Where(d => newDiet.DietMeals.All(dm => d.MealId != dm.Id)).ToList();
                    if(mealsToDelete.Count > 0)
                    {
                        foreach(var mealToDelete in mealsToDelete )
                        {
                            oldDiet.Meals.Remove(mealToDelete);
                        }
                    }
                    var mealsToAdd = newDiet.DietMeals.Where(dm => oldDiet.Meals.All(d => d.MealId != dm.Id)).ToList();
                    if(mealsToAdd.Count > 0 )
                    {
                        foreach(var mealToAdd in mealsToAdd)
                        {
                            var newDietMeal = new DietMeal{
                                MealId = mealToAdd.Id,
                                MealNumber = mealToAdd.MealNumber
                            };
                            oldDiet.Meals.Add(newDietMeal);
                        }
                    }
                    oldDiet.Description = newDiet.Description;
                    oldDiet.Name = newDiet.Name;
                    oldDiet.Type  = newDiet.Type;
                    Diet updatedDiet = await _repositoryManager.DietRepository.UpdateAsync(oldDiet);
                    _repositoryManager.Save();
                    return _mapper.Map<DietDTO>(updatedDiet);
                }
                throw new AlgoFitError(404, "Diet Not Found");;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task DeleteDiet(Guid dietId)
        {
            var diet = await _repositoryManager.DietRepository.GetByIdAsync(dietId);
            if (diet == null)
            {
                throw new AlgoFitError(404, "Diet Not Found");
            }
            await _repositoryManager.DietRepository.DeleteAsync(diet);
            _repositoryManager.Save();
        }
        public async Task<DietDTO> GetDietById(Guid id)
        {
            var diet = await _repositoryManager.DietRepository.GetByIdAsync(id);
            if (diet == null)
            {
                throw new AlgoFitError(404, "Diet Not Found");
            }
            var dietDTO = _mapper.Map<DietDTO>(diet);
            return dietDTO;
        }
        public async Task SubscribeToDiet(Guid dietId,Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserByIdAsync(userId);
            var diet = await _repositoryManager.DietRepository.GetByIdAsync(dietId);
            var today = DateTime.Today;
            if(diet != null)
            {
                user.DietId = dietId;
                user.DietStartedAt = today;
            }
            await _repositoryManager.UserRepository.UpdateAsync(user);
            _repositoryManager.Save();
        }
    }
}