using Microsoft.EntityFrameworkCore;
using TaskAssignmentSystem.Interfaces;
using TaskAssignmentSystem.Models;
using TaskAssignmentSystem.Models.Employees;
using TaskAssignmentSystem.Models.TaskAssignment;
using TaskAssignmentSystem.Models.Tasks;
using TaskAssignmentSystem.TaskAssignmentDbContext;

namespace TaskAssignmentSystem.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly TaskAssignmentSystemDBContext _dbContext;
        private readonly ILogger<EmployeeRepo> _logger;

        public EmployeeRepo(ILogger<EmployeeRepo> logger, TaskAssignmentSystemDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<List<EmployeeResponseDto>?> GetAllEmployees()
        {
            try
            {
                IEnumerable<EmployeeModel> employeesList = await _dbContext.Employees.OrderByDescending(e => e.CreationDate).ToListAsync();

                List<EmployeeResponseDto> employeeResponseList = new();

                // In a more complex situation we can use auto mapping libraries such as AutoMapper or Mapperly. Doing custom mapping for now

                foreach (var item in employeesList)
                {
                    employeeResponseList.Add(new EmployeeResponseDto
                    {
                        EmployeeId = item.EmployeeId,
                        EmployeeName = item.EmployeeName,
                    });
                }

                return employeeResponseList;
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured While Getting Employees List: {exception}", ex);
                return null;
            }
        }

        public async Task<EmployeeSingleResponseDto?> GetEmployeeById(int id)
        {
            try
            {
                var employeeModel = await _dbContext.Employees
                    .Include(e => e.TaskAssignments)
                    .ThenInclude(ta => ta.Task)
                    .FirstOrDefaultAsync(e => e.EmployeeId == id);

                // Not Found
                if (employeeModel == null)
                {
                    return null;
                }

                // Map employee information to DTO
                var employeeResponseDto = new EmployeeSingleResponseDto
                {
                    EmployeeId = employeeModel.EmployeeId,
                    EmployeeName = employeeModel.EmployeeName,
                    RoleName = employeeModel.RoleName,
                    AssignedTasks = employeeModel.TaskAssignments
                    .Select(ta => new TaskResponseDto
                    {
                        TaskId = ta.TaskId,
                        TaskName = ta.Task.TaskName,
                        TaskDescription = ta.Task.TaskDescription,
                    }).ToList(),
                };

                return employeeResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occurred While Getting Employee: {exception}", ex);
                return null;
            }
        }

        public async Task<EmployeeSingleResponseDto?> CreateNewEmployee(EmployeeCreateDto employeeCreateDto)
        {
            try
            {
                // In a more complex situation we can use auto mapping libraries such as AutoMapper or Mapperly. Doing custom mapping for now

                EmployeeModel employeeModel = new()
                {
                    EmployeeName = employeeCreateDto.EmployeeName,
                    RoleName = employeeCreateDto.RoleName,
                };

                // Further we can check if the employee already exist or the username or email already exist and stuffs, since we are using id as unique identifier and it is auto generated, all created employees are unique for now

                _dbContext.Employees.Add(employeeModel);

                if (employeeCreateDto.TaskIds != null && employeeCreateDto.TaskIds.Count != 0)
                {
                    foreach (var taskId in employeeCreateDto.TaskIds)
                    {
                        var task = await _dbContext.Tasks.FindAsync(taskId);

                        if (task != null)
                        {
                            var taskAssignment = new TaskAssignmentModel { TaskId = taskId, Employee = employeeModel };
                            _dbContext.TaskAssignments.Add(taskAssignment);
                        }
                    }
                }

                // Save Changes To Db
                await _dbContext.SaveChangesAsync();

                // In a more complex situation we can use auto mapping libraries such as AutoMapper or Mapperly. Doing custom mapping for now

                EmployeeSingleResponseDto employeeResponseDto = new()
                {
                    EmployeeId = employeeModel.EmployeeId,
                    EmployeeName = employeeModel.EmployeeName,
                    RoleName = employeeModel.RoleName,
                    AssignedTasks = employeeModel.TaskAssignments != null ? employeeModel.TaskAssignments
                    .Select(ta => new TaskResponseDto
                    {
                        TaskId = ta.TaskId,
                        TaskName = ta.Task.TaskName,
                        TaskDescription = ta.Task.TaskDescription,
                    }).ToList() : new(),
                };

                return employeeResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error Occured While Creating Employee: {exception}", ex);
                return null;
            }
        }

        public async Task<bool?> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await _dbContext.Employees.FindAsync(id);

                // Not Found
                if (employeeToDelete == null)
                {
                    return false;
                }

                // Depending on the use case, we can make a isDeleted Property inside employee model and toggle it's value to delete or recover employee
                // Remove the employee
                _dbContext.Employees.Remove(employeeToDelete);

                // Save Changes To Db
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting an employee: {ExceptionMessage}", ex.Message);
                return null;
            }
        }
    }
}
