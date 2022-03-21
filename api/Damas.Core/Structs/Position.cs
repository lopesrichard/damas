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
    }
}