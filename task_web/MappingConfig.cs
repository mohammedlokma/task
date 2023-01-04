using AutoMapper;
using task_web.Models.DTO;

namespace MagicVilla_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<EmployeeDto, EmployeeUpdateDto>().ReverseMap();
        }
    }
}
