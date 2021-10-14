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
namespace NFact.WebAPI.Logic
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

        public IPaginationResult<DietItemDTO> GetDiets(PaginationDataParams pagination)
        {
            var query = _repositoryManager.DietRepository.GetAllAsDTOs();
            return query.ToPagination(pagination.Page, pagination.PageSize);
        }

        public async Task CreateDiet(DietCreateDTO newDiet)
        {
            try
            {
                var diet = new Diet
                    {
                        Name = newDiet.Name,
                        Description = newDiet.Description,
                        Type = newDiet.Type
                    };
                await _repositoryManager.DietRepository.AddAsync(diet);
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
    }
}