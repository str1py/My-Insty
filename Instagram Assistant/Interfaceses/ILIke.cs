using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Interfaceses
{
    interface ILIke
    {
        // IInstaApi Account;
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }

        int Delay { get; set; }
        int timepass { get; set; }

        Task BeginLike();
        void StopLike();
        bool SkipPost();
    }

}
