using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AutoMapper;

namespace AlgoFit.Data.Profiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<Meal, MealDTO>().ReverseMap();
            CreateMap<Meal, MealCreateDTO>().ReverseMap();
        }
    }
}