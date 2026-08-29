using Raylib_cs;
using System.Numerics;


namespace Dinozavrik
{
    internal class Program
    {
        public static bool gameover = false;
        public static bool paused = false;
        public static bool start=false;
        public static void Main() 
        {
            int ScreenHeight = Config.ScreenHeight;        
            int ScreenWidth = Config.ScreenWidth;
            Raylib.InitWindow(ScreenWidth, ScreenHeight, "Dinozavrik");
            Random rnd = new Random();
            DinoPlayer player = new DinoPlayer();
            EnemyManager manager = new EnemyManager();
            DustManager dustManager = new DustManager();
            SaveManager saveManager = new SaveManager();
            while (!Raylib.WindowShouldClose())
            {
                
                var dt = Raylib.GetFrameTime() * 50;
                
                if (Raylib.IsKeyPressed(KeyboardKey.T)) Stop();
                if (gameover && Raylib.IsKeyPressed(KeyboardKey.R))//Рестарт игры
                {
                  
                    saveManager.SaveResult(manager.Gold);
                    manager.Reset();
                    player.Reset();
                    gameover = false;
                }
                if (!gameover && !paused&&!start)//пока игра идет - происходит обновление
                {

                    player.Move(dt);     // Сначала физика
                    manager.SpawnEnemies(dt);
                    manager.UpdateEnemies(dt);
                    dustManager.Update(dt);
                    if (manager.IsPlayerColliding(player)) gameover = true;

                }
                //Рисовка
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Config.ColorBackGround);
                Raylib.DrawRectangle(0, ScreenHeight - 100, ScreenWidth, 
                                   100, Config.UnderPlayerZone);//Зона под игроком
                
                //Инфа об игре:лучший результат,стоп игра,прыжок,голда
                Raylib.DrawText("Best result:\n" + saveManager.BestGold.ToString(), 600, 70, 20, Color.White);//
                Raylib.DrawText("Stop game:T\nJump:Space", 50, 30, 20, Color.White);
                Raylib.DrawText(manager.Gold.ToString(), 700, 200, 40, Color.White);
                //обновление действий игры
                player.Show();     
                manager.ShowEnemies();
                dustManager.Show();
                if (gameover)
                {
                    Raylib.DrawText("GAMEOVER\nRestart - R",

                    (int)(ScreenWidth / 2.85),
                    ScreenHeight / 4, 40, Color.Brown);

                }
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
            saveManager.SaveResult(manager.Gold);
        }
        public static void Stop() => paused = !paused;
    }
}
