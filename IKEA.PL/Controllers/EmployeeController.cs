using IKEA.BLL.DTOs.Departments;
using IKEA.BLL.DTOs.Employees;
using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;
using IKEA.PL.ViewModel;
using IKEA.DAL.Models.Employees;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeService employeeService,ILogger<EmployeeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Employees=_employeeService.GetAll();

            return View(Employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var Message = string.Empty;


            try
            {
                var empDTO=new CreatedEmployeeDTO()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    HiringDate = employee.HiringDate,
                    salary = employee.salary,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    EmployeeType = employee.EmployeeType,
                    IsActive = employee.IsActive,
                };
                var Result = _employeeService.CreateEmployee(empDTO);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));

                else               
                    Message = "Department Is Not Created";
                   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_webHostEnvironment.IsDevelopment())
                    Message = ex.Message;              
                else
                    Message = "Error While Creation";
            }

            ModelState.AddModelError(string.Empty, Message);
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = _employeeService.GetByEmployeeId(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = _employeeService.GetByEmployeeId(id.Value);

            if (employee == null)
                return NotFound();

            //add this to return object of type UpdatedDepartmentDTO instead of DepartmentDetailsDTO
            var MappedEmployee = new EmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                HiringDate = employee.HiringDate,
                salary = employee.salary,
                Gender = employee.Gender,
                Email = employee.Email,
                EmployeeType = employee.EmployeeType,
                IsActive = employee.IsActive,
                PhoneNUmber=employee.PhoneNUmber,

            };
            return View(MappedEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var Message = string.Empty;

            try
            {
                var empDTO = new UpdatedEmployeeDTO()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    HiringDate = employee.HiringDate,
                    salary = employee.salary,
                    Gender = employee.Gender,
                    Email = employee.Email,
                    EmployeeType = employee.EmployeeType,
                    IsActive = employee.IsActive,
                };
                var Result = _employeeService.UpdateEmployee(empDTO);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Employee Is Not Updated";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_webHostEnvironment.IsDevelopment())
                    Message = ex.Message;
                  
                else
                
                    Message = "Error While Update";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(employee);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = _employeeService.GetByEmployeeId(id.Value);

            if (employee == null)
                return NotFound();

            return View(employee);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int EmpId)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = _employeeService.DeleteEmployee(EmpId);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));
                Message = "Employee Is Not Deleted";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_webHostEnvironment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View();
                }
                else
                {
                    Message = "Error While Delete Employee";
                    ModelState.AddModelError(string.Empty, Message);
                    return View();
                }
            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { Id = EmpId });
        }


    }
}
