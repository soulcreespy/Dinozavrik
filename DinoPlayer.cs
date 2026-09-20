using Dinozavrik.Interfaces;
using Raylib_cs;

using System.Numerics;

namespace Dinozavrik
{
    public class DinoPlayer:IDrawable,IUpdate,IShowCollision
    {

        
        private float posY = 0;
       
        private const float posX = 50;
        private  float velY=0; // Текущая скорость
        private const float gravity = 0.6f;    // Меньше гравитация = медленнее падает
        private const float jumpForce = 12.0f;  // Больше сила = выше прыгает
        public const int size = 20;
        public Texture2D dino;
        public int WidthFrame;
        public int HeightFrame;
        int currentFrame = 0;
        float FrameTime = 0.0f;
        const float updateTime = 0.2f;
        public Rectangle dinoRec;
        private bool jumpRequested = false;
        public DinoPlayer(string path)
        {
            Image ex = Raylib.LoadImage(path);
            
            Raylib.ImageResize(ref ex, ex.Width/6, ex.Height/6);
            dino = Raylib.LoadTextureFromImage(ex);
            Raylib.UnloadImage(ex);
            if (File.Exists(path) == false) Console.WriteLine("ошибка такой текстурки нету");
             WidthFrame = dino.Width / 2;
             HeightFrame = dino.Height;
            dinoRec = new Rectangle(
            WidthFrame,
            0,
            WidthFrame, HeightFrame);

        }
        public void Draw()=>Raylib.DrawTextureRec(dino,dinoRec,new Vector2(
                posX, Raylib.GetScreenHeight() - 107 - (posY + size * 2)),Color.White);
            
        
        //                    50    400-100-(0+20*2)
  
        public void Reset()=>posY = 0;
        public int PosX()=>(int)posX;
        public void ShowCollision()=>
             Raylib.DrawRectangleRec(GetRectangleCollision(), Config.ColorPlayer);
        public void SubscribeToJump(EnemyManager enemyManager)=>enemyManager.JumpPosted += TryJump;
        public void UnSubscribeToJump(EnemyManager enemyManager)=>enemyManager.JumpPosted-= TryJump;

        private void TryJump()
        {
           
            if (posY <=0)
            {
                jumpRequested = true;
             
            }
        }
        public void Update(float dt)
        {
           
            FrameTime += dt;
            if (FrameTime > updateTime&&posY<=0)
            {
                FrameTime -= updateTime;
                currentFrame++;
                if (currentFrame >= 2) currentFrame = 0;
                dinoRec.X = currentFrame * WidthFrame;

            }

            if ( Raylib.IsKeyDown(KeyboardKey.Space) && posY <= 0)
                velY= jumpForce;

            
            if (jumpRequested && posY <= 0)
            {
                velY = jumpForce-2;
                jumpRequested = false;
            }
                if (Raylib.IsKeyReleased(KeyboardKey.Space) && velY > 0)
                velY /= 2;
            velY -= gravity * dt;
            posY += velY * dt;
          
            // Гравитация только если в воздухе
            if (posY <= 0)
            {
                posY = 0;
                velY = 0;
            }
        }
        
        public Rectangle GetRectangleCollision() => new Rectangle(
            posX, 
            Raylib.GetScreenHeight()-98-posY-HeightFrame,
            WidthFrame-10, HeightFrame-10);
    
    }
}
