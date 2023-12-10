using System.ComponentModel.DataAnnotations;

namespace TaskAssignmentSystem.Models.Tasks
{
    public class AssignTasksToEmployees
    {
        [Required]
        public List<int> EmployeeIds { get; set; }

        [Required]
        public List<int> TaskIds { get; set; }
    }
}
