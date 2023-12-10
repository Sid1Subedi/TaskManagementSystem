using TaskAssignmentSystem.Models.Employees;
using TaskAssignmentSystem.Models.Tasks;

namespace TaskAssignmentSystem.Models.TaskAssignment
{
    // This is a bridge enitity model
    public class TaskAssignmentModel
    {
        public int TaskId { get; set; }
        public TaskModel? Task { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeModel? Employee { get; set; }

        public DateTime TaskAssignedDate { get; set; } = DateTime.UtcNow;
    }
}
