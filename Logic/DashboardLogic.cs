using System;
using System.Linq;
using System.Threading.Tasks;
using AlgoFit.Data.DTO;
using AlgoFit.Repositories.Manager;
using AutoMapper;
namespace AlgoFit.WebAPI.Logic
{
    public class DashboardLogic
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public DashboardLogic(RepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<DashboardDTO>  GetDashboard(Guid userId)
        {
            var user = await _repositoryManager.UserRepository.GetUserByIdAsync(userId);
            var eatenCalories =  0.0;
            if(user.Diet != null)
            {
                var passedDays = (DateTime.Today - user.DietStartedAt.GetValueOrDefault()).Days;
;                eatenCalories = user.Diet.Meals.OrderBy(dm => dm.MealNumber).Take(passedDays*3).Sum(dm => dm.Meal.Kilocalories);
            }
            var biometrics = await _repositoryManager.BiometricRepository.GetAllByUserIdAsync(userId);
            var latestBiometric = biometrics.OrderByDescending(b => b.Date)
                            .Select(b => 
                            new BiometricDashboardDTO{
                                Weight = b.Weight,
                                Height = b.Height,
                                CaloriesConsumed = eatenCalories,
                                FatIndex = b.FatIndex
                            } ).FirstOrDefault();
            var biometricHistory= biometrics.Select( b => new SimpleBiometricDTO {
                Weight = b.Weight,
                Height = b.Height, 
                Date = b.Date
                 }).ToList();
            var dashboardDTO = new DashboardDTO{
                        BiometricHistory = biometricHistory,
                        LatestBiometrics = latestBiometric
                        };
            return dashboardDTO;
        }

    }
}