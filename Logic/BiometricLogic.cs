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
namespace AlgoFit.WebAPI.Logic
{
    public class BiometricLogic
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public BiometricLogic(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IPaginationResult<BiometricItemDTO> GetBiometrics(PaginationDataParams pagination,Guid userId)
        {
            var query = _repositoryManager.BiometricRepository.GetAllAsDTOs(userId);
            return query.ToPagination(pagination.Page, pagination.PageSize);
        }

        public async Task CreateBiometric(BiometricCreateDTO newBiometric,Guid userId)
        {
            var date = DateTime.Now;
            try
            {
                var biometric= new Biometric
                    {
                        Weight = newBiometric.Weight,
                        Height = newBiometric.Height,
                        Date = date,
                        UserId= userId
                    };
                await _repositoryManager.BiometricRepository.AddAsync(biometric);
                _repositoryManager.Save();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<BiometricDTO> UpdateBiometric(BiometricCreateDTO newBiometric,Guid biometricId)
        {
            try
            {
                var oldBiometric = await _repositoryManager.BiometricRepository.GetByIdAsync(biometricId);
                if (oldBiometric != null)
                {
                    _mapper.Map(newBiometric, oldBiometric);
                    Biometric updatedBiometric = await _repositoryManager.BiometricRepository.UpdateAsync(oldBiometric);
                    _repositoryManager.Save();
                    return _mapper.Map<BiometricDTO>(updatedBiometric);
                }
                throw new AlgoFitError(404, "Biometric Not Found");;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task DeleteBiometric(Guid biometricId)
        {
            var biometric = await _repositoryManager.BiometricRepository.GetByIdAsync(biometricId);
            if (biometric == null)
            {
                throw new AlgoFitError(404, "Biometric Not Found");
            }
            await _repositoryManager.BiometricRepository.DeleteAsync(biometric);
            _repositoryManager.Save();
        }
        public async Task<BiometricDTO> GetBiometricById(Guid id)
        {
            var biometric = await _repositoryManager.BiometricRepository.GetByIdAsync(id);
            if (biometric == null)
            {
                throw new AlgoFitError(404, "Biometric Not Found");
            }
            var biometricDTO = _mapper.Map<BiometricDTO>(biometric);
            return biometricDTO;
        }
    }
}