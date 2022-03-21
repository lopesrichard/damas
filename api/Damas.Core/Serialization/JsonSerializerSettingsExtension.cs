using Damas.Core.Converters;
using Damas.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Damas.Core.Serialization
{
    public static class JsonSerializerSettingsExtension
    {
        public static JsonSerializerSettings Configure(this JsonSerializerSettings settings)
        {
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;

            settings.Converters.Add(new PositionConverter());
            settings.Converters.Add(new StringEnumConverter<Color>());
            settings.Converters.Add(new StringEnumConverter<MessageType>());

            return settings;
        }
    }
}