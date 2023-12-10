using Newtonsoft.Json;
using TaskAssignmentSystemWebApp.Constants;
using TaskAssignmentSystemWebApp.Interfaces;
using TaskAssignmentSystemWebApp.Models;
using TaskAssignmentSystemWebApp.Models.Task;
using TaskAssignmentSystemWebApp.Services;

namespace TaskAssignmentSystemWebApp.Repos
{
    public class TaskRepo : ITaskRepo
    {
        private readonly WebAPICallServices _webAPICallServices;
        private readonly ILogger<TaskRepo> _logger;

        public TaskRepo(WebAPICallServices webAPICallServices, ILogger<TaskRepo> logger)
        {
            _webAPICallServices = webAPICallServices;
            _logger = logger;
        }

        public async Task<string> GetAllTasks(string dataToLoad)
        {
            try
            {
                string returnedData = await _webAPICallServices.HTTPGETRequest("task/GetAll");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                if (deserializedData.ErrCode == GlobalConstants.SuccessErrorCode)
                {
                    return JsonConvert.SerializeObject(deserializedData.Body);
                }

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Getting tasks: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }

        public async Task<string> GetTaskById(string dataToLoad)
        {
            try
            {
                string returnedData = await _webAPICallServices.HTTPGETRequest($"task?id={dataToLoad}");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                if (deserializedData.ErrCode == GlobalConstants.SuccessErrorCode)
                {
                    return JsonConvert.SerializeObject(deserializedData.Body);
                }

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Getting task By ID: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }

        public async Task<string> CreateNewTask(string dataToLoad)
        {
            try
            {
                var dataParams = JsonConvert.DeserializeObject<TaskCreateRequestModel>(dataToLoad);

                if (string.IsNullOrEmpty(dataParams.TaskName.Trim()))
                {
                    return "Task Name is Required";
                }

                if (string.IsNullOrEmpty(dataParams.TaskDescription.Trim()))
                {
                    return "Task Description is Required";
                }

                var returnedData = await _webAPICallServices.HTTPPostRequest(dataParams, "task");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                if (deserializedData.ErrCode == GlobalConstants.SuccessErrorCode)
                {
                    return JsonConvert.SerializeObject(deserializedData.Body);
                }

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Adding Task: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }

        public async Task<string> DeleteTask(string itemId)
        {
            try
            {
                string returnedData = await _webAPICallServices.HTTPDelRequest($"task?id={itemId}");

                var deserializedData = JsonConvert.DeserializeObject<GeneralResponseModel>(returnedData);

                return deserializedData.ErrMsg ?? GlobalConstants.ErrorMessage;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An error occurred while Deleting task: {ex.Message}";
                _logger.LogError(errorMessage);
                return GlobalConstants.ErrorMessage;
            }
        }
    }
}
