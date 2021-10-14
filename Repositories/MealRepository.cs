
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgoFit.Data.Context;
using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoFit.Repositories
{
    public class MealRepository
    {
        protected readonly AlgoFitContext _context;
        public MealRepository(AlgoFitContext context)
        {
            _context = context;
        }

        public async Task<Meal> AddAsync(Meal newMeal)
        {
            try
            {
               var meal =  await _context.AddAsync(newMeal);
               return meal.Entity;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Meal could not be saved", ex);
            }
        }
        public Task<Meal> UpdateAsync(Meal newMeal)
        {
            if (newMeal == null)
            {
                throw new ArgumentNullException("Meal cannot be null");
            }

            try
            {
                var updatedMeal = _context.Update(newMeal);

                return Task.FromResult(updatedMeal.Entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Meal could not be updated: {ex.Message}", ex);
            }
        }
        public async Task<Meal> GetByIdAsync(Guid id)
        {
            return await _context.Meals.Where(m => m.Id == id).Include(m => m.Ingredients).FirstOrDefaultAsync();
        }
        public async Task<ICollection<Meal>> GetAllAsync()
        {
           return await _context.Meals.ToListAsync();
        }
        public IQueryable<MealItemDTO> GetAllAsDTOs()
        {
            return _context.Meals
                    .Select( m => new MealItemDTO
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Kilocalories = m.Kilocalories
                    });
        }
        public Task<Meal> DeleteAsync(Meal meal)
        {
            try
            {
                _context.Remove(meal);
                return Task.FromResult(meal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Meal could not be deleted: {ex.Message}", ex);
            }
        }
    }
}
