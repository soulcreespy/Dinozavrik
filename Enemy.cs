using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;
namespace Dinozavrik
{
    public class Enemy
    {
        public readonly int width;
        public readonly int height;
        public readonly float speed = 10;
        public readonly int type;
        public float posX = (Raylib.GetScreenWidth());
      
        public Enemy(int t)
        {
            type = t;
            switch (type)
            {
                case 0:
                    width = 30;
                    height = 60;
                    break;
                case 1:
                    width = 60;
                    height = 30;
                    break;
            }
        }

        public void Move(float dt)
        {
            posX -= speed*dt;
        }

        public void Show()=>
            Raylib.DrawRectangleRec(GetRectangleCollision(), Color.Black);
        

        public Rectangle GetRectangleCollision() => new Rectangle(
            posX + width, 
            Raylib.GetScreenHeight() - 100 - height,
            new Vector2(width,height));




    }
}
