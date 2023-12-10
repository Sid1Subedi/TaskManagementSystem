using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskAssignmentSystem.Interfaces;
using TaskAssignmentSystem.Models.Employees;
using TaskAssignmentSystem.Models.TaskAssignment;
using TaskAssignmentSystem.Models.Tasks;
using TaskAssignmentSystem.TaskAssignmentDbContext;

namespace TaskAssignmentSystem.Repos
{
    public class TaskRepo : ITaskRepo
    {
        private readonly TaskAssignmentSystemDBContext _dbContext;
        private readonly ILogger<TaskRepo> _logger;

        public TaskRepo(TaskAssignmentSystemDBContext dbContext, ILogger<TaskRepo> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<TaskResponseDto>?> GetAllTasks()
        {
            try
            {
                var allTasks = await _dbContext.Tasks
                    .Include(t => t.TaskAssignments)
                    .ThenInclude(ta => ta.Employee)
                    .OrderByDescending(ta => ta.CreationDate)
                    .ToListAsync();

                // Map the TaskModel to TaskResponseDto
                var taskResponseDtos = allTasks.Select(task => new TaskResponseDto
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    TaskDescription = task.TaskDescription,
                    AssignedEmployees = task.TaskAssignments.Select(ta => new EmployeeResponseDto
                    {
                        EmployeeId = ta.Employee.EmployeeId,
                        EmployeeName = ta.Employee.EmployeeName,
                    }).ToList(),
                }).ToList();

                return taskResponseDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching all tasks: {ExceptionMessage}", ex.Message);
                return null;
            }
        }

        public async Task<TaskResponseDto?> GetTaskById(int id)
        {
            try
            {
                TaskResponseDto taskResponseDto = new();

                var task = await _dbContext.Tasks
                    .Include(t => t.TaskAssignments)
                    .ThenInclude(ta => ta.Employee)
                    .FirstOrDefaultAsync(t => t.TaskId == id);

                // Not Found
                if (task == null)
                {
                    return taskResponseDto;
                }

                // Map the TaskModel to TaskResponseDto
                taskResponseDto = new TaskResponseDto
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    TaskDescription = task.TaskDescription,
                    AssignedEmployees = task.TaskAssignments.Select(ta => new EmployeeResponseDto
                    {
                        EmployeeId = ta.Employee.EmployeeId,
                        EmployeeName = ta.Employee.EmployeeName,
                    }).ToList(),
                };

                return taskResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching a task by ID: {ExceptionMessage}", ex.Message);
                return null;
            }
        }

        public async Task<TaskResponseDto?> CreateNewTask(TaskCreateDto taskCreateDto)
        {
            try
            {
                // Create a new TaskModel and populate its properties
                var newTask = new TaskModel
                {
                    TaskName = taskCreateDto.TaskName,
                    TaskDescription = taskCreateDto.TaskDescription,
                };

                // Attach the new task to the context
                _dbContext.Tasks.Add(newTask);

                // Assign employees to the task if selected
                if (taskCreateDto.AssignedEmployeeIds != null && taskCreateDto.AssignedEmployeeIds.Any())
                {
                    foreach (var employeeId in taskCreateDto.AssignedEmployeeIds)
                    {
                        var employee = await _dbContext.Employees.FindAsync(employeeId);

                        if (employee != null)
                        {
                            var taskAssignment = new TaskAssignmentModel
                            {
                                Task = newTask,
                                Employee = employee,
                            };

                            _dbContext.TaskAssignments.Add(taskAssignment);
                        }
                    }
                }

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                // Map the created task to TaskResponseDto
                var taskResponseDto = new TaskResponseDto
                {
                    TaskId = newTask.TaskId,
                    TaskName = newTask.TaskName,
                    TaskDescription = newTask.TaskDescription,
                    AssignedEmployees = newTask.TaskAssignments != null ? newTask.TaskAssignments.Select(ta => new EmployeeResponseDto
                    {
                        EmployeeId = ta.Employee.EmployeeId,
                        EmployeeName = ta.Employee.EmployeeName,
                    }).ToList() : new(),
                };

                return taskResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating a task: {ExceptionMessage}", ex.Message);
                return null;
            }
        }

        public async Task<bool?> DeleteTask(int id)
        {
            try
            {
                var taskToDelete = await _dbContext.Tasks
                    .Include(t => t.TaskAssignments)
                    .FirstOrDefaultAsync(t => t.TaskId == id);

                // Not Found
                if (taskToDelete == null)
                {
                    return false;
                }

                // Depending on the use case, we can make a isDeleted Property inside Task model and toggle it's value to delete or recover task
                // Remove The Task Assignments
                _dbContext.TaskAssignments.RemoveRange(taskToDelete.TaskAssignments);

                // Remove the task from the DbSet
                _dbContext.Tasks.Remove(taskToDelete);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting a task: {ExceptionMessage}", ex.Message);
                return null;
            }
        }
    }
}