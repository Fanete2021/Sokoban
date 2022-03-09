namespace Game
{
    public class Position
    {
        public int Y, X;

        public Position(int y, int x)
        {
            Y = y;
            X = x;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.Y + b.Y, a.X + b.X);
        }

        public static bool CompareTo(Position a, Position b)
        {
            return a.Y == b.Y && a.X == b.X;
        }
    }
}
