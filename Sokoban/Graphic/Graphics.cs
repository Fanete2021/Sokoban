using System;
using System.Collections.Generic;
using SFML.Graphics;
using Game;
using System.Threading;

namespace Graphic
{
    static class Graphics
    {
        static RenderWindow form;
        static private ActResult actResult = ActResult.Nothing;
        static public void InitializeGame(Sokoban sokoban)
        {
            var countRow = sokoban.ListEntities[sokoban.ListEntities.Count - 1].Position.Y + 1;
            var countLines = sokoban.ListEntities[sokoban.ListEntities.Count - 1].Position.X + 1;
            form = new RenderWindow(new SFML.Window.VideoMode((uint)countRow * 200, (uint)countLines * 100), "Sokoban");
            form.SetVerticalSyncEnabled(true);

            form.Closed += CloseForm;

            var map = new Map(sokoban);


            while (form.IsOpen && actResult == ActResult.Nothing)
            {
                form.DispatchEvents();
                actResult = sokoban.Act();
                form.Clear(Color.White);
                form.Draw(map);
                form.Display();
                Thread.Sleep(50);
            }

            Console.Clear();
            if (actResult == ActResult.Win)
                Console.WriteLine("Вы выиграли!");
            else
                Console.WriteLine("Вы проиграли!");
        }

        private static void CloseForm(object sender, EventArgs e)
        {
            form.Close();
        }

    }
}
