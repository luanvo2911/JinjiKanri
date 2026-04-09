using AutoMapper;
using JinjiKanri.Entity.Entities;
using JinjiKanri.WebAPI.Model;

namespace JinjiKanri.WebAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Employee, EmployeeModel>().ForMember(
                dest => dest.fullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
            CreateMap<Vlogin, LoginModel>().ReverseMap();
        }
    }
}
