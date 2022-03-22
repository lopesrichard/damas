using Damas.Core.DataStructures;
using Damas.Core.Structs;

namespace Damas.Core.Algorithms
{
    public interface IMoveSelector
    {
        IEnumerable<IGeneralTree<Position>> Select(IEnumerable<IGeneralTree<Position>> source);
    }
}