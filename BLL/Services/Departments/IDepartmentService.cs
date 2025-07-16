using BLL.Dtos.DepartementsDto;
using DAL.Entities.Departments;

namespace BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentToReturnDto>> GetAllAsync();
        //IEnumerable<DepartmentToReturnDto> GetAll();
        Task<DepartmentDetailsToReturnDto?> GetAsync(int Id);
        Task<int> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);
        Task<int> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}