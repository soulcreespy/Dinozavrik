using Raylib_cs;
using System.Numerics;


namespace Dinozavrik { 
    internal  class Program
    {
        public static bool gameover=false;
        public static bool paused = false;
        public static void Main()
        {
            int ScreenHeight = Config.ScreenHeight;
            int ScreenWidth = Config.ScreenWidth;
            Raylib.InitWindow(ScreenWidth, ScreenHeight, "Dinozavrik");
            Random rnd=new Random();
            DinoPlayer player=new DinoPlayer(); 
            EnemyManager manager = new EnemyManager();
            DustManager dustManager = new DustManager();
            while (!Raylib.WindowShouldClose())
            {

                var dt = Raylib.GetFrameTime() * 50;
                if (Raylib.IsKeyPressed(KeyboardKey.T)) Stop();
                if (gameover && Raylib.IsKeyPressed(KeyboardKey.R))
                {

                    manager.Reset();
                    player.Reset();
                    gameover = false;
                }
                if (!gameover&&!paused)
                {
                    
                    player.Move(dt);     // Сначала физика
                    manager.SpawnEnemies(dt);
                    manager.UpdateEnemies(dt);
                    dustManager.Update(dt);
                    if (manager.IsPlayerColliding(player)) gameover = true;
                    
                }
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Config.ColorBackGround);
                    Raylib.DrawRectangle(0,ScreenHeight-100,ScreenWidth,100,Config.UnderPlayerZone);
                    Raylib.DrawText("Best result:\n"+manager.BestGold.ToString(),50,70,20,Color.White);
                    Raylib.DrawText("Stop game:T" , 50, 30, 20, Color.White);
                    player.Show();     // Потом отрисовка
                    manager.ShowEnemies();
                    dustManager.Show();

                    Raylib.DrawText(manager.Gold.ToString(), 700, 200, 40, Color.White);
                   // Raylib.DrawText(manager.Enemies.Count.ToString(), 600, 200, 40, Color.Black);
                    

                    Raylib.DrawLine(0, ScreenHeight - 100, ScreenWidth, ScreenHeight - 100, Color.Gray);
                    //                 400-100                800          400-100
                  
                    if (gameover)
                    {
                    
                        Raylib.DrawText("GAMEOVER\nRestart - R",
                        
                        (int)(ScreenWidth / 2.85),
                        ScreenHeight / 4, 40, Color.Brown);
                        
                        
                    }
                    Raylib.EndDrawing();
                    
                    
                
                 

            }

            Raylib.CloseWindow();
        }
       
        public static void Stop() => paused=!paused;
      
      
       
    }


   
}
