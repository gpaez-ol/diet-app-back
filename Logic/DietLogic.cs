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
                var passedDays = (DateTime.Today - user.DietStartedAt.GetValueOrDefault()).Days;
                var eatenMeals = passedDays * 3;
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
                        ImageRef = "data:image/jpeg;base64,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"
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
                    _mapper.Map(newDiet, oldDiet);
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