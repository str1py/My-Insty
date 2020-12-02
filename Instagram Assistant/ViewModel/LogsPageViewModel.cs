using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.ViewModel
{
    class LogsPageViewModel:ViewModelBase, INotifyCollectionChanged
    {
        private static LogsPageViewModel loginstance;
        public static LogsPageViewModel Instanse
        {
            get
            {
                if (loginstance == null)              
                    loginstance = new LogsPageViewModel();         
                return loginstance;
            }
        }

        public static ObservableCollection<LogMessageModel> ActionList { get; set; }
        public LogsPageViewModel()
        {
            ActionList = new ObservableCollection<LogMessageModel>();
        }

        private TimeHelper th = new TimeHelper();

        public void Add(string message, MessageType.Type type, string _class)
        {

            string icon = GetIconByType(type);
            string detailedtype = GetDetailedByClass(_class);
            ActionList.Add(new LogMessageModel
            {
                date = th.GetTimeNow(),
                type = type.ToString(),
                message = message,
                detailedtype = detailedtype,
                icon = icon
            });
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }
        public void Add(Exception e, MessageType.Type type, Type from)
        {
            ActionList.Add(new LogMessageModel
            {
                date = th.GetTimeNow(),
                type = type.ToString(),
                message = e.Message + " Error in" + from.Name
            });
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }


        //TIMEHELPER
  
        private string GetIconByType(MessageType.Type type)
        {
            if (type == MessageType.Type.ERROR)
                return "ExclamationTriangle";
            else if (type == MessageType.Type.DEBUGINFO)
                return "InfoCircle";
            else if (type == MessageType.Type.LIKE)
                return "Heart";
            else if (type == MessageType.Type.STORY)
                return "eye";
            else if (type == MessageType.Type.UNFOLLOW)
                return "UserTimes";
            else if (type == MessageType.Type.FOLLOW)
                return  "UserCheck";
            else if (type == MessageType.Type.AUDIENCE)
                return "Users";
            else return "ExclamationTriangle";
        }

        private string GetDetailedByClass(string classname)
        {
            if (classname == "UnfollowHelper")
                return "Unfollow";
            else if (classname == "FeedLikeHelper" || classname == "FeedLikePageViewModel")
                return "Feed Like";
            else if (classname == "ImageHelper")
                return "Image Helper";
            else if (classname == "LoginPageViewModel" || classname == "LoginHelper")
                return "Login";
            else if (classname == "StoriesFeedHelper" || classname == "FeedStoriesPageViewModel") 
                return "Stories Watch";
            else if(classname == "AudienceHelper" || classname == "AudiencePageViewModel")
                return "Getting Audience";
            else return "????";
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
    }
}
