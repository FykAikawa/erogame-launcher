using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace erlauncher
{
    class KeyEventArg:EventArgs
    {
        public int KeyCode { get; }

        public KeyEventArg(int keyCode)
        {
            KeyCode = keyCode;
        }
    }
}
