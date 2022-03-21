
using Newtonsoft.Json;

namespace Damas.Core.Serialization
{
    public static class JsonSerializerExtension
    {
        public static string Serialize(this object payload)
        {
            return JsonConvert.SerializeObject(payload, new JsonSerializerSettings().Configure());
        }

        public static T? Deserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings().Configure());
        }

        public static object? Deserialize(this string json)
        {
            return JsonConvert.DeserializeObject(json, new JsonSerializerSettings().Configure());
        }
    }
}