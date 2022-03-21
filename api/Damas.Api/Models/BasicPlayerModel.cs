using System.Linq.Expressions;
using Damas.Data.Entities;

namespace Damas.Api.Models
{
    public class BasicPlayerModel
    {
        public static Expression<Func<Player, BasicPlayerModel>> Selector
        {
            get => (Player player) => new BasicPlayerModel(player.Id, player.Name);
        }

        public static Func<Player, BasicPlayerModel> FromEntity
        {
            get => (Player player) => new BasicPlayerModel(player.Id, player.Name);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public BasicPlayerModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}