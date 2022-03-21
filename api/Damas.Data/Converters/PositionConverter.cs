using Damas.Core.Structs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Damas.Data.Converters
{
    public class PositionConverter : ValueConverter<Position, string>
    {
        public PositionConverter() : base(
            position => position.ToString(),
            position => Position.Parse(position)
        )
        {
        }
    }
}