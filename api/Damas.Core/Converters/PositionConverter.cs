using Damas.Core.Structs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Damas.Core.Converters
{
    public class PositionConverter : JsonConverter<Position>
    {
        public override void WriteJson(JsonWriter writer, Position value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, new int[] { value.X, value.Y });
        }

        public override Position ReadJson(JsonReader reader, Type objectType, Position existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var array = JArray.Load(reader);

            var x = array[0].Value<int>();
            var y = array[1].Value<int>();

            return new Position(x, y);
        }
    }
}