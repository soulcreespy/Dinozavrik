using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Dinozavrik { 
    internal  class Program
    {
        public static int size = 20;
         
        public static void Main()
        {
            Raylib.InitWindow(800, 400, "Hello World");
            int WidthScreen = Raylib.GetScreenWidth();
            int HeightScreen = Raylib.GetScreenHeight();
            Random rnd=new Random();
            Enemy enemy = new Enemy(1);
            DinoPlayer player=new DinoPlayer(); 
            EnemyManager manager = new EnemyManager();

            while (!Raylib.WindowShouldClose())
            {
                var dt = Raylib.GetFrameTime()*50;
                Raylib.DrawText(manager.GOLD.ToString(),700,200,40,Color.Black);
                player.Move(dt);     // Сначала физика
                manager.SpawnEnemies(dt);
                manager.UpdateEnemies(dt);     
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                player.Show();     // Потом отрисовка
                player.InfoShow();
                Dust(rnd);
                Raylib.DrawLine(0, HeightScreen -100 , WidthScreen, HeightScreen - 100, Color.Black);
                //                 400-100                800          400-100
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        public static void Dust(Random rnd)
        {
            int x=rnd.Next(0,800);
            int y=rnd.Next(320,380);
            Raylib.DrawRectangle(x, y,3,3, Color.Black);
        }
       
    }


   
}
