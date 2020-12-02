using Instagram_Assistant.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model
{
    public class AccountInfo
    {
        public string userName { get; set; }
        public string postCount { get; set; }
        public string followersCount { get; set; }
        public string followingCount { get; set; }
        public int accountRole { get; set; }
        public AccountStatus.Type accountStatus { get; set; }
        public BitmapImage image { get; set; }
        public Visibility LoginReqVisibility { get; set; }
        public Visibility ChallengeReqVisibility { get; set; }

    }
}
