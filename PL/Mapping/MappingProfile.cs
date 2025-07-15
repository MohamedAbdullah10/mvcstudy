using AutoMapper;
using BLL.Dtos.DepartementsDto;
using BLL.Dtos.EmployeesDto;
using DAL.Entities.Departments;
using DAL.Entities.Employees;
using PL.ViewModels.Departments;
using PL.ViewModels.Employees;

namespace PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // From Entity to DTO
            CreateMap<Employee, EmployeeToReturnDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<Employee, EmployeeDetailsToReturnDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            // From DTO to Entity
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();

            // From ViewModel to DTO and back
            CreateMap<EmployeeViewModel, CreateEmployeeDto>().ReverseMap();
            CreateMap<EmployeeViewModel, UpdateEmployeeDto>().ReverseMap();
            CreateMap<EmployeeDetailsToReturnDto, EmployeeViewModel>().ReverseMap();

            // ===================================================
            // Department Mappings (You should already have these)
            // ===================================================
            CreateMap<Department, DepartmentToReturnDto>();
            CreateMap<Department, DepartmentDetailsToReturnDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
            CreateMap<DepartmentEditViewModel, UpdateDepartmentDto>().ReverseMap();
            CreateMap<DepartmentDetailsToReturnDto, DepartmentEditViewModel>();

        }
    }
}

