using System.Linq;
using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AutoMapper;

namespace AlgoFit.Data.Profiles
{
    public class DietProfile : Profile
    {
        public DietProfile()
        {
            CreateMap<Diet, DietDTO>()
            .ForMember(dto => dto.Meals,
                opt => opt.MapFrom(diet => diet.Meals
                .Where(meal => meal.MealId != null && meal.Meal != null)
                .Select(meal => new MealItemDTO
                {
                    Id = meal.MealId.GetValueOrDefault(),
                    Name = meal.Meal.Name,
                    Kilocalories = meal.Meal.Kilocalories
                }
                ))).ReverseMap();
            CreateMap<Diet, DietCreateDTO>().ReverseMap();
        }
    }
}