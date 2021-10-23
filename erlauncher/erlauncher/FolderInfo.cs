using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace erlauncher
{
    public class FolderInfo
    {
        public string Name { set; get; }
        public List<GameInfo> GameList { set; get; } = new List<GameInfo>();
        public FolderInfo(string name)
        {
            Name = name;
        }

        private FolderInfo() { }

        public void AddGame(GameInfo gi)
        {
            GameList.Add(gi);
        }

        public void AddGame(string dName, string path)
        {
            var gi = new GameInfo(dName, path);
            GameList.Add(gi);
        }
    }
}
