using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
namespace Dinozavrik
{
    public  class EnemyManager
    {
        public double timer = 2;
        public Random rnd=new Random();
        public List<Enemy>Enemies=new List<Enemy>();
        public int GOLD = 0;
        public double timeGold = 2;

        

        public void SpawnEnemies(float dt)
        {
            timeGold-=dt;
            if (timeGold <= 0)
            {
                GOLD++;
                timeGold = 2;
            }
            timer -= dt;
            if (timer <= 0&&GOLD>50)
            {
                CreateEnemy();
                timer = rnd.Next(40,60);
            }
        }

        public void UpdateEnemies(float dt)
        {
            if (GOLD >50)
            {
                Enemy enemy;
                for (int i = 0; i < Enemies.Count; i++)
                {
                    enemy = Enemies[i];
                    enemy.Move(dt);
                    enemy.Show();
                    if (enemy.posX + enemy.width * 2 <= 0) Enemies.Remove(enemy);
                }
            }
        }

        public void CreateEnemy()
        {
            int type = rnd.Next(0, 2);
            Enemy enemy = new Enemy(type);
            Enemies.Add(enemy);
        }
    }
}
