using Damas.DataStructures;
using Damas.Entities;
using Damas.Structs;

namespace Damas.Algorithms
{
    public class MoveCalculator : IMoveCalculator
    {
        public IDictionary<Guid, GeneralTree<Position>> Calculate(Match match)
        {
            var pieces = match.GetCurrentPlayerPieces();

            return pieces.ToList().ToDictionary(piece => piece.Id, piece => GetCalculator(piece).Calculate(match, piece));
        }

        private IPieceMoveCalculator GetCalculator(Piece piece)
        {
            return piece.IsDama ? new DamaPieceMoveCalculator() : new RegularPieceMoveCalculator();
        }
    }
}