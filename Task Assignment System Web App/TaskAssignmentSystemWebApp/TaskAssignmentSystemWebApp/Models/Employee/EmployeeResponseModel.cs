using TaskAssignmentSystemWebApp.Models.Task;

namespace TaskAssignmentSystemWebApp.Models.Employee
{
    public class EmployeeResponseModel : GeneralResponseModel
    {
        public int? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public string? RoleName { get; set; }

        public List<TaskResponseModel>? AssignedTasks { get; set; }
    }
}
