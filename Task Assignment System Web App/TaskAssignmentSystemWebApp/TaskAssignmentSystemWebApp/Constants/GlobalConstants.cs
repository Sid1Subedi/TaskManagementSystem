namespace TaskAssignmentSystemWebApp.Constants
{
    public class GlobalConstants
    {
        public static readonly string SuccessErrorCode = "200";

        public static readonly string ErrorMessage = "Something Went Wrong. Please Try Again!";

        //private static readonly string BaseUrl = "https://jsonplaceholder.typicode.com";
        private static readonly string BaseUrl = "http://localhost:5172";
        //http://localhost:5172
        public string GenerateURL(string url)
        {
            return $"{BaseUrl}/{url}";
        }
    }
}
