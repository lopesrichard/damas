using Damas.Enums;

namespace Damas.Entities
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public Player(Guid id, string name, Color color) : base(id)
        {
            Name = name;
            Color = color;
        }
    }
}