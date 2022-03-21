using Newtonsoft.Json.Converters;

namespace Damas.Core.Converters
{
    public class StringEnumConverter<TEnum> : StringEnumConverter where TEnum : struct, IConvertible
    {
        public override bool CanConvert(Type source)
        {
            var type = Nullable.GetUnderlyingType(source) ?? source;
            return type.IsEnum && type == typeof(TEnum);
        }
    }
}