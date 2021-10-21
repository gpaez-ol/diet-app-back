using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AutoMapper;

namespace AlgoFit.Data.Categories
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
        }
    }
}