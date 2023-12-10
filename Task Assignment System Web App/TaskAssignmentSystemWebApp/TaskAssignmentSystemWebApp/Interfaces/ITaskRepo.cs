namespace TaskAssignmentSystemWebApp.Interfaces
{
    public interface ITaskRepo
    {
        Task<string> GetAllTasks(string dataToLoad);

        Task<string> GetTaskById(string dataToLoad);

        Task<string> CreateNewTask(string dataToLoad);

        Task<string> DeleteTask(string itemId);
    }
}
