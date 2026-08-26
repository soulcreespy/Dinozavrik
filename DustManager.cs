using System.Numerics;
using Raylib_cs;
namespace Dinozavrik
{
    public  class DustManager
    {
        public SandPixel[] dust;
        public Random rnd=new Random();
        public DustManager()
        {
            dust=new SandPixel[Config.CountSandPixel];
            CreateSand();
        }
        public int ScreenWidth = Config.ScreenWidth;
        public void CreateSand()
        {
            for(int i = 0; i < Config.CountSandPixel; i++)
            {
                dust[i] = new SandPixel
                {
                    position = new Vector2(rnd.Next(ScreenWidth, ScreenWidth + 20),
                    rnd.Next(320, 380)),

                    speed = new Vector2(rnd.Next(5, 20), 0),
                    size = rnd.Next(2, Config.SizePixel)
                };
            }
        }

        public void Update(float dt)
        {
            for (int i = 0; i < Config.CountSandPixel; i++)
            {
                dust[i].position.X -= dust[i].speed.X*dt;
                if (dust[i].position.X + dust[i].size <= 0)
                {
                    dust[i].position.X = ScreenWidth + rnd.Next(0, 21);
                    dust[i].position.Y = rnd.Next(320, 380);
                }
            }
        }

        public void Show()
        {
            for(int i = 0; i < Config.CountSandPixel; i++)
            {
                SandPixel pixel=dust[i];
                Raylib.DrawRectangle((int)pixel.position.X,
                (int)pixel.position.Y,
                pixel.size,
                pixel.size,
                Config.ColorSandPixel);
            }
        }


    }
    public struct SandPixel
    {
        public Vector2 position;
        public Vector2 speed;
        public int size;
    }
}
