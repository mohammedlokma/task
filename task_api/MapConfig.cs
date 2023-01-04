using AutoMapper;
using task_api.Models;
using task_api.Models.Dtos;

namespace task_api
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {

            CreateMap<Employee,EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();
            CreateMap<Departement, DepartementDTO>().ReverseMap();

        }

    }
}
