using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers
{
    class ImageHelpers
    {
        public BitmapImage GetImage(string link)
        {
            try
            {
                var imgUrl = new Uri(link);
                var imageData = new WebClient().DownloadData(imgUrl);

                var bitmapImage = new BitmapImage { CacheOption = BitmapCacheOption.OnLoad };
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(imageData);
                bitmapImage.EndInit();

                return bitmapImage;
            }
#pragma warning disable CS0168 // Переменная "e" объявлена, но ни разу не использована.
            catch (Exception e){ return null; }
#pragma warning restore CS0168 // Переменная "e" объявлена, но ни разу не использована.
        }

    }
}
