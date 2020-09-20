using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model
{
    public class FeedLikeModel
    {
        public BitmapImage AccountImage { get; set; }
        public string AccountName { get; set; }
        public BitmapImage PostImage { get; set; }
        public string Action { get; set; }
        public DateTime likeTime { get; set; }
    }
}
