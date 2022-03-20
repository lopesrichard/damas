using Damas.Entities;
using Damas.Enums;
using Damas.Exceptions;
using Damas.Structs;

namespace Damas.Validators
{
    public class PositionValidator
    {
        public IEnumerable<Exception> Validate(BoardSize size, Position position)
        {
            var exceptions = new List<Exception>();

            var odds = new List<int>() { 1, 3, 5, 7 };
            var evens = new List<int>() { 0, 2, 4, 6 };

            if (size == BoardSize.ONE_HUNDRED_SQUARES)
            {
                odds.Add(9);
                evens.Add(8);
            }

            var numbers = odds.Concat(evens);

            if (!numbers.Contains(position.X))
            {
                exceptions.Add(new CoordinateOutOfBoundException());
            }

            if (!numbers.Contains(position.Y))
            {
                exceptions.Add(new CoordinateOutOfBoundException());
            }

            if (odds.Contains(position.X) && evens.Contains(position.Y))
            {
                exceptions.Add(new NonPlayablePositionException());
            }

            if (evens.Contains(position.X) && odds.Contains(position.Y))
            {
                exceptions.Add(new NonPlayablePositionException());
            }

            return exceptions;
        }
    }
}