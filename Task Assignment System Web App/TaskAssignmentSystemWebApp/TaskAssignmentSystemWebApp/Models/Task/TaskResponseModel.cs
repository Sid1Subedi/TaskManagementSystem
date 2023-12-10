using TaskAssignmentSystemWebApp.Models.Employee;

namespace TaskAssignmentSystemWebApp.Models.Task
{
    public class TaskResponseModel : GeneralResponseModel
    {
        public int? TaskId { get; set; }

        public string? TaskName { get; set; }

        public string? TaskDescription { get; set; }

        public List<EmployeeResponseModel>? AssignedEmployees { get; set; }
    }
}
