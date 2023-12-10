using System.ComponentModel.DataAnnotations;

namespace TaskAssignmentSystem.Models.Tasks
{
    public class TaskCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string TaskName { get; set; }

        [Required]
        [MaxLength(255)]
        public string TaskDescription { get; set; }

        public List<int>? AssignedEmployeeIds { get; set; }
    }
}
