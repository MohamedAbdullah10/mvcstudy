using BLL.Dtos.EmployeesDto;
using BLL.Services.Departments;
using BLL.Services.Employees;
using DAL.Entities.Common.Enums;
using DAL.Entities.Departments;
using DAL.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PL.ViewModels.Employees;
using System;
using System.Linq;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empserv;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IDepartmentService departmentService;
        private readonly IWebHostEnvironment _webHost;
        public EmployeeController(IEmployeeService empserv, IWebHostEnvironment webHost, ILogger<EmployeeController> logger, IDepartmentService departmentService)
        {
            _empserv = empserv;
            _webHost = webHost;
            _logger = logger;
            this.departmentService = departmentService;
        }

        // GET: Employee
        public IActionResult Index(string search)
        {
            var employees = _empserv.GetAll(search);
            ViewData["CurrentFilter"] = search;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
               
                return PartialView("partial/_EmployeeTablePartial", employees);
            }
           
            return View(employees);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            var model = new EmployeeViewModel();
            PopulateDropdowns(model); 
            return View(model);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model); // Repopulate dropdowns if validation fails
                return View(model);
            }

            try
            {
                // Map ViewModel to DTO
                var dto = new CreateEmployeeDto
                {
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Salary = model.Salary,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    Gender = model.Gender,
                    DepartmentId = model.DepartmentId,
                    EmployeeType = model.EmployeeType
                    
                };

                var result = _empserv.CreateEmployee(dto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee could not be created. Please check the values.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Employee");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
            }

            PopulateDropdowns(model);
            return View(model);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int id)
        {
            var empDto = _empserv.GET(id);
            if (empDto == null)
            {
                return NotFound();
            }

            // Map DTO to ViewModel
            var model = new EmployeeViewModel
            {
                Id = empDto.Id,
                Name = empDto.Name,
                Age = empDto.Age,
                Address = empDto.Address,
                Email = empDto.Email,
                PhoneNumber = empDto.PhoneNumber,
                Salary = empDto.Salary,
                HiringDate = empDto.HiringDate,
                IsActive = empDto.IsActive,
                Gender = empDto.Gender,
                DepartmentId = empDto.DepartmentId,
                EmployeeType = empDto.EmployeeType
            };

            PopulateDropdowns(model);
            return View(model);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(model);
                return View(model);
            }

            try
            {
                // Map ViewModel to DTO
                var dto = new UpdateEmployeeDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Salary = model.Salary,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    DepartmentId=model.DepartmentId,
                    Gender = model.Gender,
                    EmployeeType = model.EmployeeType
                };

                var result = _empserv.EditEmployee(dto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee could not be updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while editing Employee");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
            }

            PopulateDropdowns(model);
            return View(model);
        }

       
        private void PopulateDropdowns(EmployeeViewModel model)
        {
            model.Genders = Enum.GetNames(typeof(Gender))
                                .Select(g => new SelectListItem { Text = g, Value = g })
                                .ToList();

            model.EmployeeTypes = Enum.GetNames(typeof(EmployeeType))
                                      .Select(t => new SelectListItem { Text = t, Value = t })
                                      .ToList();
            model.Departemnts=departmentService.GetAll()
                .Select(d=>new SelectListItem { Text=d.Name ,Value=d.Id.ToString() })
                .ToList();
        }

      
        public IActionResult Details(int id)
        {
            // You can implement this later
            var empDto = _empserv.GET(id);
            if (empDto == null)
            {
                return NotFound();
            }
            return View(empDto);
        }

        //public IActionResult Delete(int id)
        //{
        //    // You can implement this later
        //    var empDto = _empserv.GET(id);
        //    if (empDto == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(empDto);
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _empserv.RemoveEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}