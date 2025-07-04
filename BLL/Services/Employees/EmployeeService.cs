using BLL.Dtos.EmployeesDto;
using DAL.Entities.Common.Enums;
using DAL.Entities.Employees;
using DAL.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Employees
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IGenericRepository<Employee> _emprepo;

        public EmployeeService(IGenericRepository<Employee> emprepo)
        {
            _emprepo = emprepo;
        }

        public int CreateEmployee(CreateEmployeeDto createEmployee)
        {
            if (!Enum.TryParse<Gender>(createEmployee.Gender,out var gender))
                return 0;
            if (!Enum.TryParse<EmployeeType>(createEmployee.EmployeeType, out var emptype))
                return 0;
            var emp = new Employee
            {
                Name = createEmployee.Name,
                Age = createEmployee.Age,
                Address = createEmployee.Address,
                HiringDate = createEmployee.HiringDate,
                Gender = gender,
                EmployeeType = emptype,
                Email = createEmployee.Email,
                Salary = createEmployee.Salary,
                PhoneNumber = createEmployee.PhoneNumber,
                IsActive = createEmployee.IsActive,
                CreationBy = 1,
                CreationOn = DateTime.Now
              






            };
            if(emp is { })
              return _emprepo.Add(emp);

            return 0;
        }

        public int EditeEmployee(UpdateEmployeeDto updateEmployee)
        {
            var emp = _emprepo.Get(updateEmployee.Id);
            if (emp is { }) {
                emp.Name = updateEmployee.Name;
                emp.Salary = updateEmployee.Salary;
                emp.Address = updateEmployee.Address;
                emp.Email = updateEmployee.Email;
                emp.Age = updateEmployee.Age;
                emp.Gender = Enum.Parse<Gender>(updateEmployee.Gender);
                emp.EmployeeType = Enum.Parse<EmployeeType>(updateEmployee.EmployeeType);
                emp.IsActive = updateEmployee.IsActive;
                emp.HiringDate = updateEmployee.HiringDate;
                emp.PhoneNumber = updateEmployee.PhoneNumber;
                return _emprepo.Update(emp);
            
            }
            return 0;
        }

        public EmployeeDetailsToReturnDto?GET(int id)
        {
            var emp = _emprepo.Get(id);
            if (emp is { }) {

                return new EmployeeDetailsToReturnDto
                {
                    Name = emp.Name,
                    Age = emp.Age,
                    Address = emp.Address,
                    HiringDate = emp.HiringDate,
                    Gender = emp.Gender.ToString(),
                    EmployeeType =emp.EmployeeType.ToString(),
                    Email = emp.Email,
                    Salary = emp.Salary,
                    PhoneNumber = emp.PhoneNumber,
                    IsActive = emp.IsActive,
                    CreationBy=1,
                    CreationOn=emp.CreationOn,
                    Id=emp.Id,
                    LastModifiedBy = emp.LastModifiedBy
                    
                    
                    



                };
            
            
            }
            return null;
           
        }

        public IEnumerable<EmployeeToReturnDto> GetAll()
        {
            var emp = _emprepo.GetAllQueryable().Select(e => new EmployeeToReturnDto
            {
                Name = e.Name,
                Address = e.Address,
                Age = e.Age,
                Email = e.Email,
                Salary = e.Salary,
                IsActive = e.IsActive,
                Gender = e.Gender.ToString(),
                EmployeeType = e.EmployeeType.ToString(),
                HiringDate = e.HiringDate,
                PhoneNumber = e.PhoneNumber




            }


            ).AsNoTracking();

            return emp.ToList();
        }

        public int RemoveEmployee(int id)
        {
            var emp = _emprepo.Get(id);
            if (emp is null)
                return 0;
            return _emprepo.Delete(emp);
        }
    }
}
