namespace Damas.Core.Structs
{
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj) => obj is Position position && Equals(position);

        public bool Equals(Position p) => X == p.X && Y == p.Y;

        public override int GetHashCode() => (X, Y).GetHashCode();

        public static bool operator ==(Position a, Position b) => a.Equals(b);

        public static bool operator !=(Position a, Position b) => !a.Equals(b);

        public Position Northwest(int squares = 1)
        {
            return new Position(X - squares, Y + squares);
        }

        public Position Northeast(int squares = 1)
        {
            return new Position(X + squares, Y + squares);
        }

        public Position Southwest(int squares = 1)
        {
            return new Position(X - squares, Y - squares);
        }

        public Position Southeast(int squares = 1)
        {
            return new Position(X + squares, Y - squares);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public static Position Parse(string source)
        {
            var position = source.Split(",").Select(position => int.Parse(position));

            return new Position(position.ElementAt(0), position.ElementAt(1));
        }

        public int Distance(Position position)
        {
            return Distance(this, position);
        }

        public IEnumerable<Position> Between(Position position)
        {
            return Between(this, position);
        }

        public static int Distance(Position a, Position b)
        {
            return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
        }

        public static IEnumerable<Position> Between(Position a, Position b)
        {
            var positions = new HashSet<Position>();

            for (var i = 1; i < Distance(a, b); i++)
            {
                var X = a.X + (a.X > b.X ? -i : +i);
                var Y = a.Y + (a.Y > b.Y ? -i : +i);

                positions.Add(new Position(X, Y));
            }

            return positions;
        }
    }
}