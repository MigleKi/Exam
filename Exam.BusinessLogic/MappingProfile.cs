using AutoMapper;
using Exam.Database.Models;
using Exam.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<Person, PersonGetDTO>().ReverseMap();
            CreateMap<Person, PersonUpdateDTO>().ReverseMap();
            CreateMap<Address, AddressCreateDTO>().ReverseMap();
        }
    }
}
