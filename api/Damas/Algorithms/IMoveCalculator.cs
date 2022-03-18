using Damas.DataStructures;
using Damas.Entities;
using Damas.Structs;

namespace Damas.Algorithms
{
    public interface IMoveCalculator
    {
        IDictionary<Guid, IGeneralTree<Position>> Calculate(Match match);
    }
}