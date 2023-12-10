namespace TaskAssignmentSystemWebApp.Models.Employee
{
    public class EmployeeCreateRequestModel : GeneralResponseModel
    {
        public string? EmployeeName { get; set; }

        public string? RoleName { get; set; }

        public List<int>? TaskIds { get; set; }
    }
}
