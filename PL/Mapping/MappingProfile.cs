using AutoMapper;
using BLL.Dtos.EmployeesDto;
using PL.ViewModels.Employees;

namespace PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, CreateEmployeeDto>();

            CreateMap<EmployeeDetailsToReturnDto, EmployeeViewModel>();
          
        }
    }
}

