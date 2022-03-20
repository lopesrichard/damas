using Damas.Core.Enums;

namespace Damas.Core.Enums
{
    public static class ColorExtension
    {
        public static Color Opposite(this Color color)
        {
            return color == Color.BLACK ? Color.WHITE : Color.BLACK;
        }
    }
}