namespace TaskAssignmentSystemWebApp.Services;

using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Policy;
using System.Text;
using TaskAssignmentSystemWebApp.Constants;

public class WebAPICallServices
{
    #region HTTP WEB API CALLS

    #region HTTP GET

    public async Task<string> HTTPGETRequest(string url)
    {
        try
        {
            bool showResponse = true;

            GlobalConstants globalConstants = new GlobalConstants();

            var generateURI = globalConstants.GenerateURL(url);

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(generateURI);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                var request = client.GetAsync(generateURI);

                var result = await request;

                var response = request.Result.Content.ReadAsStringAsync().Result;

                JObject jsonResponse = JObject.Parse(response);

                if (showResponse)
                {
                    Debug.WriteLine("Json Response is: " + jsonResponse);
                }

                try
                {
                    int errorCode = 0;
                    if (jsonResponse["ErrCode"] is not null)
                    {
                        errorCode = int.Parse(jsonResponse["ErrCode"].ToString());
                    }
                    if (errorCode < 0)
                    {
                        return jsonResponse["ErrorMsg"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "Something Went Wrong, Just Stay Calm It is Almost Fixed!";
                }

                return response;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception is: " + ex.Message);
            return "Failed GET";
        }
    }

    #endregion

    #region HTTP POST

    public async Task<string> HTTPPostRequest<T>(T requestParams, string url)
    {
        try
        {
            bool showRequest = true;
            bool showResponse = true;

            GlobalConstants globalConstants = new GlobalConstants();
            //Convert Obj to json String
            var requestParamSer = requestParams.ToString();

            var generateURI = globalConstants.GenerateURL(url);

            if (showRequest)
            {
                Debug.WriteLine("requestParamSer is: " + requestParamSer);
                Debug.WriteLine("generateURI: " + generateURI);
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(generateURI);
                client.DefaultRequestHeaders.Clear();

                Debug.WriteLine("Url is: " + client.BaseAddress + generateURI);

                //Define request data format
                var content = new StringContent(requestParamSer, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                var request = client.PostAsync(generateURI, content);

                var result = await request;

                var response = request.Result.Content.ReadAsStringAsync().Result;

                JObject jsonResponse = JObject.Parse(response);

                if (showResponse)
                {
                    Debug.WriteLine("Json Response is: " + jsonResponse);
                }

                try
                {
                    int errorCode = 0;
                    if (jsonResponse["ErrCode"] is not null)
                    {
                        errorCode = int.Parse(jsonResponse["ErrCode"].ToString());
                    }
                    if (errorCode < 0)
                    {
                        return jsonResponse["ErrorMsg"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "Something Went Wrong, Just Stay Calm It is Almost Fixed!";
                }

                return response;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception is: " + ex.Message);
            return "Failed POST";
        }
    }

    #endregion

    #region HTTP PUT

    public async Task<string> HTTPPutRequest<T>(T requestParams, string url)
    {
        try
        {
            bool showRequest = true;
            bool showResponse = true;

            GlobalConstants globalConstants = new GlobalConstants();

            //Convert Obj to json String
            var requestParamSer = requestParams.ToString();

            var generateURI = globalConstants.GenerateURL(url);

            if (showRequest)
            {
                Debug.WriteLine("requestParamSer is: " + requestParamSer);
                Debug.WriteLine("generateURI: " + generateURI);
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(generateURI);
                client.DefaultRequestHeaders.Clear();

                Debug.WriteLine("Url is: " + client.BaseAddress + generateURI);

                //Define request data format
                var content = new StringContent(requestParamSer, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                var request = client.PutAsync(generateURI, content);

                var result = await request;

                var response = request.Result.Content.ReadAsStringAsync().Result;

                JObject jsonResponse = JObject.Parse(response);

                if (showResponse)
                {
                    Debug.WriteLine("Json Response is: " + jsonResponse);
                }

                try
                {
                    int errorCode = 0;
                    if (jsonResponse["ErrCode"] is not null)
                    {
                        errorCode = int.Parse(jsonResponse["ErrCode"].ToString());
                    }
                    if (errorCode < 0)
                    {
                        return jsonResponse["ErrorMsg"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "Something Went Wrong, Just Stay Calm It is Almost Fixed!";
                }

                return response;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception is: " + ex.Message);
            return "Failed PUT";
        }
    }

    #endregion

    #region HTTP DEL

    public async Task<string> HTTPDelRequest(string url)
    {
        try
        {
            GlobalConstants globalConstants = new GlobalConstants();

            var generateURI = globalConstants.GenerateURL(url);

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(generateURI);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                Debug.WriteLine("URL Full: " + generateURI);

                var request = client.DeleteAsync(generateURI);

                var result = await request;

                var response = request.Result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("response is: " + response);

                return response;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception is: " + ex.Message);
            return "Failed DELETE";
        }
    }

    #endregion

    #endregion
}