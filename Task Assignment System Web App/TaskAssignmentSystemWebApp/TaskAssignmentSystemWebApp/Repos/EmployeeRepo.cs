using Newtonsoft.Json;
using TaskAssignmentSystemWebApp.Constants;
using TaskAssignmentSystemWebApp.Interfaces;
using TaskAssignmentSystemWebApp.Models;
using TaskAssignmentSystemWebApp.Models.Employee;
using TaskAssignmentSystemWebApp.Services;

namespace TaskAssignmentSystemWebApp.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly WebAPICallServices _webAPICallServices;
        private readonly ILogger<EmployeeRepo> _logger;

        public EmployeeRepo(WebAPICallServices webAPICallServices, ILogger<EmployeeRepo> logger)
        {
            _webAPICallServices = webAPICallServices;
            _logger = logger;
        }

        public async Task<string> GetAllEmployees(string dataToLoad)
        {
            try
            {
                string returnedData = await _webAPICallServices.HTTPGETRequest("employee/GetAll");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                if (deserializedData.ErrCode == GlobalConstants.SuccessErrorCode)
                {
                    return JsonConvert.SerializeObject(deserializedData.Body);
                }

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Getting employees: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }

        public async Task<string> GetEmployeeById(string dataToLoad)
        {
            try
            {
                string returnedData = await _webAPICallServices.HTTPGETRequest($"employee?id={dataToLoad}");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                if (deserializedData.ErrCode == GlobalConstants.SuccessErrorCode)
                {
                    return JsonConvert.SerializeObject(deserializedData.Body);
                }

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Getting Employee By ID: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }

        public async Task<string> CreateNewEmployee(string dataToLoad)
        {
            try
            {
                var dataParams = JsonConvert.DeserializeObject<EmployeeCreateRequestModel>(dataToLoad);

                if (string.IsNullOrEmpty(dataParams.EmployeeName.Trim()))
                {
                    return "Employee Name is Required";
                }

                var returnedData = await _webAPICallServices.HTTPPostRequest(dataParams, "employee");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                if (deserializedData.ErrCode == GlobalConstants.SuccessErrorCode)
                {
                    return JsonConvert.SerializeObject(deserializedData.Body);
                }

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Adding employee: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }

        public async Task<string> DeleteEmployee(string itemId)
        {
            try
            {
                string returnedData = await _webAPICallServices.HTTPDelRequest($"employee?id={itemId}");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Deleting employee: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }
    }
}
