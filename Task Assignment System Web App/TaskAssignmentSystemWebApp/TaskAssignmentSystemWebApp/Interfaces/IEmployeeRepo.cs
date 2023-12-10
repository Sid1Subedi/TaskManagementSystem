namespace TaskAssignmentSystemWebApp.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<string> GetAllEmployees(string dataToLoad);

        Task<string> GetEmployeeById(string dataToLoad);

        Task<string> CreateNewEmployee(string dataToLoad);

        Task<string> DeleteEmployee(string itemId);
    }
}
