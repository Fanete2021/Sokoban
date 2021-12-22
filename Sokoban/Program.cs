using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Sokoban
{
    class Program
    {
        static void Main()
        {
            var nameFile = ChoiceFile();
            var map = ReadFile(nameFile);
            Sokoban.InitializeGame(map);
        }

        static string ChoiceFile()
        {
            Console.WriteLine("Выберете один из предложенных уровней: \n");
            var directoryFiles = new DirectoryInfo(@"Levels\").GetFileSystemInfos();
            foreach(var element in directoryFiles)
            {
                Console.WriteLine(" " + element.Name.Replace(".txt", ""));
            }
            Console.WriteLine();
            var isFile = false;
            var outputString = Console.ReadLine();
            while (isFile != true)
            {
                foreach (var element in directoryFiles)
                {
                    if(string.Compare(outputString,element.Name.Replace(".txt", "")) == 0)
                    {
                        isFile = true;
                        break;
                    }
                }
                if (isFile == false)
                {
                    Console.WriteLine("Вы неправильно ввели, пробуйте заного!");
                    outputString = Console.ReadLine();
                }
            }
            
            return outputString + ".txt";
        }

        static List<string> ReadFile(string nameFile)
        {
            var level = new StreamReader(@"Levels\" + nameFile);
            var map = new List<string>();
            var line = level.ReadLine();
            while (line != null)
            {
                map.Add(line);
                line = level.ReadLine();
            }
            level.Close();
            return map;
        }
    }
}
