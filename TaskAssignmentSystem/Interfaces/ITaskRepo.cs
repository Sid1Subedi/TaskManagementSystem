using TaskAssignmentSystem.Models.Employees;
using TaskAssignmentSystem.Models.Tasks;

namespace TaskAssignmentSystem.Interfaces
{
    public interface ITaskRepo
    {
        Task<List<TaskResponseDto>?> GetAllTasks();

        Task<TaskResponseDto?> GetTaskById(int id);

        Task<TaskResponseDto?> CreateNewTask(TaskCreateDto taskCreateDto);

        Task<bool?> DeleteTask(int id);
    }
}
