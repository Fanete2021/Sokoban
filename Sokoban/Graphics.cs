using System;
using System.Collections.Generic;

namespace Sokoban
{
    static class Graphics
    {
        static public void PrintMap(List<Entity> entities, int countMoves)
        {
            Console.Clear();
            int lastRow = 0;
            foreach(var entity in entities)
            {
                if(lastRow != entity.position.position1)
                    Console.WriteLine();
                lastRow = entity.position.position1;
                Console.Write(entity.name);
            }
            Console.WriteLine("\n\nСчётчик победы " + Sokoban.countInPlace + "/" + Sokoban.allBox + "\nСчётчик ходов " + countMoves);
        }
    }
}
