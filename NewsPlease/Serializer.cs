using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NewsPlease
{
    public class Serializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        public T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data, Settings);

        public byte[] Serialize<T>(T data) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, Settings));
    }
}