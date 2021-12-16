using System;
using System.Collections.Generic;

namespace Sokoban
{
    static class Sokoban
    {
        static public Player player;
        static public List<List<Entity>> entity;
        static public int countMoves = 0;

        static public void InitializeGame(List<List<char>> map)
        {
            FillListEntities(map);
            PrintMap();

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

        static private void FillListEntities(List<List<char>> map)
        {
            entity = new List<List<Entity>>();

            for (var index1 = 0; index1 < map.Count; ++index1)
            {
                entity.Add(new List<Entity>());
                for (var index2 = 0; index2 < map[index1].Count; ++index2)
                {
                    entity[index1].Add(CreateEntity(map[index1][index2], index1, index2));
                }
            }
        }

        static private Entity CreateEntity(char entity, int index1, int index2)
        {
            switch (entity)
            {
                case '+':
                    return new PlaceBox(entity);
                case 'o':
                    return new Box(entity, index1, index2);
                case '?':
                    return new Pit(entity);
                case '#':
                    return new Wall(entity);
                case 'p':
                    player = new Player(entity, index1, index2);
                    return player;
                default:
                    return new Void(entity);
            }
        }

        static private void PrintMap()
        {
            Console.Clear();
            for (var index1 = 0; index1 < entity.Count; ++index1)
            {
                for (var index2 = 0; index2 < entity[index1].Count; ++index2)
                {
                    Console.Write(entity[index1][index2].name);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nСчётчик победы " + Box.countInPlace + "/" + Box.allBox + "\nСчётчик ходов " + countMoves);
        }

        static private ActResult Act()
        {
            player.Action(null);
            var coordinatesPlayer = player.DetermineCoordinate();
            var selectedEntity = entity[player.coordinate1 + coordinatesPlayer.Item1][player.coordinate2 + coordinatesPlayer.Item2];

            if (player.directionMove == Direction.Nothing || selectedEntity.name == '#')
                return ActResult.Nothing;
            else
            {
                selectedEntity.Action(player);
            }

            countMoves++;

            if (!(selectedEntity is Box))
                player.MovePlayer();
            //else if(Box.isDefeat((Box)selectedEntity))


            PrintMap();

            if (Box.allBox == Box.countInPlace)
                return ActResult.Win;
            if (selectedEntity is Pit)
                return ActResult.Defeat;

            return ActResult.Nothing;
        }

        static public void ChangeListEntity(int coordinate1, int coordinate2, int coordinate3, int coordinate4)
        {
            if (entity[coordinate3][coordinate4] is PlaceBox)
            {
                entity[coordinate3][coordinate4] = CreateEntity(entity[coordinate1][coordinate2].name, coordinate3, coordinate4);
                entity[coordinate3][coordinate4].name = char.ToUpper(entity[coordinate3][coordinate4].name);
            }
            else if (!(entity[coordinate3][coordinate4] is Pit))
                entity[coordinate3][coordinate4] = CreateEntity(char.ToLower(entity[coordinate1][coordinate2].name), coordinate3, coordinate4);

            if (entity[coordinate1][coordinate2].name == 'P' || entity[coordinate1][coordinate2].name == 'O')
            {
                if (entity[coordinate1][coordinate2].name == 'O')
                    Box.countInPlace--;
                entity[coordinate1][coordinate2] = CreateEntity('+', coordinate1, coordinate2);
            }
            if (entity[coordinate1][coordinate2].name == 'p' || entity[coordinate1][coordinate2].name == 'o')
                entity[coordinate1][coordinate2] = CreateEntity(' ', coordinate1, coordinate2);
        }
    }
}
