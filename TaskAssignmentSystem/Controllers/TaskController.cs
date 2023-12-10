using Microsoft.AspNetCore.Mvc;
using TaskAssignmentSystem.Interfaces;
using TaskAssignmentSystem.Models.Tasks;
using TaskAssignmentSystem.Models;
using System.Net;
using TaskAssignmentSystem.Repos;

namespace TaskAssignmentSystem.Controllers;

// ||
[ApiController]
[Route("/task")]
public class TaskController : ControllerBase
{
    private ITaskRepo _TaskRepo;

    public TaskController(ITaskRepo ITaskRepo)
    {
        _TaskRepo = ITaskRepo;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllTasks()
    {
        GeneralResponseModel<List<TaskResponseDto>?> generalResponseModel = new()
        {
            ErrCode = "500",
            ErrMsg = "Something Went Wrong. Please Try Again Later",
            Body = null,
        };

        var taskResponseDtoList = await _TaskRepo.GetAllTasks();

        // Internal Server Error
        if (taskResponseDtoList == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, generalResponseModel);
        }

        // Success
        generalResponseModel = new()
        {
            ErrCode = "200",
            ErrMsg = "Success",
            Body = taskResponseDtoList,
        };

        return Ok(generalResponseModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskById(int id)
    {
        GeneralResponseModel<TaskResponseDto> generalResponseModel = new()
        {
            ErrCode = "400",
            ErrMsg = "Bad Request, Id Must be greater than 0",
            Body = null,
        };

        // Bad Request
        if (id <= 0)
        {
            return BadRequest(generalResponseModel);
        }

        var taskResponseDto = await _TaskRepo.GetTaskById(id);

        // Internal Server Error
        if (taskResponseDto == null)
        {
            generalResponseModel = new()
            {
                ErrCode = "500",
                ErrMsg = "Something Went Wrong. Please Try Again Later",
                Body = null,
            };

            return StatusCode((int)HttpStatusCode.InternalServerError, generalResponseModel);
        }

        // Not Found
        if (taskResponseDto.TaskId <= 0)
        {
            generalResponseModel = new()
            {
                ErrCode = "404",
                ErrMsg = $"Task With ID: {id} Not Found",
                Body = null,
            };

            return NotFound(generalResponseModel);
        }

        // Success
        generalResponseModel = new()
        {
            ErrCode = "200",
            ErrMsg = "Success",
            Body = taskResponseDto,
        };

        return Ok(generalResponseModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewTask([FromBody] TaskCreateDto taskCreateDto)
    {
        GeneralResponseModel<TaskResponseDto> generalResponseModel;

        // Bad Request
        if (taskCreateDto == null)
        {
            generalResponseModel = new()
            {
                ErrCode = "400",
                ErrMsg = "Bad Request. No Data Found To Save",
                Body = null,
            };
            return BadRequest(generalResponseModel);
        }

        var taskCreateResponse = await _TaskRepo.CreateNewTask(taskCreateDto);

        // Internal Server Error
        if (taskCreateResponse == null)
        {
            generalResponseModel = new()
            {
                ErrCode = "500",
                ErrMsg = "Something Went Wrong. Please Try Again Later",
                Body = null,
            };

            return StatusCode((int)HttpStatusCode.InternalServerError, generalResponseModel);
        }

        // Success
        generalResponseModel = new()
        {
            ErrCode = "200",
            ErrMsg = "Success",
            Body = taskCreateResponse,
        };

        return CreatedAtAction(nameof(GetTaskById), new { id = taskCreateResponse.TaskId }, generalResponseModel);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTask(int id)
    {
        GeneralResponseModel<bool> generalResponseModel = new();

        var isDeleted = await _TaskRepo.DeleteTask(id);

        // Internal Server Error
        if (isDeleted == null)
        {
            generalResponseModel = new()
            {
                ErrCode = "500",
                ErrMsg = "Something Went Wrong. Please Try Again Later",
                Body = false,
            };

            return StatusCode((int)HttpStatusCode.InternalServerError, generalResponseModel);
        }

        // Not Found
        if (isDeleted == false)
        {
            generalResponseModel = new()
            {
                ErrCode = "404",
                ErrMsg = $"Task With ID: {id} Not Found",
                Body = false,
            };

            return NotFound(generalResponseModel);
        }

        // Success
        generalResponseModel = new()
        {
            ErrCode = "200",
            ErrMsg = "Success",
            Body = true,
        };

        return Ok(generalResponseModel);
    }
}
