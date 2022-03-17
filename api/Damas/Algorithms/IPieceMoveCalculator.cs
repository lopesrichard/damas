using Damas.DataStructures;
using Damas.Entities;
using Damas.Structs;

namespace Damas.Algorithms
{
    public interface IPieceMoveCalculator
    {
        GeneralTree<Position> Calculate(Match match, Piece piece);
    }
}