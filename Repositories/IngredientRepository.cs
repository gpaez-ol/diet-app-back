
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
    public class IngredientRepository
    {
        protected readonly AlgoFitContext _context;
        public IngredientRepository(AlgoFitContext context)
        {
            _context = context;
        }

        public async Task<Ingredient> AddAsync(Ingredient newIngredient)
        {
            try
            {
               var ingredient =  await _context.AddAsync(newIngredient);
               return ingredient.Entity;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Ingredient could not be saved", ex);
            }
        }
        public Task<Ingredient> UpdateAsync(Ingredient newIngredient)
        {
            if (newIngredient == null)
            {
                throw new ArgumentNullException("Ingredient cannot be null");
            }

            try
            {
                var updatedUser = _context.Update(newIngredient);

                return Task.FromResult(updatedUser.Entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ingredient could not be updated: {ex.Message}", ex);
            }
        }
        public async Task<Ingredient> GetByIdAsync(Guid id)
        {
            return await _context.Ingredients.Where(ing => ing.Id == id).FirstOrDefaultAsync();
        }
        public async Task<ICollection<Ingredient>> GetAllAsync()
        {
           return await _context.Ingredients.ToListAsync();
        }
        public IQueryable<IngredientDTO> GetAllAsDTOs()
        {
            return _context.Ingredients
                    .Select( i => new IngredientDTO
                    {
                        Id = i.Id,
                        Name = i.Name
                    });
        }
        public Task<Ingredient> DeleteAsync(Ingredient ingredient)
        {
            try
            {
                _context.Remove(ingredient);
                return Task.FromResult(ingredient);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ingredient could not be updated: {ex.Message}", ex);
            }
        }
    }
}
