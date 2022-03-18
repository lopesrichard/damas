using Damas.DataStructures;
using Damas.Structs;

namespace Damas.Algorithms
{
    public interface IMoveSelector
    {
        IGeneralTree<Position> Select(IGeneralTree<Position> source);
    }
}