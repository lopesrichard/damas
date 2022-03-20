using Damas.DataStructures;
using Damas.Models;
using Damas.Structs;

namespace Damas.Algorithms
{
    public class MoveCalculator : IMoveCalculator
    {
        public IEnumerable<IGeneralTree<Position>> Calculate(Match match)
        {
            var pieces = match.GetPlayerPieces();

            return pieces.ToList().Select(piece => GetCalculator(piece).Calculate(match, piece));
        }

        private IPieceMoveCalculator GetCalculator(Piece piece)
        {
            return piece.IsDama ? new DamaPieceMoveCalculator() : new RegularPieceMoveCalculator();
        }
    }
}