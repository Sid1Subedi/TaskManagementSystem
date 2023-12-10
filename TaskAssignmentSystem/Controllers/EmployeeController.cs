using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskAssignmentSystem.Interfaces;
using TaskAssignmentSystem.Models;
using TaskAssignmentSystem.Models.Employees;

namespace TaskAssignmentSystem.Controllers
{

    // ||
    [ApiController]
    [Route("/employee")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepo _EmployeeRepo;

        public EmployeeController(IEmployeeRepo IEmployeeRepo)
        {
            _EmployeeRepo = IEmployeeRepo;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllEmployees()
        {
            GeneralResponseModel<List<EmployeeResponseDto>?> generalResponseModel = new()
            {
                ErrCode = "500",
                ErrMsg = "Something Went Wrong. Please Try Again Later",
                Body = null,
            };

            var employeeResponseDtoList = await _EmployeeRepo.GetAllEmployees();

            // Internal Server Error
            if (employeeResponseDtoList == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, generalResponseModel);
            }

            // Success
            generalResponseModel = new()
            {
                ErrCode = "200",
                ErrMsg = "Success",
                Body = employeeResponseDtoList,
            };

            return Ok(generalResponseModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            GeneralResponseModel<EmployeeSingleResponseDto> generalResponseModel = new()
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

            var employeeResponseDto = await _EmployeeRepo.GetEmployeeById(id);

            // Internal Server Error
            if (employeeResponseDto == null)
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
            if (employeeResponseDto.EmployeeId <= 0)
            {
                generalResponseModel = new()
                {
                    ErrCode = "404",
                    ErrMsg = $"Employee With ID: {id} Not Found",
                    Body = null,
                };

                return NotFound(generalResponseModel);
            }

            // Success
            generalResponseModel = new()
            {
                ErrCode = "200",
                ErrMsg = "Success",
                Body = employeeResponseDto,
            };

            return Ok(generalResponseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEmployee([FromBody] EmployeeCreateDto employeeCreateDto)
        {
            GeneralResponseModel<EmployeeSingleResponseDto> generalResponseModel;

            // Bad Request
            if (employeeCreateDto == null)
            {
                generalResponseModel = new()
                {
                    ErrCode = "400",
                    ErrMsg = "Bad Request. No Data Found To Save",
                    Body = null,
                };

                return BadRequest(generalResponseModel);
            }

            var employeeCreateResponse = await _EmployeeRepo.CreateNewEmployee(employeeCreateDto);

            // Internal Server Error
            if (employeeCreateResponse == null)
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
                Body = employeeCreateResponse,
            };

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeCreateResponse.EmployeeId }, generalResponseModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            GeneralResponseModel<bool> generalResponseModel = new();

            var isDeleted = await _EmployeeRepo.DeleteEmployee(id);

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
                    ErrMsg = $"Employee With ID: {id} Not Found",
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
}
