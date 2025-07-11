using BLL.Dtos.EmployeesDto;
using DAL.Entities.Common.Enums;
using DAL.Entities.Employees;
using DAL.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _emprepo;

        public EmployeeService(IGenericRepository<Employee> emprepo)
        {
            _emprepo = emprepo;
        }

        public int CreateEmployee(CreateEmployeeDto createEmployee)
        {
            // 1. التحقق من الـ Enums بأمان
            if (!Enum.TryParse<Gender>(createEmployee.Gender, true, out var gender) ||
                !Enum.TryParse<EmployeeType>(createEmployee.EmployeeType, true, out var empType))
            {
                // لو القيمة اللي جاية غلط (مثلاً "Malee" أو نص فاضي)، ارجع بـ 0
                return 0;
            }

            var emp = new Employee
            {
                Name = createEmployee.Name,
                Age = createEmployee.Age,
                Address = createEmployee.Address,
                HiringDate = createEmployee.HiringDate,
                Gender = gender, // استخدام القيمة بعد تحويلها
                EmployeeType = empType, // استخدام القيمة بعد تحويلها
                Email = createEmployee.Email,
                Salary = createEmployee.Salary,
                PhoneNumber = createEmployee.PhoneNumber,
                IsActive = createEmployee.IsActive,
                DepartmentId = createEmployee.DepartmentId,
                CreationOn = DateTime.Now,
                CreationBy = 1 
        
            };

            return _emprepo.Add(emp);
        }

        public int EditEmployee(UpdateEmployeeDto updateEmployee)
        {
            var emp = _emprepo.Get(updateEmployee.Id);

           
            if (emp is null)
                return 0;

           
            if (!Enum.TryParse<Gender>(updateEmployee.Gender, true, out var gender) ||
                !Enum.TryParse<EmployeeType>(updateEmployee.EmployeeType, true, out var empType))
            {
                return 0; 
            }

            
            emp.Name = updateEmployee.Name;
            emp.Age = updateEmployee.Age;
            emp.Address = updateEmployee.Address;
            emp.HiringDate = updateEmployee.HiringDate;
            emp.Gender = gender;
            emp.EmployeeType = empType;
            emp.Email = updateEmployee.Email;
            emp.Salary = updateEmployee.Salary;
            emp.PhoneNumber = updateEmployee.PhoneNumber;
            emp.DepartmentId = updateEmployee.DepartmentId;
            emp.IsActive = updateEmployee.IsActive;
            emp.LastModifiedOn = DateTime.Now;
            // emp.LastModifiedBy = 1; // NOTE: This should be the ID of the logged-in user

            return _emprepo.Update(emp);
        }

        public EmployeeDetailsToReturnDto? GET(int id)
        {
            var emp = _emprepo.GetAllQueryable().Include(e=>e.Department)
                .FirstOrDefault(e=>e.Id==id);

            if (emp is null)
                return null;

           
            return new EmployeeDetailsToReturnDto
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Address = emp.Address,
                HiringDate = emp.HiringDate,
                Gender = emp.Gender.ToString(), // CHANGE: تحويل الـ enum إلى string
                EmployeeType = emp.EmployeeType.ToString(), // CHANGE: تحويل الـ enum إلى string
                Email = emp.Email,
                Salary = emp.Salary,
                PhoneNumber = emp.PhoneNumber,
                IsActive = emp.IsActive,
                DepartmentId = emp.DepartmentId,
               DepartmentName=emp.Department!=null?emp.Department.Name:"N/A", 
                CreationBy = emp.CreationBy, 
                CreationOn = emp.CreationOn,
                LastModifiedBy = emp.LastModifiedBy, // FIX: عرض القيمة الحقيقية
                LastModifiedOn = emp.LastModifiedOn

            };
        }

        public IEnumerable<EmployeeToReturnDto> GetAll(string search)
        {
            return _emprepo.GetAllQueryable().Where(s=> string.IsNullOrEmpty(search)||s.Name.ToLower().Contains(search.ToLower())).Include(e=>e.Department)
                           .Select(e => new EmployeeToReturnDto
                           {
                               Id = e.Id,
                               Name = e.Name,
                               Age = e.Age,
                               Address = e.Address,
                               HiringDate = e.HiringDate,
                               Gender = e.Gender.ToString(), 
                               EmployeeType = e.EmployeeType.ToString(), 
                               Email = e.Email,
                               DepartmentId = e.DepartmentId,
                               DepartmentName = e.Department != null ? e.Department.Name : "N/A",
                               Salary = e.Salary,
                               PhoneNumber = e.PhoneNumber,
                               IsActive = e.IsActive
                           }).ToList();
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