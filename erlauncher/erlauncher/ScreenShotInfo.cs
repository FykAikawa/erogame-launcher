using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace erlauncher
{
    class ScreenShotInfo
    {
        public string FilePath { get; private set; }
        public Image Thumbnail { get; private set; }

        public ScreenShotInfo(string path, int thumbWidth, int thumbHeight)
        {
            FilePath = Path.GetFullPath(path);

            using (FileStream fs = File.OpenRead(path))
            using (Image image = Image.FromStream(fs, false, false))
            {
                Thumbnail = image.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);
            }
        }
    }
}
