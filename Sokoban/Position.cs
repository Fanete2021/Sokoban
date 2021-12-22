namespace Sokoban
{
    class Position
    {
        public int position1, position2;

        public Position(int position1, int position2)
        {
            this.position1 = position1;
            this.position2 = position2;
        }

        public static Position operator +(Position position1, Position position2)
        {
            return new Position(position1.position1 + position2.position1, position1.position2 + position2.position2);
        }

        public static bool CompareTo(Position position1, Position position2)
        {
            if (position1.position1 == position2.position1 && position1.position2 == position2.position2)
                return true;

            return false;
        }
    }
}
