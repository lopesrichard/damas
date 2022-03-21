using System.Reflection;
using Damas.Core.Enums;
using Damas.Core.Structs;
using Damas.Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Damas.Data
{
    public static class ModelBuilderExtension
    {
        public static IDictionary<Type, ValueConverter> Converters = new Dictionary<Type, ValueConverter>()
        {
            { typeof(Position), new PositionConverter() },
            { typeof(Color), new EnumToStringConverter<Color>() },
            { typeof(MessageType), new EnumToStringConverter<MessageType>() },
        };

        public static void Configure(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                foreach (var property in entity.ClrType.GetProperties())
                {
                    if (Converters.TryGetValue(property.PropertyType, out var converter))
                    {
                        builder.UseValueConverter(converter, entity, property);
                    }
                }
            }
        }

        private static void UseValueConverter(
            this ModelBuilder builder,
            ValueConverter converter,
            IMutableEntityType entity,
            PropertyInfo property)
        {
            builder.Entity(entity.Name).Property(property.Name).HasConversion(converter);
        }
    }
}