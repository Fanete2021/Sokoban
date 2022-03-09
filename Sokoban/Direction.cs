using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum Directions
    {
        Right,
        Left,
        Up,
        Down,
        Nothing
    }

    public static class Direction
    {
        public static Position DeterminePosition(Directions direction)
        {
            return direction switch
            {
                Directions.Up => new Position(-1, 0),
                Directions.Down => new Position(1, 0),
                Directions.Right => new Position(0, 1),
                Directions.Left => new Position(0, -1),
                _ => new Position(0, 0),
            };
        }
    }
}
