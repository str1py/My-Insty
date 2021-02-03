using Instagram_Assistant.Enums;
using System;
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


        public string BigNumbersCutting(int? _number)
        {
            if (_number != null)
            {
                try
                {
                    double number = Convert.ToDouble(_number);
                    double result = 0;
                    char ch = 'K';
                    if (number > 1000)
                    {
                        int length = number.ToString().Length;
                        if (length >= 4 && length <= 6)
                        {
                            result = number / 1000;
                            ch = 'K';
                        }
                        else if (length >= 7 && length <= 9)
                        {
                            result = number / 1000000;
                            ch = 'M';
                        }
                        else if (length >= 10 && length <= 12)
                        {
                            result = number / 1000000000;
                            ch = 'B';
                        }
                        return result.ToString("0.0").Replace(',', '.') + ch;
                    }
                    else
                    {
                        return number.ToString();
                    }
                }
                catch { return _number.ToString(); }
            }
            else return null;
        }

        public string BigNumbersCutting(long? _number)
        {
            if (_number != null)
            {
                try
                {
                    double number = Convert.ToDouble(_number);
                    double result = 0;
                    char ch = 'K';
                    if (number > 1000)
                    {
                        int length = number.ToString().Length;
                        if (length >= 4 && length <= 6)
                        {
                            result = number / 1000;
                            ch = 'K';
                        }
                        else if (length >= 7 && length <= 9)
                        {
                            result = number / 1000000;
                            ch = 'M';
                        }
                        else if (length >= 10 && length <= 12)
                        {
                            result = number / 1000000000;
                            ch = 'B';
                        }
                        return result.ToString("0.0").Replace(',', '.') + ch;
                    }
                    else
                    {
                        return number.ToString();
                    }
                }
                catch { return _number.ToString(); }
            }
            else return null;
        }

        public string BigNumbersCutting(string _number)
        {
            if (_number != null)
            {
                try
                {
                    double number = Convert.ToDouble(_number);
                    double result = 0;
                    char ch = 'K';
                    if (number > 1000)
                    {
                        int length = number.ToString().Length;
                        if (length >= 4 && length <= 6)
                        {
                            result = number / 1000;
                            ch = 'K';
                        }
                        else if (length >= 7 && length <= 9)
                        {
                            result = number / 1000000;
                            ch = 'M';
                        }
                        else if (length >= 10 && length <= 12)
                        {
                            result = number / 1000000000;
                            ch = 'B';
                        }
                        return result.ToString("0.0").Replace(',', '.') + ch;
                    }
                    else
                    {
                        return number.ToString();
                    }
                }
                catch { return _number.ToString(); }
            }
            else return null;
        }
    }

}
