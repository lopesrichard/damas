using System.Linq.Expressions;
using Damas.Core.Structs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Damas.Data
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