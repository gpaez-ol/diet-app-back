
using System;
using System.Linq;
using System.Threading.Tasks;
using AlgoFit.Data.Context;
using AlgoFit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoFit.Repositories
{
    public class UserRepository
    {
        protected readonly AlgoFitContext _context;
        public UserRepository(AlgoFitContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User newUser)
        {
            try
            {
               var user=  await _context.AddAsync(newUser);
               return user.Entity;
            }
            catch (Exception ex)
            {
                
                throw new Exception("User could not be saved", ex);
            }
        }
        public async Task<User> UpdateAsync(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }

            try
            {
                var updatedUser = _context.Update(newUser);

                return await Task.FromResult(updatedUser.Entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"User could not be updated: {ex.Message}", ex);
            }
        }
        public async Task<UserCredential> GetCredentialsByUsernameAsync(string email)
        {
            var user = await _context.Users.Include(user => user.UserCredential).Where(user => user.Email == email).FirstOrDefaultAsync();
            return user != null ? user.UserCredential : null;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return (await GetAllAsync()).Include(u => u.Diet)
            .ThenInclude(d => d.Meals)
            .ThenInclude(dm => dm.Meal)
            .ThenInclude(m => m.Ingredients)
            .ThenInclude(i => i.Ingredient
            ).FirstOrDefault(x => x.Id == id);
        }
        public async Task<IQueryable<User>> GetAllAsync()
        {
            try
            {
                return await Task.FromResult(_context.Set<User>().AsQueryable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
    }
}
