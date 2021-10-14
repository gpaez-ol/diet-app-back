using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AutoMapper;

namespace AlgoFit.Data.Profiles
{
    public class BiometricProfile : Profile
    {
        public BiometricProfile()
        {
            CreateMap<Biometric, BiometricDTO>().ReverseMap();
            CreateMap<Biometric, BiometricCreateDTO>().ReverseMap() ;
        }
    }
}