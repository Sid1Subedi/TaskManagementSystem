using Newtonsoft.Json;

namespace TaskAssignmentSystemWebApp.Models
{
    public class GeneralResponseModel
    {
        public string? ErrCode { get; set; }
        public string? ErrMsg { get; set; }

        public object? Body { get; set; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            var options = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };
            return JsonConvert.SerializeObject(this, options);
        }
    }
}
