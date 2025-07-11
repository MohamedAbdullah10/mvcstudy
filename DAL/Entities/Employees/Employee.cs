using DAL.Entities.Common.Enums;
using DAL.Entities.Common.Interfaces;
using DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Employees
{
    public class Employee :ModelBase,ISoftDelete
    {

        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public string ?Email { get; set; }
        public decimal Salary { get; set; }
        public string ?PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }


    }
}
