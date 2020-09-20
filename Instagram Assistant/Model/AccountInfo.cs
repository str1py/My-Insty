using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model
{
    public class AccountInfo
    {
        public string userName { get; set; }
        public string postCount { get; set; }
        public string followersCount { get; set; }
        public string followingCount { get; set; }
        public BitmapImage image { get; set; }
    }
}
