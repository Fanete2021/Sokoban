using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Graphic;

namespace Game
{
    class Program
    {
        static void Main()
        {
            var nameFile = ReadingFile.ChoiceFile();
            var map = ReadingFile.ReadFile(nameFile);
            Graphics.InitializeGame(new Sokoban(map));
        }
    }
}
