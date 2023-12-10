using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskAssignmentSystem.Models.Employees;
using TaskAssignmentSystem.Models.TaskAssignment;

namespace TaskAssignmentSystem.Models.Tasks
{
    public class TaskModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TaskName { get; set; }

        [Required]
        [MaxLength(255)]
        public string TaskDescription { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        // Navigation property for employees assigned to this task
        public List<TaskAssignmentModel>? TaskAssignments { get; set; }
    }
}
