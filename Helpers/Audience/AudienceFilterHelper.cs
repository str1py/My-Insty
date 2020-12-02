using Instagram_Assistant.Helpers.Audience;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram_Assistant.Helpers
{
    class AudienceFilterHelper
    {
        //STATUS : OK

        private string[] stopListWords { get; set; }
        private string[] goListWords { get; set; }
        private string[] wordsInNameListWords { get; set; }
        private List<AudienceModel> filteredFollowers = new List<AudienceModel>();

        private LogsPageViewModel logs = LogsPageViewModel.Instanse;
        private TextFileHelper txthelp = new TextFileHelper();
        private MainVars mainVars = new MainVars();
        private CheckRequirementsHelper check = new CheckRequirementsHelper();

        private void InitDateFromTxt()
        {
            stopListWords = txthelp.TxtToStrMassive(Properties.Settings.Default.StopWordsPath);
            goListWords = txthelp.TxtToStrMassive(Properties.Settings.Default.GoWordsPath);
            wordsInNameListWords = txthelp.TxtToStrMassive(Properties.Settings.Default.WordsInNamePath);
        }

        public async Task FilterAudience()
        {
            InitDateFromTxt();
            mainVars.IsAudienceFilterInProgress = true;
            int passed = 0;

            if (Properties.Settings.Default.SaveAudiencePath != "")
            {
                logs.Add($"Starting filter followers", MessageType.Type.AUDIENCE, this.GetType().Name);
        
                List<AudienceActionModel> list = await txthelp.GetAudienceFromTxtFile(Properties.Settings.Default.SaveAudiencePath);
                FilterAudiencePageViewModel.Instanse.LastActionTextHelper = "";

                foreach (var follower in list)
                {
                    if (mainVars.IsAudienceFilterInProgress == true)
                    {
                        await Task.Delay(2000);
                        if (check.CheckRequirements(stopListWords,goListWords,wordsInNameListWords,follower) && await check.IsFilterUserExist(follower.AccountID) == false)
                            filteredFollowers.Add(new AudienceModel { userName = follower.AccountName, userId = follower.AccountID, phone = follower.Phone, email = follower.Email });

                        passed++;

                        Save(passed);
                    }
                    else
                        logs.Add($"Audience wasn`t collect! Audience actions was stopped", MessageType.Type.AUDIENCE, this.GetType().Name);
                }

                txthelp.SaveFilterAudienceToTxtFile(Properties.Settings.Default.SaveFilteredAudiencePath, filteredFollowers);
                filteredFollowers.Clear();
                logs.Add($"Audience was filtered! Audience actions was stopped", MessageType.Type.AUDIENCE, this.GetType().Name);
                StopFilterAudience();
            }
            else
            {
                MessageBox.Show("You dont select audience file");
            }
        }

        public void StopFilterAudience()
        {
            mainVars.IsAudienceFilterInProgress = false;
        }

        private void Save(int _countpassed)
        {
            if (_countpassed % 20 == 0)
            {
                txthelp.SaveFilterAudienceToTxtFile(Properties.Settings.Default.SaveFilteredAudiencePath, filteredFollowers);
                filteredFollowers.Clear();
            }
        }

    }
}
