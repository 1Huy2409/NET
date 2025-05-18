using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBS.Utils
{
    public class ImageHelper
    {
        private static ImageHelper _instance;
        public static ImageHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ImageHelper();
            }
            return _instance;
        }
        public Image ResizeImage(Image imgToResize, Size size)
        {
            var resized = new Bitmap(imgToResize, size);
            return resized;
        }
    }
}
