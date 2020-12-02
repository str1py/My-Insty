using Instagram_Assistant.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers.Audience
{
    class CheckRequirementsHelper
    {
        //STATUS : OK

        private TextFileHelper txthelp = new TextFileHelper();
        private DataUpdate du = new DataUpdate();

        public bool CheckRequirements(string[] stopListWords, string[] goListWords, string[] wordsInNameListWords, AudienceActionModel ui)
        {
            bool result = false;
            string act;
            bool city = CountryCheck(ui.City);
            bool acctype = AccountTypeCkeck(ui.AccountType);
            bool goword = GoWordsCheck(goListWords, ui.Bio, ui.FullName);
            bool stopword = StopWordsCheck(stopListWords,ui.Bio, ui.FullName);
            bool wordsinname = WordsInNameCheck(wordsInNameListWords,ui.AccountName);
            bool category = CategotyCheck(ui.AccountCategory);
            bool hl = false;

            if (ui.HasHighlight == "Yes")
                hl = true;


            if (Properties.Settings.Default.HasBioParameter)
            {
                if (acctype == true && ui.HasBio == "Yes" && ui.MediaCount > Properties.Settings.Default.MinMediaCount
                    && ui.FollowersCount > Properties.Settings.Default.MinFollowersCount && city != false && stopword != true)
                    result = true;
            }
            if (Properties.Settings.Default.HasHLparameter)
            {

                if (acctype == true && ui.MediaCount > Properties.Settings.Default.MinMediaCount && ui.HasHighlight == "Yes"
                    && ui.FollowersCount > Properties.Settings.Default.MinFollowersCount && city != false && stopword != true)
                    result = true;

            }
            if (Properties.Settings.Default.HasImageParameter)
            {

                if (acctype == true && ui.MediaCount > Properties.Settings.Default.MinMediaCount && ui.HasImage == "Yes"
                    && ui.FollowersCount > Properties.Settings.Default.MinFollowersCount && city != false && stopword != true)
                    result = true;

            }


            if (!stopword)
            {
                if (goword)
                    result = true;

                if (!result)
                {
                    if (category == true && acctype == true && stopword != true)
                        result = true;
                }

                if (!result)
                {
                    if (wordsinname)
                        result = true;
                }

            }
            else result = false;


            if (!result)
                act = "UserTimes";
            else act = "UserPlus";

            du.UpdateFilterAudienceActions(ui.AccountName, ui.FullName, ui.AccountID, ui.Phone, ui.Email, AccountTypeConvertToInt(ui.AccountType), ui.AccountCategory, ui.City,
                ui.IsCity, ui.MediaCount, ui.FollowersCount, ui.Bio, hl, ui.ImageLink, stopword, goword, act);

            return result;

        }

        private bool StopWordsCheck(string[] stopListWords,string bio, string fullname)
        {
            if (stopListWords != null)
            {
                foreach (var word in stopListWords)
                {
                    if (bio.ToLower().Contains(word) || fullname.ToLower().Contains(word))
                        return true;
                }
                return false;
            }
            else return false;
        }
        private bool GoWordsCheck(string[] goListWords, string bio, string fullname)
        {
            if (goListWords != null)
            {
                foreach (var word in goListWords)
                {
                    if (bio.ToLower().Contains(word) || fullname.ToLower().Contains(word))
                        return true;
                }
                return false;
            }
            else return false;
        }
        private bool WordsInNameCheck(string[] wordsInNameListWords, string name)
        {
            if (wordsInNameListWords != null)
            {
                foreach (var word in wordsInNameListWords)
                {
                    if (name.ToLower().Contains(word))
                        return true;
                }
                return false;
            }
            else return false;
        }
        private bool AccountTypeCkeck(string acctype)
        {
            string type = IntConvertToAccountType(Properties.Settings.Default.AccountTypeFilter);
            if (acctype == type)
                return true;

            return false;
        }
        private int AccountTypeConvertToInt(string acctype)
        {
            if (acctype == "Personal")
                return 1;
            else if (acctype == "Business")
                return 2;
            else // 3 - author
                return 3;
        }
        private string IntConvertToAccountType(int acctype)
        {
            acctype++;
            if (acctype == 1)
                return "Personal";
            else if (acctype == 2)
                return "Business";
            else
                return "Author";
        }
        private bool CountryCheck(string info)
        {
            string[] cities = new string[] { "Belarus", "Ukraine" };
            if (info != null && info != "")
            {
                foreach (var city in cities)
                {
                    if (info.Contains(city))
                        return false;
                }
                return true;
            }
            else return true;
        }
        private bool CategotyCheck(string categoty)
        {
            string[] neededcategories = Properties.Settings.Default.AccountCategoriesFilter.Split(',');
            string[] sep = categoty?.Split(',');

            if (sep?.Count() > 1)
            {
                foreach (var word in neededcategories)
                {
                    foreach (var sepword in sep)
                    {
                        if (sepword == word)
                            return true;
                    }
                }
            }
            else if (sep?.Count() == 1)
            {
                foreach (var word in neededcategories)
                {
                    if (categoty == word)
                        return true;
                }
            }
            else return false;


            return false;
        }
        public async Task<bool> IsFilterUserExist(long id)
        {
            var existUsers = await txthelp.GetAudienceFromTxtFileShort(Properties.Settings.Default.SaveFilteredAudiencePath);
            if (existUsers.Count != 0)
            {
                foreach (var user in existUsers)
                {
                    if (user != null)
                    {
                        string userStr = $"{user?.userName};{user?.userId};{user?.email};{user?.phone}";
                        if (userStr.Contains(id.ToString()))
                            return true;
                    }
                }
                return false;
            }
            return false;
        }
    }
}
