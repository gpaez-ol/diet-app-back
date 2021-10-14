using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AutoMapper;

namespace AlgoFit.Data.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDTO>().ReverseMap();
            CreateMap<Ingredient, IngredientCreateDTO>().ReverseMap();
        }
    }
}