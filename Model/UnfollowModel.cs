using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model
{
    class UnfollowModel
    {
        public string user { get; set; }
        public BitmapImage userPict { get; set; }
        public long userid { get; set; }
        public bool isfollowback { get; set; }
        public string status { get; set; }
    }
}
