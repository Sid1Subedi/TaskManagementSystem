using Newtonsoft.Json;
using System.ComponentModel;

namespace TaskAssignmentSystem.Models
{
    public class GeneralResponseModel<T>
    {
        [DefaultValue("")]
        public string ErrCode { get; set; } = string.Empty;

        [DefaultValue("")]
        public string ErrMsg { get; set; } = string.Empty;

        public T? Body { get; set; }

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
