namespace Damas.Api.Models
{
    public class UpdatePlayerModel
    {
        public string Name { get; set; }

        public UpdatePlayerModel(string name)
        {
            Name = name;
        }
    }
}