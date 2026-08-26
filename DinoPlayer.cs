using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        
            Raylib.DrawRectangle(posX, Raylib.GetScreenHeight() -100 - (int)(posY + size * 2),
                                                            size, size * 2, Color.Black);
           //                    50    400-100-(0+20*2)

        public void InfoShow()
        {
            Raylib.DrawText((Raylib.GetScreenHeight() - 100 - (int)(posY + size * 2)).ToString(),600,150,40,Color.Black);
        }

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

        public Vector2 GetRectangleCollision() => new Vector2(posX, Raylib.GetScreenHeight() - 100 - (int)(posY + size * 2));
    
    }
}
