using Damas.DataStructures;
using Damas.Models;
using Damas.Structs;

namespace Damas.Algorithms
{
    public interface IMoveCalculator
    {
        IEnumerable<IGeneralTree<Position>> Calculate(Match match);
    }
}