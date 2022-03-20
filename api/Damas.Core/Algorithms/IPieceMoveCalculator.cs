using Damas.Core.DataStructures;
using Damas.Core.Models;
using Damas.Core.Structs;

namespace Damas.Core.Algorithms
{
    public interface IPieceMoveCalculator
    {
        IGeneralTree<Position> Calculate(Match match, Piece piece);
    }
}