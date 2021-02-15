using Instagram_Assistant.ViewModel;
using System;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers
{
    class ImageHelpers
    {
        public BitmapImage GetImage(string link)
        {
            if (link != null)
            {
                try
                {
                    var imgUrl = new Uri(link);
                    var imageData = new WebClient().DownloadData(imgUrl);

                    var bitmapImage = new BitmapImage { CacheOption = BitmapCacheOption.OnLoad };
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(imageData);
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    return bitmapImage;
                }
                catch (Exception e)
                {
                    LogsPageViewModel.Instance.Add(e.Message, MessageType.Type.ERROR, this.GetType().Name);
                    return null;
                }
            } return null;
        }
    }
}
