using Damas.Core.Enums;

namespace Damas.Api.Models
{
    public class NewMatchModel
    {
        public Guid PlayerOneId { get; set; }
        public Color PlayerOneColor { get; set; }
        public Guid PlayerTwoId { get; set; }
        public Color PlayerTwoColor { get; set; }
        public BoardSize BoardSize { get; set; }

        public NewMatchModel(Guid playerOneId, Color playerOneColor, Guid playerTwoId, Color playerTwoColor, BoardSize boardSize)
        {
            PlayerOneId = playerOneId;
            PlayerOneColor = playerOneColor;
            PlayerTwoId = playerTwoId;
            PlayerTwoColor = playerTwoColor;
            BoardSize = boardSize;
        }
    }
}