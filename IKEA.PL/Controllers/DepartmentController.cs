using IKEA.BLL.DTOs.Departments;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using IKEA.PL.ViewModel;
using AutoMapper;
using System.Security.Policy;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment,IMapper mapper)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
            _mapper = mapper;
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel department)
        {
            if(!ModelState.IsValid)
                return View(department);

            var Message = string.Empty;


            try
            {
                var dept = _mapper.Map<DepartmentViewModel, CreatedDepartmentDTO>(department);

                //var deptDTO = new CreatedDepartmentDTO()
                //{
                //    Name=department.Name,
                //    Code=department.Code,
                //    Description=department.Description,
                //    C0reationDate=department.C0reationDate,

                //};
                var Result = _departmentService.CreateDepartment(dept);

                if (Result > 0)
                {
                    TempData["Message"] = $"{department.Name} Department Created";
                    return RedirectToAction(nameof(Index));
                }
                else
                    Message = "Department Is Not Created";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                 
                //set Default message to user 
                //check first if environment devlopment show message to develope(using web host environment)

                if (_environment.IsDevelopment())
                       Message=ex.Message;
                else
                    Message = "Error While Creation";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(department);
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
            var MappedDept = _mapper.Map<DepartmentDetailsDTO, DepartmentViewModel>(department);

            //var MappedDepartment = new DepartmentViewModel()
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    C0reationDate = department.C0reationDate
            //};

            return View(MappedDept);



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentViewModel department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var Message = string.Empty;

            try
            {
                var deptDTO=_mapper.Map< DepartmentViewModel,UpdatedDepartmentDTO>(department);
                //var deptDTO = new UpdatedDepartmentDTO()
                //{
                //    Id= department.Id,
                //    Name = department.Name,
                //    Code = department.Code,
                //    Description = department.Description,
                //    C0reationDate = department.C0reationDate,

                //};
                var Result=_departmentService.UpdateDepartment(deptDTO);
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
                    Message = ex.Message;

                else
                    Message = "Error While Update";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View (department);
              

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
        [ValidateAntiForgeryToken]
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
