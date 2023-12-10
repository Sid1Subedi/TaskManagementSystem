using System.ComponentModel.DataAnnotations;

namespace TaskAssignmentSystem.Models.Employees
{
    public class EmployeeCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [MaxLength(100)]
        public string? RoleName { get; set; }

        public List<int>? TaskIds { get; set; }
    }
}
