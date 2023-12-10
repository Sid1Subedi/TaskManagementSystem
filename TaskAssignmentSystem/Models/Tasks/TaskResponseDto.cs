using System.ComponentModel.DataAnnotations;
using TaskAssignmentSystem.Models.Employees;

namespace TaskAssignmentSystem.Models.Tasks
{
    public class TaskResponseDto : GeneralResponseModel<TaskResponseDto>
    {
        public int TaskId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TaskName { get; set; }

        [Required]
        [MaxLength(255)]
        public string TaskDescription { get; set; }

        public List<EmployeeResponseDto>? AssignedEmployees { get; set; }
    }
}
