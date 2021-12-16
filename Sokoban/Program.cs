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
            var isHave = false;
            var outputString = Console.ReadLine();
            while (isHave != true)
            {
                foreach (var element in directoryFiles)
                {
                    if(string.Compare(outputString,element.Name.Replace(".txt", "")) == 0)
                    {
                        isHave = true;
                        break;
                    }
                }
                if (isHave == false)
                {
                    Console.WriteLine("Вы неправильно ввели, пробуйте заного!");
                    outputString = Console.ReadLine();
                }
            }
            
            return outputString + ".txt";
        }

        static List<List<char>> ReadFile(string nameFile)
        {
            var level = new StreamReader(@"Levels\" + nameFile);
            var map = new List<List<char>>();
            var line = level.ReadLine();
            while (line != null)
            {
                map.Add(CreateListChars(line));
                line = level.ReadLine();
            }
            level.Close();
            return map;
        }

        static List<char> CreateListChars(string line)
        {
            var listChars = new List<char>();
            for(var index = 0; index < line.Length; ++index)
            {
                listChars.Add(line[index]);
            }
            return listChars;
        }
    }
}
