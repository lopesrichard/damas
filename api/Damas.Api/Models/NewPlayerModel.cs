namespace Damas.Api.Models
{
    public class NewPlayerModel
    {
        public string Name { get; set; }

        public NewPlayerModel(string name)
        {
            Name = name;
        }
    }
}