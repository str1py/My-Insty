using Instagram_Assistant.ViewModel;
using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    class CommonForAll
    {
        protected IInstaApi Account;
        protected DateTime StartTime { get; set; }
        protected DateTime EndTime { get; set; }

        //Delay per like for stats overview
        protected int Delay { get; set; }
        protected int timepass { get; set; } = 0;

        protected DispatcherTimer timer;

        protected ImageHelpers imghelp = new ImageHelpers();
        protected CommonHelper helper = new CommonHelper();
        protected TimeHelper th = new TimeHelper();
        protected LogsPageViewModel logs = LogsPageViewModel.Instanse;
        protected MainVars mainVars = new MainVars();
        protected Limits limits = new Limits();
        protected DataUpdate du = new DataUpdate();
        protected AccountInfoHelper accountInfoHelper = new AccountInfoHelper();

        protected void TimeInit()
        {
            timepass = 0;
            timer = new DispatcherTimer();
            StartTime = DateTime.Now;
            EndTime = helper.GetNextTime(StartTime); // time to next session after four
        }
    }
}
