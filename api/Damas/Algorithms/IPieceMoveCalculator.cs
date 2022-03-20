using Damas.DataStructures;
using Damas.Models;
using Damas.Structs;

namespace Damas.Algorithms
{
    public interface IPieceMoveCalculator
    {
        IGeneralTree<Position> Calculate(Match match, Piece piece);
    }
}