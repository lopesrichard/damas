using System.Reflection;
using Damas.Core.Structs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Damas.Data
{
    public static class ModelBuilderExtension
    {
        public static void Configure(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var properties = entity.ClrType.GetProperties();

                builder.UseValueConverter<Position>(new PositionConverter(), entity, properties);
            }
        }

        private static ModelBuilder UseValueConverter<T>(
            this ModelBuilder builder,
            ValueConverter converter,
            IMutableEntityType entity,
            PropertyInfo[] properties)
        {
            foreach (var property in properties.Where(p => p.PropertyType == typeof(T)))
            {
                builder.Entity(entity.Name).Property(property.Name).HasConversion(converter);
            }

            return builder;
        }
    }
}