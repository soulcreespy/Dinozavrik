using Raylib_cs;

using System.Numerics;

namespace Dinozavrik
{
    public class DinoPlayer
    {
        private float posY = 0;
        private const int posX = 50;
        private  float velY=0; // Текущая скорость
        private const float gravity = 0.6f;    // Меньше гравитация = медленнее падает
        private const float jumpForce = 12.0f;  // Больше сила = выше прыгает
        public const int size = 20;
        

        public void Show()=>
        
            Raylib.DrawRectangleRec(GetRectangleCollision(), Config.ColorPlayer);
        //                    50    400-100-(0+20*2)
        public void Reset()=>posY = 0;
        

        public void Move(float dt)
        {

            if (Raylib.IsKeyDown(KeyboardKey.Space) && posY <= 0)
                velY = jumpForce;
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
        
        public Rectangle GetRectangleCollision() => new Rectangle(posX, 
            Raylib.GetScreenHeight() - 100 - (posY + size * 2),
            new Vector2(size,size*2));
    
    }
}
