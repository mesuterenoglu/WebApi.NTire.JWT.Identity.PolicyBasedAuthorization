using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();

            CreateMap<AppRole, AppRoleDto>().ReverseMap();

            CreateMap<Company, CompanyDto>().ReverseMap();

            CreateMap<CompanyUser, CompanyUserDto>().ReverseMap();
        }
    }
}
