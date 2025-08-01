﻿#region Old Code
//using BLL.Dtos.EmployeesDto;
//using DAL.Entities.Common.Enums;
//using DAL.Entities.Employees;
//using DAL.Persistance.Repositories;
//using DAL.Persistance.UnitOfWork;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace BLL.Services.Employees
//{
//    public class EmployeeService : IEmployeeService
//    {
//        //private readonly IGenericRepository<Employee> _emprepo;
//        private readonly IUnitOfWork unitOfWork;

//        public EmployeeService(/*IGenericRepository<Employee> emprepo*/IUnitOfWork unitOfWork)
//        {
//            this.unitOfWork = unitOfWork;
//            //_emprepo = emprepo;
//        }

//        public int CreateEmployee(CreateEmployeeDto createEmployee)
//        {
//            // 1. التحقق من الـ Enums بأمان
//            if (!Enum.TryParse<Gender>(createEmployee.Gender, true, out var gender) ||
//                !Enum.TryParse<EmployeeType>(createEmployee.EmployeeType, true, out var empType))
//            {
//                // لو القيمة اللي جاية غلط (مثلاً "Malee" أو نص فاضي)، ارجع بـ 0
//                return 0;
//            }

//            var emp = new Employee
//            {
//                Name = createEmployee.Name,
//                Age = createEmployee.Age,
//                Address = createEmployee.Address,
//                HiringDate = createEmployee.HiringDate,
//                Gender = gender, // استخدام القيمة بعد تحويلها
//                EmployeeType = empType, // استخدام القيمة بعد تحويلها
//                Email = createEmployee.Email,
//                Salary = createEmployee.Salary,
//                PhoneNumber = createEmployee.PhoneNumber,
//                IsActive = createEmployee.IsActive,
//                DepartmentId = createEmployee.DepartmentId,
//                CreationOn = DateTime.Now,
//                CreationBy = 1 

//            };

//           unitOfWork.Repository<Employee>().Add(emp);


//            return unitOfWork.Complete();
//        }

//        public int EditEmployee(UpdateEmployeeDto updateEmployee)
//        {
//            var _emprepo = unitOfWork.Repository<Employee>();
//            var emp = _emprepo.Get(updateEmployee.Id);


//            if (emp is null)
//                return 0;


//            if (!Enum.TryParse<Gender>(updateEmployee.Gender, true, out var gender) ||
//                !Enum.TryParse<EmployeeType>(updateEmployee.EmployeeType, true, out var empType))
//            {
//                return 0; 
//            }


//            emp.Name = updateEmployee.Name;
//            emp.Age = updateEmployee.Age;
//            emp.Address = updateEmployee.Address;
//            emp.HiringDate = updateEmployee.HiringDate;
//            emp.Gender = gender;
//            emp.EmployeeType = empType;
//            emp.Email = updateEmployee.Email;
//            emp.Salary = updateEmployee.Salary;
//            emp.PhoneNumber = updateEmployee.PhoneNumber;
//            emp.DepartmentId = updateEmployee.DepartmentId;
//            emp.IsActive = updateEmployee.IsActive;
//            emp.LastModifiedOn = DateTime.Now;
//            // emp.LastModifiedBy = 1; // NOTE: This should be the ID of the logged-in user

//             _emprepo.Update(emp);
//            return unitOfWork.Complete();
//        }

//        public EmployeeDetailsToReturnDto? GET(int id)
//        {
//            var _emprepo = unitOfWork.Repository<Employee>();
//            var emp = _emprepo.GetAllQueryable().Include(e=>e.Department)
//                .FirstOrDefault(e=>e.Id==id);

//            if (emp is null)
//                return null;


//            return new EmployeeDetailsToReturnDto
//            {
//                Id = emp.Id,
//                Name = emp.Name,
//                Age = emp.Age,
//                Address = emp.Address,
//                HiringDate = emp.HiringDate,
//                Gender = emp.Gender.ToString(), // CHANGE: تحويل الـ enum إلى string
//                EmployeeType = emp.EmployeeType.ToString(), // CHANGE: تحويل الـ enum إلى string
//                Email = emp.Email,
//                Salary = emp.Salary,
//                PhoneNumber = emp.PhoneNumber,
//                IsActive = emp.IsActive,
//                DepartmentId = emp.DepartmentId,
//               DepartmentName=emp.Department!=null?emp.Department.Name:"N/A", 
//                CreationBy = emp.CreationBy, 
//                CreationOn = emp.CreationOn,
//                LastModifiedBy = emp.LastModifiedBy, // FIX: عرض القيمة الحقيقية
//                LastModifiedOn = emp.LastModifiedOn

//            };
//        }

//        public IEnumerable<EmployeeToReturnDto> GetAll(string search)
//        {
//            var _emprepo = unitOfWork.Repository<Employee>();
//            return _emprepo.GetAllQueryable().Where(s=> string.IsNullOrEmpty(search)||s.Name.ToLower().Contains(search.ToLower())).Include(e=>e.Department)
//                           .Select(e => new EmployeeToReturnDto
//                           {
//                               Id = e.Id,
//                               Name = e.Name,
//                               Age = e.Age,
//                               Address = e.Address,
//                               HiringDate = e.HiringDate,
//                               Gender = e.Gender.ToString(), 
//                               EmployeeType = e.EmployeeType.ToString(), 
//                               Email = e.Email,
//                               DepartmentId = e.DepartmentId,
//                               DepartmentName = e.Department != null ? e.Department.Name : "N/A",
//                               Salary = e.Salary,
//                               PhoneNumber = e.PhoneNumber,
//                               IsActive = e.IsActive
//                           }).ToList();
//        }

//        public int RemoveEmployee(int id)
//        {
//            var _emprepo = unitOfWork.Repository<Employee>();
//            var emp = _emprepo.Get(id);

//            if (emp is null)
//                return 0;

//             _emprepo.Delete(emp);

//            return unitOfWork.Complete();
//        }
//    }
//}
#endregion
using AutoMapper;
using BLL.Dtos.EmployeesDto;
using BLL.Services.Employees;
using DAL.Entities.Employees;
using DAL.Persistance.Repositories;
using DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Employee> _employeeRepository;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _employeeRepository = _unitOfWork.Repository<Employee>();
    }

    public async Task<int> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
    {
        var emp = _mapper.Map<Employee>(createEmployeeDto);
        _employeeRepository.Add(emp);
        return await _unitOfWork.CompleteAsync();
    }

    public async Task<int> EditEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
    {
        var emp = await _employeeRepository.Get(updateEmployeeDto.Id);
        if (emp == null) return 0;

        _mapper.Map(updateEmployeeDto, emp);
        _employeeRepository.Update(emp);
        return await _unitOfWork.CompleteAsync();
    }

    public async Task<EmployeeDetailsToReturnDto?> GetAsync(int id)
    {
        var emp = await _employeeRepository.GetAllQueryable()
                                           .Include(e => e.Department)
                                           .FirstOrDefaultAsync(e => e.Id == id);
        return _mapper.Map<EmployeeDetailsToReturnDto>(emp);
    }

    public async Task<IEnumerable<EmployeeToReturnDto>> GetAllAsync(string search)
    {
        var query = _employeeRepository.GetAllQueryable()
            .Where(s => string.IsNullOrEmpty(search) || s.Name.ToLower().Contains(search.ToLower()));

        return await _mapper.ProjectTo<EmployeeToReturnDto>(query).ToListAsync();
    }

    public async Task<int> RemoveEmployeeAsync(int id)
    {
        var emp = await _employeeRepository.Get(id);
        if (emp == null) return 0;

        _employeeRepository.Delete(emp);
        return await _unitOfWork.CompleteAsync();
    }
}