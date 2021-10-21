
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
    public class CategoryRepository
    {
        protected readonly AlgoFitContext _context;
        public CategoryRepository(AlgoFitContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category newCategory)
        {
            try
            {
               var category =  await _context.AddAsync(newCategory);
               return category.Entity;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Category could not be saved", ex);
            }
        }
        public Task<Category> UpdateAsync(Category newCategory)
        {
            if (newCategory == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            try
            {
                var updatedCategory = _context.Update(newCategory);

                return Task.FromResult(updatedCategory.Entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Category could not be updated: {ex.Message}", ex);
            }
        }
        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Categories.Where(ing => ing.Id == id).FirstOrDefaultAsync();
        }
        public async Task<ICollection<Category>> GetAllAsync()
        {
           return await _context.Categories.ToListAsync();
        }
        public IQueryable<CategoryDTO> GetAllAsDTOs()
        {
            return _context.Categories
                    .Select( c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description
                    });
        }
        public Task<Category> DeleteAsync(Category category)
        {
            try
            {
                _context.Remove(category);
                return Task.FromResult(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"Category could not be updated: {ex.Message}", ex);
            }
        }
    }
}
