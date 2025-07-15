#region old code    
//using AutoMapper;
//using BLL.Dtos.EmployeesDto;
//using BLL.Services.Departments;
//using BLL.Services.Employees;
//using DAL.Entities.Common.Enums;
//using DAL.Entities.Departments;
//using DAL.Persistance.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using PL.ViewModels.Employees;
//using System;
//using System.Linq;

//namespace PL.Controllers
//{
//    public class EmployeeController : Controller
//    {
//        private readonly IEmployeeService _empserv;
//        private readonly ILogger<EmployeeController> _logger;
//        private readonly IDepartmentService departmentService;
//        private readonly IWebHostEnvironment _webHost;
//        private readonly IMapper _mapper;
//        public EmployeeController(IEmployeeService empserv, IWebHostEnvironment webHost, ILogger<EmployeeController> logger, IDepartmentService departmentService, IMapper mapper)
//        {
//            _empserv = empserv;
//            _webHost = webHost;
//            _logger = logger;
//            this.departmentService = departmentService;
//            _mapper = mapper;
//        }

//        // GET: Employee
//        public IActionResult Index(string search)
//        {
//            var employees = _empserv.GetAll(search);
//            ViewData["CurrentFilter"] = search;

//            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
//            {

//                return PartialView("partial/_EmployeeTablePartial", employees);
//            }

//            return View(employees);
//        }

//        // GET: Employee/Create
//        public IActionResult Create()
//        {
//            var model = new EmployeeViewModel();
//            PopulateDropdowns(model); 
//            return View(model);
//        }

//        // POST: Employee/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(EmployeeViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                PopulateDropdowns(model); // Repopulate dropdowns if validation fails
//                return View(model);
//            }

//            try
//            {
//                // Map ViewModel to DTO
//                var dto = _mapper.Map<CreateEmployeeDto>(model);


//                var result = _empserv.CreateEmployee(dto);

//                if (result > 0)
//                    return RedirectToAction(nameof(Index));

//                ModelState.AddModelError(string.Empty, "Employee could not be created. Please check the values.");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error while creating Employee");
//                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
//            }

//            PopulateDropdowns(model);
//            return View(model);
//        }

//        // GET: Employee/Edit/5
//        public IActionResult Edit(int id)
//        {
//            var empDto = _empserv.GET(id);
//            if (empDto == null)
//            {
//                return NotFound();
//            }

//            // Map DTO to ViewModel
//            var model =_mapper.Map<EmployeeViewModel>(empDto);


//            PopulateDropdowns(model);
//            return View(model);
//        }

//        // POST: Employee/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, EmployeeViewModel model)
//        {
//            if (id != model.Id)
//            {
//                return BadRequest();
//            }

//            if (!ModelState.IsValid)
//            {
//                PopulateDropdowns(model);
//                return View(model);
//            }

//            try
//            {
//                // Map ViewModel to DTO
//                var dto = new UpdateEmployeeDto
//                {
//                    Id = model.Id,
//                    Name = model.Name,
//                    Age = model.Age,
//                    Address = model.Address,
//                    Email = model.Email,
//                    PhoneNumber = model.PhoneNumber,
//                    Salary = model.Salary,
//                    HiringDate = model.HiringDate,
//                    IsActive = model.IsActive,
//                    DepartmentId=model.DepartmentId,
//                    Gender = model.Gender,
//                    EmployeeType = model.EmployeeType
//                };

//                var result = _empserv.EditEmployee(dto);
//                if (result > 0)
//                    return RedirectToAction(nameof(Index));

//                ModelState.AddModelError(string.Empty, "Employee could not be updated.");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error while editing Employee");
//                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
//            }

//            PopulateDropdowns(model);
//            return View(model);
//        }


//        private void PopulateDropdowns(EmployeeViewModel model)
//        {
//            model.Genders = Enum.GetNames(typeof(Gender))
//                                .Select(g => new SelectListItem { Text = g, Value = g })
//                                .ToList();

//            model.EmployeeTypes = Enum.GetNames(typeof(EmployeeType))
//                                      .Select(t => new SelectListItem { Text = t, Value = t })
//                                      .ToList();
//            model.Departemnts=departmentService.GetAll()
//                .Select(d=>new SelectListItem { Text=d.Name ,Value=d.Id.ToString() })
//                .ToList();
//        }


//        public IActionResult Details(int id)
//        {
//            // You can implement this later
//            var empDto = _empserv.GET(id);
//            if (empDto == null)
//            {
//                return NotFound();
//            }
//            return View(empDto);
//        }

//        //public IActionResult Delete(int id)
//        //{
//        //    // You can implement this later
//        //    var empDto = _empserv.GET(id);
//        //    if (empDto == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return View(empDto);
//        //}

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            _empserv.RemoveEmployee(id);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
#endregion

using AutoMapper;
using BLL.Dtos.EmployeesDto;
using BLL.Services.Departments;
using BLL.Services.Employees;
using DAL.Entities.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PL.ViewModels.Employees;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empserv;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService empserv, IDepartmentService departmentService, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _empserv = empserv;
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string search)
        {
            ViewData["CurrentFilter"] = search;
            var employees = await _empserv.GetAllAsync(search);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("partial/_EmployeeTablePartial", employees);
            }
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new EmployeeViewModel();
            await PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(model);
                return View(model);
            }

            try
            {
                var dto = _mapper.Map<CreateEmployeeDto>(model);
                var result = await _empserv.CreateEmployeeAsync(dto);

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Employee Created Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Employee could not be created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Employee");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
            }

            await PopulateDropdowns(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var empDto = await _empserv.GetAsync(id);
            if (empDto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EmployeeViewModel>(empDto);
            await PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(model);
                return View(model);
            }

            try
            {
                var dto = _mapper.Map<UpdateEmployeeDto>(model);
                var result = await _empserv.EditEmployeeAsync(dto);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Employee Updated Successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Employee could not be updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing Employee");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
            }

            await PopulateDropdowns(model);
            return View(model);
        }

        private async Task PopulateDropdowns(EmployeeViewModel model)
        {
            model.Genders = Enum.GetNames(typeof(Gender))
                                .Select(g => new SelectListItem { Text = g, Value = g }).ToList();

            model.EmployeeTypes = Enum.GetNames(typeof(EmployeeType))
                                .Select(t => new SelectListItem { Text = t, Value = t }).ToList();

            // This call must now be awaited
            var departments = await _departmentService.GetAllAsync();
            model.Departemnts = departments.Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }).ToList();
        }

        public async Task<IActionResult> Details(int id)
        {
            var empDto = await _empserv.GetAsync(id);
            if (empDto == null)
            {
                return NotFound();
            }
            return View(empDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _empserv.RemoveEmployeeAsync(id);
            if (result > 0)
            {
                return Ok(); // Return HTTP 200 OK for successful AJAX call
            }
            return BadRequest(); // Return error if delete failed
        }
    }
}