using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace erlauncher
{
    public class GameInfo
    {
        public int id { set; get; }
        public string displayName { set; get; }
        public string Path { set; get; }
        public string imagepath { set; get; }
        public GameInfo(string dName,string path)
        {
            displayName = dName;
            Path = path;
        }
        private GameInfo() { }
    }
}
