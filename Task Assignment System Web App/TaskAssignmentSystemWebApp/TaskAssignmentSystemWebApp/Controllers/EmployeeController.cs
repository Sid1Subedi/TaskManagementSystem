using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskAssignmentSystemWebApp.Interfaces;
using TaskAssignmentSystemWebApp.Models.Employee;
using TaskAssignmentSystemWebApp.Repos;

namespace TaskAssignmentSystemWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllEmployees(string dataToLoad)
        {
            return Json(await _employeeRepo.GetAllEmployees(dataToLoad));
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeById(string dataToLoad)
        {
            return Json(await _employeeRepo.GetEmployeeById(dataToLoad));
        }

        [HttpPost]
        public ActionResult LoadEmployeeListPartialView(string dataToLoad)
        {
            try
            {
                ViewBag.Success = JsonConvert.DeserializeObject<List<EmployeeResponseModel>>(dataToLoad); ;
            }
            catch (Exception ex)
            {
                ViewBag.Error = dataToLoad;
            }

            return PartialView("_EmployeeListPartialView");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEmployee(string dataToLoad)
        {
            return Json(await _employeeRepo.CreateNewEmployee(dataToLoad));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(string itemId)
        {
            return Json(await _employeeRepo.DeleteEmployee(itemId));
        }
    }
}
