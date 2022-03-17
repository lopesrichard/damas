
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Damas.Serialization
{
    public static class JsonSerializerExtension
    {
        public static string Serialize(this object payload)
        {
            return JsonConvert.SerializeObject(payload, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        public static T? Deserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static object? Deserialize(this string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}