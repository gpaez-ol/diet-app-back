using System.Linq;
using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AutoMapper;

namespace AlgoFit.Data.Profiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<Meal, MealDTO>()
            .ForMember(dto => dto.MealIngredients,
                opt => opt.MapFrom(diet => diet.Ingredients
                .Select(meal => new MealIngredientDetailDTO
                {
                    IngredientId = meal.IngredientId,
                    Name =  meal.Ingredient.Name,
                    Amount = meal.Amount,
                    Notes = meal.Notes,
                }
                ))).ReverseMap();
            CreateMap<Meal, MealCreateDTO>().ReverseMap();
        }
    }
}