using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinozavrik
{

    public class SaveManager
    {
        public int BestGold { get; private set; }
        private string path = Path.Combine("source", "save.txt");
     
        public SaveManager()
        {
            Directory.CreateDirectory("source");
            if (File.Exists(path))BestGold=int.Parse(File.ReadAllText(path));
            else
            {
                BestGold = 0;
                File.WriteAllText(path, "0");
            }
        }
        public void SaveResult(int gold)
        {
            if (gold > BestGold)
            {
                BestGold = gold;
                File.WriteAllText(path, BestGold.ToString());
            }
            
        }
  
    }
}
