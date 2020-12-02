using Instagram_Assistant.Enums;
using System.Windows;

namespace Instagram_Assistant.Helpers
{
    public class ConvertHelper
    {

        //COMMON HELPER + THIS -- THINKING
        public Visibility CoverterBoolToVisibility(bool value)
        {
            if (value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public int AccTypeToInt(string accType)
        {
            if (accType == "Personal")
                return 0;
            else if (accType == "Author")
                return 2;
            else return 1;
        }

        public bool YesNoToBoolean(string value)
        {
            if (value == "Yes")
                return true;
            else if (value == "No")
                return false;
            else return false;

        }

        public AccountStatus.Type ResponseToAccountType(string response)
        {
            if (response == "LoginRequired")
                return AccountStatus.Type.NEEDSLOGIN;
            else if (response == "ChallangeRequired")
                return AccountStatus.Type.NEEDSCHALLENGE;
            else if (response == "Spam")
                return AccountStatus.Type.SPAM;
            else if (response == "Seccusses")
                return AccountStatus.Type.REST;
            else return AccountStatus.Type.REST;
        }
    }

}
