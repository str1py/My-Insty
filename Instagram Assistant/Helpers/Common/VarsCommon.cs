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
    class VarsCommon
    {
        #region Vars
        protected IInstaApi Account;
        protected DateTime StartTime { get; set; }
        protected DateTime EndTime { get; set; }
        //Delay per like for stats overview
        protected int Delay { get; set; }
        protected int timepass { get; set; } = 0;
        protected DispatcherTimer timer;
        #endregion

        #region Helpers
        protected ImageHelpers imghelp = new ImageHelpers();
        protected TimeHelper th = new TimeHelper();
        protected LogsPageViewModel logs = LogsPageViewModel.Instance;
        protected MainVars mainVars = new MainVars();
        protected Limits limits = new Limits();
        protected DataUpdate du = new DataUpdate();
        protected AccountInfoHelper accountInfoHelper = new AccountInfoHelper();
        protected ImageHelpers imageHelper = new ImageHelpers();
        #endregion


    }
}
