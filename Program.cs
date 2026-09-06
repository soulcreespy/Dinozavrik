using Dinozavrik.Interfaces;
using Raylib_cs;
using System.Numerics;


namespace Dinozavrik
{
    internal class Program
    {
        public static bool gameover = false;
        public static bool paused = false;
        public static bool start=false;
        public static bool showCollision = false;
        public static bool turn=false;
        public static void Main() 
        {
            int ScreenHeight = Config.ScreenHeight;        
            int ScreenWidth = Config.ScreenWidth;
            Raylib.InitWindow(ScreenWidth, ScreenHeight, "Dinozavrik"); 
            DinoPlayer player = new DinoPlayer(Path.Combine("assets","Dino2.png"));
            EnemyManager managerEnemies = new EnemyManager();
            StarManager StarManager = new StarManager();
            SaveManager saveManager = new SaveManager();
            MoonManager MoonManager = new MoonManager(3,3,4);
            List<IDrawable> drawables = new() { player,StarManager,managerEnemies};
            List<IUpdate> updatables = new(){player,StarManager , managerEnemies };
            List<IShowCollision> ShowCollisions = new() { player, managerEnemies };
            while (!Raylib.WindowShouldClose())
            {
                float dt = Raylib.GetFrameTime()*50;
                if (Raylib.IsKeyPressed(KeyboardKey.T)) 
                    Stop();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Config.ColorBackGround);
                MoonManager.Draw();
                Raylib.DrawRectangle(
                    0,
                    ScreenHeight - 100,
                    ScreenWidth,
                    100,
                    Config.UnderPlayerZone
                ); 

                //логика
                if (!start)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.Space))
                        start = true;
                   
                }
                else
                {
                   
                    if (!gameover && !paused)
                    {
                        foreach (IUpdate obj in updatables) obj.Update(dt);
                        
                        if (managerEnemies.IsPlayerColliding(player))
                        {
                            gameover = true;
                        }
                    }
                    if (gameover && Raylib.IsKeyPressed(KeyboardKey.R))
                    {
                        saveManager.SaveResult(managerEnemies.Gold);
                        managerEnemies.Reset();
                        player.Reset();
                        gameover = false;
                    }
                     
                }

                //рисовка
                if (!start)
                {

                    player.Draw();

                    Raylib.DrawText(
                        "Press space to play",
                        ScreenWidth / 2,
                        ScreenHeight / 2,
                        30,
                        Color.Gray
                    );
                }
                else
                {
                    foreach (IDrawable obj in drawables) obj.Draw();
                    
                    if (Raylib.IsKeyPressed(KeyboardKey.F4))
                        showCollision = !showCollision;

                    if (showCollision)
                    {
                        foreach (IShowCollision obj in ShowCollisions) obj.ShowCollision();
                    }
                        Raylib.DrawText(
                        "Best result:\n" + saveManager.BestGold,
                        ScreenWidth - 200,
                        ScreenHeight / 4,
                        20,
                        Color.White
                    );

                    Raylib.DrawText(
                        "Stop game:T\nJump:Space",
                        50,
                        30,
                        20,
                        Color.White
                    );

                    Raylib.DrawText(
                        managerEnemies.Gold.ToString(),
                        ScreenWidth - 100,
                        ScreenHeight / 2,
                        40,
                        Color.White
                    );

                    if (gameover)
                    {
                        Raylib.DrawText(
                            "GAMEOVER\nRestart - R",
                            (int)(ScreenWidth / 2.85f),
                            ScreenHeight / 4,
                            40,
                            Color.Red
                        );
                    }
                }
                    Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
            saveManager.SaveResult(managerEnemies.Gold);
        }
        public static void Stop() => paused = !paused;
    }
}
