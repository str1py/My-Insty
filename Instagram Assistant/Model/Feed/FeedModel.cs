using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model
{
    class FeedModel
    {
        public string user { get; set; }
        public BitmapImage userPict { get; set; }
        public BitmapImage postPreview { get; set; }
        public string InstaIdentifier { get; set; }
        public string MediaType { get; set; }
        public long userid { get; set; }
    }
}
