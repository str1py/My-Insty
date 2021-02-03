using System;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model
{
    public class ActionModel
    {
        public BitmapImage AccountImage { get; set; }
        public string AccountName { get; set; }
        public long AccountID { get; set; }
        public BitmapImage PostImage { get; set; }
        public string Action { get; set; }
        public string Status { get; set; } // for unfollow
        public DateTime Time { get; set; } //for sort
    }
}
