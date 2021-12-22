using System;
using System.Collections.Generic;

namespace Sokoban
{
    static class Sokoban
    {
        static public Player player;
        static public List<Entity> entities;
        static public int countMoves = 0, countInPlace = 0, allBox = 0;

        static public void InitializeGame(List<string> map)
        {
            entities = Entities.CreateListEntities(map);
            Graphics.PrintMap(entities, countMoves);
            Game();
        }

        static public void Game()
        {
            var actResult = ActResult.Nothing;

            while (actResult == ActResult.Nothing)
            {
                actResult = Act();
            }

            if (actResult == ActResult.Win)
                Console.WriteLine("Вы выиграли!");
            else
                Console.WriteLine("Вы проиграли!");
        }

        static private ActResult Act()
        {
            ConsoleKeyInfo keyPressed;
            keyPressed = Console.ReadKey();

            player.directionMove = DetermineDirection(keyPressed);
            var offsetCoordinatesPlayer = DeterminePosition(player.directionMove);
            var selectedEntity = entities[FindEntity(player.position + offsetCoordinatesPlayer)];

            if (player.directionMove == Direction.Nothing || selectedEntity is Wall)
                return ActResult.Nothing;
            else
            {
                countMoves++;
                selectedEntity.Action(player);
            }

            if (!(selectedEntity is Box))
                player.MovePlayer(offsetCoordinatesPlayer);

            Graphics.PrintMap(entities, countMoves);

            if (isWin())
                return ActResult.Win;
            if (isDefeat(entities[FindEntity(selectedEntity.position)]))
                return ActResult.Defeat;

            return ActResult.Nothing;
        }

        static public bool isWin()
        {
            if (allBox == countInPlace)
                return true;
            return false;
        }

        static public bool isDefeat(Entity selectedEntity)
        {
            if (selectedEntity is Pit ||
                (selectedEntity is Box && (entities[FindEntity(selectedEntity.position)] is Pit || isBoxStuck(selectedEntity))))
                return true;
            return false;
        }

        static public int FindEntity(Position position)
        {
            for(int index = 0; index < entities.Count; ++index)
            { 
                if (Position.CompareTo(position, entities[index].position))
                    return index;
            }

            throw new ArgumentNullException("missing element");
        }

        static public void ChangeEntity(int indexEntity1, int indexEntity2)
        {
            if (entities[indexEntity2] is PlaceBox)
            {
                entities[indexEntity2] = Entities.CreateEntity(entities[indexEntity1].name, entities[indexEntity2].position, indexEntity2);
                entities[indexEntity2].name = Char.ToUpper(entities[indexEntity2].name);
            }
            else if (!(entities[indexEntity2] is Pit))
                entities[indexEntity2] = Entities.CreateEntity(Char.ToLower(entities[indexEntity1].name), entities[indexEntity2].position, indexEntity2);

            if (char.IsUpper(entities[indexEntity1].name))
            {
                if (entities[indexEntity1] is Box)
                    countInPlace--;
                entities[indexEntity1] = Entities.CreateEntity('+', entities[indexEntity1].position, indexEntity1);
            }
            else
            {
                entities[indexEntity1] = Entities.CreateEntity(' ', entities[indexEntity1].position, indexEntity1);
            }
        }

        static public Position DeterminePosition(Direction direction)
        {
            if (direction == Direction.Up)
                return new Position(-1, 0);
            if (direction == Direction.Right)
                return new Position(0, 1);
            if (direction == Direction.Left)
                return new Position(0, -1);
            if (direction == Direction.Down)
                return new Position(1, 0);
            return new Position(0, 0);
        }

        static public Direction DetermineDirection(ConsoleKeyInfo keyPressed)
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

        static public bool isBoxStuck(Entity box)
        {
            var entityLeft = FindEntity(new Position(box.position.position1, box.position.position2 - 1));
            var entityRight = FindEntity(new Position(box.position.position1, box.position.position2 + 1));
            var entityAbove = FindEntity(new Position(box.position.position1 - 1, box.position.position2));
            var entityBelow = FindEntity(new Position(box.position.position1 + 1, box.position.position2));

            if (!(char.IsUpper(box.name)) &&
                (entities[entityAbove] is Wall && entities[entityRight] is Wall) ||
                (entities[entityAbove] is Wall && entities[entityLeft] is Wall) ||
                (entities[entityBelow] is Wall && entities[entityRight] is Wall) ||
                (entities[entityBelow] is Wall && entities[entityLeft] is Wall))
                return true;
            return false;
        }
    }
}
