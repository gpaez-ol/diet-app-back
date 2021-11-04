using AlgoFit.Data.DTO;
using AlgoFit.Data.Models;
using AlgoFit.Utils.Enums;
using AutoMapper;

namespace AlgoFit.Data.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, ProfileDTO>()
                .ForMember(profile => profile.Type, opt => opt.MapFrom(user => EnumHelper.GetEnumText(user.Type)));
            CreateMap<SignUpDTO, User>()
                .ForMember(user => user.UserCredential,
                    opt => opt.MapFrom(signUp => new UserCredential
                    {
                        Password = signUp.Password
                    })
                );
        }
    }
}