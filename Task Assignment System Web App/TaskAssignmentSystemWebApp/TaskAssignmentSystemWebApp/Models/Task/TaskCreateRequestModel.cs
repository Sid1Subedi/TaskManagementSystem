using System.ComponentModel.DataAnnotations;

namespace TaskAssignmentSystemWebApp.Models.Task
{
    public class TaskCreateRequestModel : GeneralResponseModel
    {
        public string? TaskName { get; set; }

        public string? TaskDescription { get; set; }

        public List<int>? AssignedEmployeeIds { get; set; }
    }
}
