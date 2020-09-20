using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Model
{
    public class FeedLikeStatsModel
    {
        public string Status { get; set; }
        public int? Likes { get; set; }
        public int? SessionLikes { get; set; }
        public int? NextLikeIn { get; set; }
        public string LikeGoal { get; set; }
        public string TimeInWork { get; set; }
        public string NextSessionIn { get; set; }
    }
}
