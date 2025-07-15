using BLL.Dtos.DepartementsDto;
using DAL.Entities.Departments;

namespace BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentToReturnDto>> GetAllAsync();
        //IEnumerable<DepartmentToReturnDto> GetAll();
        Task<DepartmentDetailsToReturnDto>? Get(int Id);
        Task<int> CreateDepartment(CreateDepartmentDto createDepartmentDto);
        Task<int> UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);
        Task<bool> DeleteDepartment(int id);
    }
}