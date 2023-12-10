using TaskAssignmentSystem.Models.Employees;

namespace TaskAssignmentSystem.Interfaces
{
    public interface IEmployeeRepo
    {
        // Additionally, we can add pagination or search filters
        Task<List<EmployeeResponseDto>?> GetAllEmployees();

        Task<EmployeeSingleResponseDto?> GetEmployeeById(int id);

        Task<EmployeeSingleResponseDto?> CreateNewEmployee(EmployeeCreateDto employeeCreateDto);

        Task<bool?> DeleteEmployee(int id);
    }
}
