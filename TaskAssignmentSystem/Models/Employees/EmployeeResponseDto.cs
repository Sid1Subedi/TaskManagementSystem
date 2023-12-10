using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskAssignmentSystem.Models.Tasks;

namespace TaskAssignmentSystem.Models.Employees
{
    public class EmployeeResponseDto : GeneralResponseModel<EmployeeResponseDto>
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmployeeName { get; set; }
    }
}
