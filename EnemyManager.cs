using Raylib_cs;
namespace Dinozavrik
{
    public  class EnemyManager
    {
        public double timer = 2;
        public Random rnd=new Random();
        public List<Enemy>Enemies=new List<Enemy>();
        public int BestGold {  get; private set; }
        public int Gold { get; private set; }
        public double timeGold = 2; 
        public void SpawnEnemies(float dt)
        {

            timeGold -= dt;
            if (timeGold <= 0)
            {
                Gold++;
                timeGold = 2;
            }
            if (Gold > 50)
            {
                timer -= dt;
                if (timer <= 0)
                {
                    CreateEnemy();
                    timer = rnd.Next(30, 60);
                }
            }
        }
        public void Reset()
        {
            if (Gold > BestGold) BestGold = Gold;
            Enemies.Clear();
            Gold = 0;
            timer = 2;
            timeGold = 2;
        }
        public void UpdateEnemies(float dt)
        {
            if (Gold >50)
            {
                Enemy enemy;
                for (int i = Enemies.Count - 1; i >= 0; i--)
                {
                    enemy = Enemies[i];
                    enemy.Move(dt);
                    
                    if (enemy.posX + enemy.width * 2 <= 0) Enemies.Remove(enemy);
                }
            }
        }
       
        public void ShowEnemies()
        {
            if(Gold>50)
                foreach (Enemy enemy in Enemies)
            
                    enemy.Show();
            
        }

        public void CreateEnemy()
        {
            int type = rnd.Next(0, 2);
            Enemy enemy = new Enemy(type);
            Enemies.Add(enemy);
        }

        public bool IsPlayerColliding(DinoPlayer player)
        {
            foreach (var enemy in Enemies)
            {
                if (Raylib.CheckCollisionRecs(enemy.GetRectangleCollision(),
                    player.GetRectangleCollision()))return true;
            }
            return false;
        }
    }
}
