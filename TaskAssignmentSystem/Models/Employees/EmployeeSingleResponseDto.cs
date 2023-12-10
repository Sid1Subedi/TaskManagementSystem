using System.ComponentModel.DataAnnotations;
using TaskAssignmentSystem.Models.Tasks;

namespace TaskAssignmentSystem.Models.Employees
{
    public class EmployeeSingleResponseDto : GeneralResponseModel<EmployeeSingleResponseDto>
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [MaxLength(100)]
        public string? RoleName { get; set; }

        public List<TaskResponseDto>? AssignedTasks { get; set; }
    }
}
