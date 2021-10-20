
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
    public class BiometricRepository
    {
        protected readonly AlgoFitContext _context;
        public BiometricRepository(AlgoFitContext context)
        {
            _context = context;
        }

        public async Task<Biometric> AddAsync(Biometric newBiometric)
        {
            try
            {
               var biometric =  await _context.AddAsync(newBiometric);
               return biometric.Entity;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Biometric could not be saved", ex);
            }
        }
        public Task<Biometric> UpdateAsync(Biometric newBiometric)
        {
            if (newBiometric == null)
            {
                throw new ArgumentNullException("Biometric cannot be null");
            }

            try
            {
                var updatedBiometric = _context.Update(newBiometric);

                return Task.FromResult(updatedBiometric.Entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Biometric could not be updated: {ex.Message}", ex);
            }
        }
        public async Task<Biometric> GetByIdAsync(Guid id)
        {
            return await _context.Biometrics.Where(b => b.Id == id)
                        .Include(b => b.User)
                        .FirstOrDefaultAsync();
        }
        public async Task<ICollection<Biometric>> GetAllAsync()
        {
           return await _context.Biometrics.ToListAsync();
        }
        public IQueryable<BiometricItemDTO> GetAllAsDTOs(Guid userId)
        {
            return _context.Biometrics
                    .Where(b => b.UserId == userId)
                    .Select( b => new BiometricItemDTO
                    {
                        Id = b.Id,
                        BodyMassIndex = b.Weight/b.Height,
                        Date = b.Date.ToShortDateString()
                    });
        }
        public Task<Biometric> DeleteAsync(Biometric biometric)
        {
            try
            {
                _context.Remove(biometric);
                return Task.FromResult(biometric);
            }
            catch (Exception ex)
            {
                throw new Exception($"Biometric could not be deleted: {ex.Message}", ex);
            }
        }
    }
}
