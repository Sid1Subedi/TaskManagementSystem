using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskAssignmentSystem.Models.TaskAssignment;

namespace TaskAssignmentSystem.Models.Employees
{
    public class EmployeeModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmployeeName { get; set; }

        [MaxLength(100)]
        public string? RoleName {  get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        // Navigation property for tasks assigned to this employee
        public List<TaskAssignmentModel>? TaskAssignments { get; set; }
    }
}
