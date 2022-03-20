namespace Damas.Entities
{
    public class Player : Entity
    {
        public string Name { get; set; }

        public Player(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}