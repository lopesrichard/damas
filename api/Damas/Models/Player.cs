using Damas.Enums;

namespace Damas.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
        }
    }
}