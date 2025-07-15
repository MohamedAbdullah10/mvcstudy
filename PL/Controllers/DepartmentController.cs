

#region last one
//using BLL.Dtos;
//using BLL.Dtos.DepartementsDto;
//using BLL.Services.Departments;
//using Microsoft.AspNetCore.Mvc;
//using PL.ViewModels.Departments;
//using System.Diagnostics;

//namespace PL.Controllers
//{
//    public class DepartmentController : Controller
//    {
//        private readonly IDepartmentService departmentService;
//        private readonly ILogger<DepartmentController> logger;
//        private readonly IWebHostEnvironment environment;

//        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger,
//           IWebHostEnvironment environment)
//        {
//            this.departmentService = departmentService;
//            this.logger = logger;
//            this.environment = environment;
//        }

//        public IActionResult Index()
//        {
//            var dept = departmentService.GetAll();
//            return View(dept);
//        }
//        [HttpGet]
//        public IActionResult Create()
//        {

//            return View();
//        }
//        [HttpPost]
//        public IActionResult Create(CreateDepartmentDto departmentDto)
//        {

//            if (!ModelState.IsValid)
//                return View(departmentDto);
//            var message = string.Empty;
//            try
//            {
//                var result = departmentService.CreateDepartment(departmentDto);

//                if (result > 0)
//                {
//                    TempData["Message"]= "Department Created Successfully";
//                    return RedirectToAction(nameof(Index));

//                }
//                else
//                {
//                    message = "Departmen is not Created";
//                    ModelState.AddModelError(string.Empty, message);
//                    return View(departmentDto);

//                }
//            }
//            catch (Exception ex)
//            {

//                logger.LogError(ex, ex.Message);

//                if (environment.IsDevelopment())
//                {
//                    message = ex.Message;
//                    return View(departmentDto);
//                }
//                else
//                {
//                    message = "Department is not Created";
//                    return View("Error", message);
//                }
//            }

//        }

//        public IActionResult Details(int? id)
//        {
//              if(id==null)
//                return BadRequest();

//            var dept = departmentService.Get(id.Value);
//            if (dept == null)
//                return NotFound();
//            return View(dept);
//        }
//        [HttpGet]
//        public IActionResult Edit(int? id)
//        {
//            if (id == null)
//                return BadRequest();

//            var dept = departmentService.Get(id.Value);
//            if (dept == null)
//                return NotFound();
//            ViewBag.Id = dept.Id;
//            return View(new DepartmentEditViewModel {

//                Name = dept.Name,
//                Code = dept.Code,
//                Description = dept.Description,
//                CreationDate = dept.CreationDate



//            });
//        }
//        [HttpPost]

//        public IActionResult Edit(int id,DepartmentEditViewModel departmentVM) {

//            if (!ModelState.IsValid)
//            {
//                ViewBag.Id = id;
//                return View(departmentVM);
//            }

//            try
//            {
//                var result = departmentService.UpdateDepartment(new UpdateDepartmentDto
//                {
//                    Id = id,
//                    Name = departmentVM.Name,
//                    Code = departmentVM.Code,
//                    Description = departmentVM.Description,
//                    CreationDate = departmentVM.CreationDate
//                });

//                if (result > 0)
//                    return RedirectToAction(nameof(Index));
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Department is not Updated");
//                    ViewBag.Id = id;
//                    return View(departmentVM);
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex, ex.Message);

//                if (environment.IsDevelopment())
//                {
//                    ModelState.AddModelError(string.Empty, ex.Message);
//                    ViewBag.Id = id;
//                    return View(departmentVM);
//                }

//                return View("Error", "An error occurred while updating the department.");
//            }

//        }
//        [HttpGet]
//        public IActionResult Delete(int? id)
//        {
//            if (id == null) return BadRequest();

//            var dept = departmentService.Get(id.Value);
//            if (dept == null) return NotFound();

//            return View(dept);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var result = departmentService.DeleteDepartment(id);

//            if (result)
//                return RedirectToAction(nameof(Index));

//            ModelState.AddModelError("", "Failed to delete department.");
//            var dept = departmentService.Get(id);
//            return View(dept);
//        }
//    }
//}
#endregion 
using AutoMapper;
using BLL.Dtos.DepartementsDto;
using BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;
using PL.ViewModels.Departments;

namespace PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
          
            var deptDtos = await _departmentService.GetAllAsync();
            return View(deptDtos);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return View(departmentDto);

            try
            {
                var result = await _departmentService.CreateDepartment(departmentDto);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Department Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Department could not be created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
            }
            return View(departmentDto);
        }

        public async Task<IActionResult> Details(int id)
        {
            var deptDto = await _departmentService.Get(id);
            if (deptDto == null)
                return NotFound();

            return View(deptDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var deptDto = await _departmentService.Get(id);
            if (deptDto == null)
                return NotFound();

            var viewModel = _mapper.Map<DepartmentEditViewModel>(deptDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var dto = _mapper.Map<UpdateDepartmentDto>(departmentVM);
                dto.Id = id; 

                var result = await _departmentService.UpdateDepartment(dto);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Department Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Department could not be updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating department.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
            }
            return View(departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deptDto = await _departmentService.Get(id);
            if (deptDto == null) return NotFound();

            return View(deptDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _departmentService.DeleteDepartment(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Department Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to delete department.");
            var deptDto = await _departmentService.Get(id);
            return View("Delete", deptDto);
        }
    }
}