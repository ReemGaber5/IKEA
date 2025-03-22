using IKEA.BLL.DTOs.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var Departments=_departmentService.GetAll();
            return View(Departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO department)
        {
            if(!ModelState.IsValid)
                return View(department);

            var Message = string.Empty;


            try
            {
                var Result = _departmentService.CreateDepartment(department);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    Message = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(department);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                //set Default message to user 
                //check first if environment devlopment show message to develope(using web host environment)

                if (_environment.IsDevelopment())
                {
                    Message=ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View(department);
                }
                else
                {
                    Message = "Error While Creation"; 
                    ModelState.AddModelError(string.Empty, Message);
                    return View(department);
                }
            }  
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(id== null)
            {
                return BadRequest();
            }

            var department = _departmentService.GetByDepartmentId(id.Value);
            if(department==null)
            {
                return NotFound();
            }
            return View (department);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var department = _departmentService.GetByDepartmentId(id.Value);

            if (department == null)
                return NotFound();

            //add this to return object of type UpdatedDepartmentDTO instead of DepartmentDetailsDTO
            var MappedDepartment = new UpdatedDepartmentDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                C0reationDate = department.C0reationDate
            };

            return View(MappedDepartment );



        }

        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDTO updatedDepartmentDTO)
        {
            if (!ModelState.IsValid)
                return View(updatedDepartmentDTO);
            var Message = string.Empty;

            try
            {
                var Result=_departmentService.UpdateDepartment(updatedDepartmentDTO);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department Is Not Updated";

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                //set Default message to user 
                //check first if environment devlopment show message to develope(using web host environment)

                if (_environment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View(updatedDepartmentDTO);
                }
                else
                {
                    Message = "Error While Update";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(updatedDepartmentDTO);
                }
            }
            ModelState.AddModelError(string.Empty, Message);
            return View (updatedDepartmentDTO);
              

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var department = _departmentService.GetByDepartmentId(id.Value);

            if (department == null)
                return NotFound();

            return View(department);

        }

        [HttpPost]
        public IActionResult Delete(int DeptId)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = _departmentService.DeleteDepartment(DeptId);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));
                Message = "Department Is Not Deleted";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View();
                }
                else
                {
                    Message = "Error While Delete Department";
                    ModelState.AddModelError(string.Empty, Message);
                    return View();
                }
            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new {Id= DeptId });
        }

    }
}
