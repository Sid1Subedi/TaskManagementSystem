using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskAssignmentSystemWebApp.Constants;
using TaskAssignmentSystemWebApp.Interfaces;
using TaskAssignmentSystemWebApp.Models.Employee;
using TaskAssignmentSystemWebApp.Models.Task;
using TaskAssignmentSystemWebApp.Repos;

namespace TaskAssignmentSystemWebApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepo _taskRepo;

        public TaskController(ITaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTask(string dataToLoad)
        {
            return Json(await _taskRepo.CreateNewTask(dataToLoad));
        }

        #region Task List Table

        [HttpPost]
        public async Task<IActionResult> GetAllTasks(string dataToLoad)
        {
            return Json(await _taskRepo.GetAllTasks(dataToLoad));
        }

        [HttpPost]
        public async Task<IActionResult> GetTaskById(string dataToLoad)
        {
            return Json(await _taskRepo.GetTaskById(dataToLoad));
        }

        [HttpPost]
        public ActionResult LoadTaskListTablePartialView(string dataToLoad)
        {
            try
            {
                ViewBag.Success = JsonConvert.DeserializeObject<List<TaskResponseModel>>(dataToLoad); ;
            }
            catch (Exception ex) {
                ViewBag.Error = dataToLoad;
            }

            return PartialView("_TaskListTablePartialView");
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> DeleteTask(string itemId)
        {
            return Json(await _taskRepo.DeleteTask(itemId));
        }
    }
}
