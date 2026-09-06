using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinozavrik.Interfaces;
using Raylib_cs;
namespace Dinozavrik
{
   public class MoonManager:IDrawable
    {
        public Moon sun;
        public MoonManager(float x,float y,float speed)
        {
            sun = new Moon
            {
                X = x,
                Y = y,
                Speed=speed
            };
        }
        public void Draw() => Raylib.DrawCircle(Config.ScreenWidth/2,Config.ScreenHeight/4,40,Color.White);
    }
    public struct Moon
    {
       public float X, Y;
        public float Speed;
    }
}
