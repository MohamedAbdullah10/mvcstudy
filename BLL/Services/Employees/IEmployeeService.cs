using BLL.Dtos.EmployeesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeToReturnDto>> GetAllAsync(string search);
        Task<EmployeeDetailsToReturnDto?> GetAsync(int id);
        Task<int> CreateEmployeeAsync(CreateEmployeeDto createEmployee);
        Task<int> EditEmployeeAsync(UpdateEmployeeDto updateEmployee);
        Task<int> RemoveEmployeeAsync(int id);
    }
}
