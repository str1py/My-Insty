using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System.Collections.ObjectModel;

namespace Instagram_Assistant.ViewModel
{
    public class LogsPageViewModel
    {
        private static LogsPageViewModel loginstance;
        public static LogsPageViewModel Instance
        {
            get
            {
                if (loginstance == null)              
                    loginstance = new LogsPageViewModel();         
                return loginstance;
            }
        }

        public ObservableCollection<LogMessageModel> ActionList { get; set; }
        public LogsPageViewModel()
        {
            ActionList = new ObservableCollection<LogMessageModel>();
        }

        public void Add(string message, MessageType.Type type, string _class)
        {
            ActionList.Add(new LogMessageModel
            {
                date = new TimeHelper().GetTimeNow(),
                type = type.ToString(),
                message = message,
                detailedtype = GetDetailedByClass(_class),
                icon = GetIconByType(type)
            });

            if(type != MessageType.Type.DEBUGINFO || type != MessageType.Type.HIDDEN)
                MainWindowViewModel.instance.Alert(GetIconByType(type), GetDetailedByClass(_class), message);
        }

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
            else if (classname == "GeoLikeHelper" || classname == "GeoLikePageViewModel")
                return "Geo Like";
            else if (classname == "HashtagLikeHelper" || classname == "HashtagLikePageViewModel")
                return "Hashtag Like";
            else if (classname == "ImageHelper")
                return "Image Helper";
            else if (classname == "LoginPageViewModel" || classname == "LoginHelper")
                return "Login";
            else if (classname == "StoriesFeedHelper" || classname == "FeedStoriesPageViewModel")
                return "Stories Watch";
            else if (classname == "GeoStoriesHelper" || classname == "GeoStoriesWatchViewModel")
                return "Stories Watch";
            else if (classname == "AudienceHelper" || classname == "AudiencePageViewModel")
                return "Getting Audience";
            else if (classname == "Update")
                return "Technical info";
            else return "Info";
        }
    }
}
