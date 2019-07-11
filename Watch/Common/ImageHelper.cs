using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Watch
{
    class ImageHelper
    {
        /// <summary>
        /// 从内存中加载图片，防止图片被占用
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static BitmapImage GetImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            if (File.Exists(imagePath))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                using (Stream ms = new MemoryStream(File.ReadAllBytes(imagePath)))
                {
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
            }
            else
            {
                return null;
            }
            return bitmap;
        }
    }
}
