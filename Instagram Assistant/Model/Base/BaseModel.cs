using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model.Base
{
    class BaseModel
    {
        public string user { get; set; }
        public BitmapImage userPict { get; set; }
        public BitmapImage postPreview { get; set; }
        public long userid { get; set; }
    }
}
