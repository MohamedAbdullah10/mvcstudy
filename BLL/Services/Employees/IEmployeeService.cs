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
        IEnumerable<EmployeeToReturnDto> GetAll();
        EmployeeDetailsToReturnDto ?GET(int id);
        int CreateEmployee(CreateEmployeeDto createEmployee);
        int EditeEmployee(UpdateEmployeeDto updateEmployee);
        int RemoveEmployee(int id);
    }
}
