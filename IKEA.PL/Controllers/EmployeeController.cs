using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
