using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model.Stories
{
    class StoryModel
    {
        public string user { get; set; }
        public BitmapImage userPict { get; set; }
        public BitmapImage postPreview { get; set; } 
        public DateTime ExpiringAt { get; set; }
        public string StoryId { get; set; }
        public DateTime DeviceTimestamp { get; set; }
    }
}
