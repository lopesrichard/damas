using Damas.Core.DataStructures;
using Damas.Core.Structs;

namespace Damas.Core.Algorithms
{
    public interface IMoveSelector
    {
        IGeneralTree<Position> Select(IGeneralTree<Position> source);
    }
}