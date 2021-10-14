
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
    public class DietRepository
    {
        protected readonly AlgoFitContext _context;
        public DietRepository(AlgoFitContext context)
        {
            _context = context;
        }

        public async Task<Diet> AddAsync(Diet newDiet)
        {
            try
            {
               var diet =  await _context.AddAsync(newDiet);
               return diet.Entity;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Diet could not be saved", ex);
            }
        }
        public Task<Diet> UpdateAsync(Diet newDiet)
        {
            if (newDiet == null)
            {
                throw new ArgumentNullException("Diet cannot be null");
            }

            try
            {
                var updatedDiet = _context.Update(newDiet);

                return Task.FromResult(updatedDiet.Entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Diet could not be updated: {ex.Message}", ex);
            }
        }
        public async Task<Diet> GetByIdAsync(Guid id)
        {
            return await _context.Diets.Where(d => d.Id == id)
                        .Include(d => d.Meals)
                        .ThenInclude(m => m.Meal)
                        .FirstOrDefaultAsync();
        }
        public async Task<ICollection<Diet>> GetAllAsync()
        {
           return await _context.Diets.ToListAsync();
        }
        public IQueryable<DietItemDTO> GetAllAsDTOs()
        {
            return _context.Diets
                    .Select( m => new DietItemDTO
                    {
                        Id = m.Id,
                        Name = m.Name
                    });
        }
        public Task<Diet> DeleteAsync(Diet diet)
        {
            try
            {
                _context.Remove(diet);
                return Task.FromResult(diet);
            }
            catch (Exception ex)
            {
                throw new Exception($"Diet could not be deleted: {ex.Message}", ex);
            }
        }
    }
}
