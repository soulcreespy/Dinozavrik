using Dinozavrik.Interfaces;
using Raylib_cs;
using System.Numerics;

namespace Dinozavrik
{
    public class Enemy
    {
        public readonly int width;
        public readonly int height;
        public readonly float speed = 10;
        public readonly int type;
        public float posX = (Raylib.GetScreenWidth());
        public Texture2D texture;
        public int posY;
        public void LoadTexture(int width,int height)
        {
           
            Image image = Raylib.LoadImage(Path.Combine("assets", "enemy.png"));
            
            Raylib.ImageResize(ref image, width, height);

            texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image);
        }
        public Enemy(int t)
        {
            
            type = t;
            
            switch (type)
            {
                case 0:
                    width = 40;
                    height = 60;
                    LoadTexture(width, height);
                    break;
                case 1:
                    width = 30;
                    height = 50;
                    LoadTexture(width, height);
                    break;
            }
            posY = Raylib.GetScreenHeight() - 100 - height;
        }

        public void Update(float dt)
        {
            posX -= speed*dt;
        }

        public void Show()=>
            Raylib.DrawTexture(texture,(int)posX, posY,Color.White);
        public void ShowCollision()=>Raylib.DrawRectangle((int)posX,posY,width,height,Color.White);
        
        public Rectangle GetRectangleCollision() => new Rectangle(
            posX, 
            posY,
            new Vector2(width,height));
    }
}
