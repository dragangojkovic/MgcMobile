using Newtonsoft.Json;

namespace MBoxMobile.Models
{
    public class IntWrapper
    {
        [JsonProperty("d")]
        public int IntValue { get; set; }
    }
}
