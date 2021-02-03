using Instagram_Assistant.Helpers;
using System;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Model
{
    class AudienceActionModel : ActionModel
    {
        public AudienceActionModel(string accountName,string fullname, long accid,string phone, string email, int type, string category, string city, bool iscity, int mediacount, 
            int followerscount, string bio,bool hl, string image, bool stopword, bool goWords, string action)
        {
            ImageHelpers imageHelper = new ImageHelpers();
            AccountImage = imageHelper.GetImage(image) ?? new BitmapImage(new Uri("pack://application:,,,/Resources/Images/instagram.png"));
            ImageLink = image;
            AccountID = accid;
            AccountName = accountName;
            FullName = RemoveSimbols(fullname);
            Time = DateTime.Now;
            ActionConvert(action);
            AccountTypeConvert(type);
            FollowersCountConvert(followerscount);
            MediaCountConvert(mediacount);
            AccountCategotyConvert(category);
            HasBioConvert(RemoveSimbols(bio));
            HasHighlightConvert(hl);
            HasImageConvert(image);
            HasStopWordConvert(stopword);
            HasGoWordConvert(goWords);
            CityConvert(iscity,city);
            Phone = phone;
            Email = email;
        }

        private string YesColor = "Green";
        private string NoColor = "Red";
        private string MayBeColor = "Yellow";

        public string ActionColor { get; set; }

        public string AccountType { get; set; }//
        public string AccountTypeColor { get; set; }//

        public int FollowersCount { get; set; }//
        public string FollowersCountColor { get; set; }//

        public int MediaCount { get; set; }//
        public string MediaCountColor { get; set; }//

        public string AccountCategory { get; set; }//
        public string AccountCategoryColor { get; set; }//

        public string HasBio { get; set; }//
        public string Bio { get; set; }
        public string FullName { get; set; }
        public string HasBioColor { get; set; }//

        public string HasHighlight { get; set; }//
        public string HasHighlightColor { get; set; }//

        public string HasImage { get; set; }//
        public string ImageLink { get; set; }
        public string HasImageColor { get; set; }//

        public string HasStopWord { get; set; }//
        public string HasStopWordColor { get; set; }//
        public string HasGoWord { get; set; }
        public string HasGoWordColor { get; set; }

        public string City { get; set; }//
        public bool IsCity { get; set; }
        public string CityColor { get; set; }//

        public string Phone { get; set; }
        public string Email { get; set; }

        private void AccountTypeConvert(int type)
        {
            if (type == 1)
            {
                AccountType = "Personal";
                AccountTypeColor = NoColor;
            }
            else if (type == 2)
            {
                AccountType = "Business";
                AccountTypeColor = YesColor;
            }
            else
            {
                AccountType = "Author";
                AccountTypeColor = NoColor;
            }
        }
        private void FollowersCountConvert(int count)
        {
            FollowersCount = count;
            if (count < 100)
                FollowersCountColor = NoColor;
            else
                FollowersCountColor = YesColor;
        }
        private void MediaCountConvert(int count)
        {
            MediaCount = count;
            if (count < 15)
                MediaCountColor = NoColor;
            else
                MediaCountColor = YesColor;
        }
        private void AccountCategotyConvert(string category)
        {
            AccountCategoryColor = MayBeColor;
            string[] AccountCategories = Properties.Settings.Default.AccountCategoriesFilter.Split(',');
            foreach(var _category in AccountCategories)
                if(category == _category)
                    AccountCategoryColor = YesColor;

            if(category == null || category == "")
            {
                AccountCategory = "Unknown";
                AccountCategoryColor = MayBeColor;
            }

        }

        private void HasBioConvert(string bio)
        {

            if (bio != null || bio != "")
            {
                HasBio = "Yes";
                HasBioColor = YesColor;
                Bio = bio;
            }
            else
            {
                HasBio = "No";
                HasBioColor = NoColor;
                Bio = "No Bio";
            }
        }
        private void HasHighlightConvert(bool hl) 
        {
            if (hl)
            {
                HasHighlight = "Yes";
                HasHighlightColor = YesColor;
            }
            else
            {
                HasHighlight = "No";
                HasHighlightColor = NoColor;
            }
        }
        private void HasImageConvert(string image)
        {
            if (image!=null || image != "")
            {
                HasImage = "Yes";
                HasImageColor = YesColor;
            }
            else
            {
                HasImage = "No";
                HasImageColor = NoColor;
            }
        }
        private void HasStopWordConvert(bool stopword) 
        {
            if (stopword)
            {
                HasStopWord = "Yes";
                HasStopWordColor = NoColor;
            }
            else
            {
                HasStopWord = "No";
                HasStopWordColor = YesColor;
            }
        }

        private void HasGoWordConvert(bool goword)
        {
            if (goword)
            {
                HasGoWord = "Yes";
                HasGoWordColor = YesColor;
            }
            else
            {
                HasGoWord = "No";
                HasGoWordColor = MayBeColor;
            }
        }
        private void CityConvert(bool iscity,string city)
        {
            IsCity = iscity;
            if (iscity && city !=null && city != "")
            {
                City = city;
                CityColor = YesColor;
            }
            else
            {
                if (string.IsNullOrEmpty(city) || city == null || city == "")
                {
                    City = "Unknown";
                    CityColor = MayBeColor;
                }
                else CityColor = NoColor;
            }



        }

        private void ActionConvert(string act)
        {
            if (act == "UserTimes")
            {
                Action = "UserTimes";
                ActionColor = NoColor;
            }
            else
            {
                Action = "UserPlus";
                ActionColor = YesColor;
            }
        }

        private string RemoveSimbols(string str)
        {
            string strRemoved = str.Replace(';', ' ');
            return strRemoved;
        }


    }
}
