using BLL.Dtos.DepartementsDto;
using DAL.Entities.Departments;

namespace BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAll();
        DepartmentDetailsToReturnDto? Get(int Id);
        int CreateDepartment(CreateDepartmentDto createDepartmentDto);
        int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);
        bool DeleteDepartment(int id);
    }
}