using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Model
{
    public class StatsModelBase
    {
        public string Status { get; set; }
        public string Count { get; set; }
        public string SessionCount { get; set; }
        public int? NextIn { get; set; }
        public string TimeInWork { get; set; }
        public string NextSessionIn { get; set; }
    }
}
