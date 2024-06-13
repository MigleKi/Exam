using AutoMapper;
using Exam.Database.Models;
using Exam.Shared.DTOs;


namespace Exam.BusinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, UserDeleteDTO>().ReverseMap();
            CreateMap<User, UserGetAllDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();

            CreateMap<Person, PersonCreateDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ReverseMap();

            CreateMap<Person, PersonDeleteDTO>().ReverseMap();
            CreateMap<Person, PersonGetDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();
            CreateMap<Person, PersonUpdateDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ReverseMap();
            CreateMap<Address, AddressCreateDTO>().ReverseMap();
            CreateMap<Address, AddressUpdateDTO>().ReverseMap();
            CreateMap<Address, AddressGetDTO>().ReverseMap();
        }
    }
}
