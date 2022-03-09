using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class ReadingFile
    {
        static public string ChoiceFile()
        {
            var directoryFiles = new DirectoryInfo(@"Levels\").GetFileSystemInfos();

            Console.WriteLine("Выберете один из предложенных уровней: \n");
            foreach (var element in directoryFiles)
                Console.WriteLine(" " + element.Name.Replace(".txt", ""));
            Console.WriteLine();

            var isFile = false;
            var outputString = Console.ReadLine() + ".txt";
            while (!isFile)
            {
                foreach (var element in directoryFiles)
                {
                    if (outputString.CompareTo(element.Name) == 0)
                    {
                        isFile = true;
                        break;
                    }
                }
                if (!isFile)
                {
                    Console.WriteLine("Вы неправильно ввели, поробуйте заного!");
                    outputString = Console.ReadLine() + ".txt";
                }
            }

            return outputString;
        }

        static public List<string> ReadFile(string nameFile)
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
