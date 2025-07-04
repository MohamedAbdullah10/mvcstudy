using BLL.Dtos.EmployeesDto;
using BLL.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empserv;
        private readonly IWebHostEnvironment webHost;
        private readonly ILogger<EmployeeController> logger;


        public EmployeeController(IEmployeeService empserv, IWebHostEnvironment webHost, ILogger<EmployeeController> logger)
        {
            _empserv = empserv;
            this.webHost = webHost;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var emp = _empserv.GetAll();

            return View(emp);
        }
        public IActionResult Create() {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto emp) {


            try
            {
                if (!ModelState.IsValid)
                    return View(emp);
                var e = _empserv.CreateEmployee(emp);
                if (e > 0)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError(string.Empty, "Employee not Created");
                return View(emp);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while Creation Employee");
                if (webHost.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error");
                
            }

            return View(emp);
        }
    }
}
