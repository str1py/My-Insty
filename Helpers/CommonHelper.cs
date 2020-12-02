using Instagram_Assistant.ViewModel;
using System;


namespace Instagram_Assistant.Helpers
{
    class CommonHelper : ViewModelBase
    {

        //Calculate Deley
        public int GetLikeDelay()
        {
            int delay = Properties.Settings.Default.DelayValue; // standart delay
            Random rnd = new Random();
            //if 0 - standart delay + random else - 
            if (rnd.Next(0, 1) == 0)
                delay += rnd.Next(0, Properties.Settings.Default.RandomDelayValue);
            else
                delay -= rnd.Next(0, Properties.Settings.Default.RandomDelayValue);

            return delay * 1000; // * for miliseconds for Task.Delay()
        }
        public int GetStoryDelay()
        {
            int delay = Properties.Settings.Default.StoriesDelay; // standart delay
            Random rnd = new Random();
            //if 0 - standart delay + random else - 
            if (rnd.Next(0, 1) == 0)
                delay += rnd.Next(0, Properties.Settings.Default.RandomStoriesDelay);
            else
                delay -= rnd.Next(0, Properties.Settings.Default.RandomStoriesDelay);

            return delay * 1000; // * for miliseconds for Task.Delay()
        }

        public int GetUnfollowDelay()
        {
            int delay = 120; // standart delay
            Random rnd = new Random();
            //if 0 - standart delay + random else - 
            if (rnd.Next(0, 1) == 0)
                delay += rnd.Next(0, 20);
            else
                delay -= rnd.Next(0, 20);

            return delay * 1000; // * for miliseconds for Task.Delay()
        }

        //Get next activity time
        public DateTime GetNextTime(DateTime start)
        {
            return start.AddMinutes(60);
        }

        public DateTime GetEndTime()
        {
            DateTime date = new DateTime();
            TimeSpan ts = new TimeSpan(22, 00, 0);
            date = date.Date + ts;
            return date;
        }

        public string BigNumbersCutting(int? _number)
        {
            if (_number != null)
            {
                try
                {
                    double number = Convert.ToDouble(_number);
                    double result = 0;
                    if (number > 1000)
                    {
                        int length = number.ToString().Length;
                        if (length == 4)
                            result = number / 1000;
                        else if (length == 5)
                            result = number / 10000;
                        else if (length == 6)
                            result = number / 100000;

                        return result.ToString("0.0").Replace(',', '.') + "k";
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
                if (_number.Contains("k"))
                    return _number;
                else
                {
                    try
                    {
                        double number = Convert.ToDouble(_number);
                        double result = 0;
                        if (number > 1000)
                        {
                            int length = number.ToString().Length;
                            if (length == 4)
                                result = number / 1000;
                            else if (length == 5)
                                result = number / 10000;
                            else if (length == 6)
                                result = number / 100000;

                            return result.ToString("0.0").Replace(',', '.') + "k";
                        }
                        else
                        {
                            return number.ToString();
                        }
                    }
                    catch { return _number; }
                }
            }
            else return null;
        }

    }
}
