using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;

namespace Sokoban
{
    static class SokobanTests
    {
        static List<Entity> entities;

        static public void CreateMap(string nameFile)
        {
            var level = new StreamReader(@"Levels\" + nameFile);
            var map = new List<string>();
            var line = level.ReadLine();
            while (line != null)
            {
                map.Add(line);
                line = level.ReadLine();
            }
            entities = Entities.CreateListEntities(map);
        }

        static public ActResult Game(List<Direction> commands)
        {
            ActResult actResult = ActResult.Nothing;
            foreach(var command in commands)
            {
                var offsetPosition = Sokoban.DeterminePosition(command);
                var selectedEntity = Sokoban.entities[Sokoban.FindEntity(Sokoban.player.position + offsetPosition)];

                if (!(selectedEntity is Box))
                    Sokoban.player.MovePlayer(offsetPosition);

                if (Sokoban.isWin())
                {
                    actResult = ActResult.Win;
                    break;
                }
                if (Sokoban.isDefeat(selectedEntity))
                {
                    actResult = ActResult.Defeat;
                    break;
                }
            }

            return actResult;
        }
    }

    [TestFixture]
    public class SokobanTest
    {
        [Test]
        public void BoxStuck_InputCommands_ReturnDefeat()
        {
            var file = "1.txt";
            var commands = new List<Direction>(new Direction[] { Direction.Up, Direction.Right, Direction.Right, Direction.Right });
            var expectedResult = ActResult.Defeat;

            SokobanTests.CreateMap(file);
            var result = SokobanTests.Game(commands);

            Assert.AreEqual(result, expectedResult);
        }

        [TestCase(Direction.Up)]
        [TestCase(Direction.Right, Direction.Up, Direction.Up, Direction.Left)]
        public void InterectionPit_InputCommands_ReturnDefeat(Direction[] args)
        {
            var file = "2.txt";
            var commands = new List<Direction>(args);
            var expectedResult = ActResult.Defeat;

            SokobanTests.CreateMap(file);
            var result = SokobanTests.Game(commands);

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void BoxInPlace_InputCommands_ReturnWin()
        {
            var file = "3.txt";
            var commands = new List<Direction>(new Direction[] { Direction.Up, Direction.Up});
            var expectedResult = ActResult.Win;

            SokobanTests.CreateMap(file);
            var result = SokobanTests.Game(commands);

            Assert.AreEqual(result, expectedResult);
        }
    }

}
