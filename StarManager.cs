using System.Numerics;
using Dinozavrik.Interfaces;
using Raylib_cs;
namespace Dinozavrik
{
    public  class StarManager:IDrawable,IUpdate
    {
        public Star[] starfall;
        public Random rnd=new Random();
        public StarManager()
        {
            starfall=new Star[Config.CountSandPixel];
            CreateStar();
        }
        public int ScreenWidth = Config.ScreenWidth;
        public void CreateStar()
        {
            for(int i = 0; i < Config.CountSandPixel; i++)
            {
                starfall[i] = new Star
                {
                    position = new Vector2(rnd.Next(0,ScreenWidth),
                    rnd.Next(0, Config.ScreenHeight/4)),

                    speed = new Vector2(rnd.Next(1,4), 0),
                    size = rnd.Next(2, Config.SizePixel)
                };
            }
            
        }

        public void Update(float dt)
        {
            for (int i = 0; i < Config.CountSandPixel; i++)
            {
                starfall[i].position.X -= starfall[i].speed.X*dt;
                if (starfall[i].position.X + starfall[i].size <= 0)
                {
                    starfall[i].position.X = ScreenWidth + rnd.Next(0, 21);
                    starfall[i].position.Y = rnd.Next(0, Config.ScreenHeight/4);
                }
            }
        }

        public void Draw()
        {
            for(int i = 0; i < Config.CountSandPixel; i++)
            {
                Star pixel=starfall[i];
                Raylib.DrawRectangle((int)pixel.position.X,
                (int)pixel.position.Y,
                pixel.size,
                pixel.size,
                Config.ColorSandPixel);
            }
        }


    }
    public struct Star
    {
        public Vector2 position;
        public Vector2 speed;
        public int size;
    }
}
