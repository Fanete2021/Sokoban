using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    abstract class Entity
    {
        public char name;
        abstract public void Action(Entity entity);
    }

    class OtherEntity: Entity
    {
        public OtherEntity(char name)
        {
            this.name = name;
        }

        public override void Action(Entity entity)
        {

        }
    }

    class Player: Entity
    {
        public int coordinate1, coordinate2;
        public int countMoves = 0;

        public Direction directionMove = Direction.Nothing;

        public override void Action(Entity entity)
        {
            ConsoleKeyInfo keyPressed;
            keyPressed = Console.ReadKey();
            countMoves++;
            directionMove = DetermineDirection(keyPressed);
        }

        public (int, int) DetermineCoordinate()
        {
            if (directionMove == Direction.Up)
                return (-1, 0);
            if (directionMove == Direction.Right)
                return (0, 1);
            if (directionMove == Direction.Left)
                return (0, -1);
            if (directionMove == Direction.Down)
                return (1, 0);
            return (0, 0);
        }

        static private Direction DetermineDirection(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.UpArrow)
                return Direction.Up;
            if (keyPressed.Key == ConsoleKey.RightArrow)
                return Direction.Right;
            if (keyPressed.Key == ConsoleKey.LeftArrow)
                return Direction.Left;
            if (keyPressed.Key == ConsoleKey.DownArrow)
                return Direction.Down;
            return Direction.Nothing;
        }

        public void MovePlayer()
        {
            var offset = Sokoban.player.DetermineCoordinate();
            Sokoban.ChangeListEntity(coordinate1, coordinate2, coordinate1 + offset.Item1, coordinate2 + offset.Item2);
        }
    }

    class PlaceBox: Entity
    {
        public PlaceBox(char name)
        {
            this.name = name;
        }

        public override void Action(Entity entity)
        {
            if (entity is Box)
            {
                Box.countInPlace++;
            }
        }
    }

    class Box : Entity
    {
        static public int countInPlace = 0, allBox = 0;
        private int coordinate1, coordinate2;
        public Box(char name, int coordinate1, int coordinate2)
        {
            this.name = name;
            this.coordinate1 = coordinate1;
            this.coordinate2 = coordinate2;
            if(Sokoban.player.countMoves == 0)
                allBox++;
        }

        public override void Action(Entity entity)
        {
            var offset = Sokoban.player.DetermineCoordinate();
            if (Sokoban.entity[coordinate1 + offset.Item1][coordinate2 + offset.Item2].name != '#' && char.ToLower(Sokoban.entity[coordinate1 + offset.Item1][coordinate2 + offset.Item2].name) != 'o')
            {
                Sokoban.entity[coordinate1 + offset.Item1][coordinate2 + offset.Item2].Action(this);
                Sokoban.ChangeListEntity(coordinate1, coordinate2, coordinate1 + offset.Item1, coordinate2 + offset.Item2);
                Sokoban.ChangeListEntity(coordinate1 - offset.Item1, coordinate2 - offset.Item2, coordinate1, coordinate2);
                coordinate1 += offset.Item1;
                coordinate2 += offset.Item2;
            }
        }
    }

    class Pit : Entity
    {
        public Pit(char name)
        {
            this.name = name;
        }

        public override void Action(Entity entity)
        {

        }
    }
}
