using DAL.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.EmployeesDto
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public decimal Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }

        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;
        public int? DepartmentId { get; set; }
    }
}
