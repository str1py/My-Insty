using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    sealed class AudienceHelper: VarsCommon
    {
        //STATUS : NOT OK
        public AudienceHelper()
        {
            stats = (new AudienceStatsModel{
                TechAccount = "n/a",
                Status = "OFF",
                Competitor = "n/a",
                Count = "0",
                TimeInWork = "00:00:00"
            });
        }

        private AudienceStatsModel stats;

        private List<AudienceActionModel> audienceList = new List<AudienceActionModel>();
        private List<AudienceModel> existAudience = new List<AudienceModel>();
        private List<InstaUserShort> followersList = new List<InstaUserShort>();

        private TextFileHelper txthelp = new TextFileHelper();
        private DataUpdate da = new DataUpdate();

        private int requests;
        public int Requests
        {
            get { return requests; }
            set { requests = value;}
        }

        private int CompetitorFollowersPassed;
           
        public async Task BeginCollectingAudience()
        {
            timer = new DispatcherTimer();
            timerStart();
            Account = await accountInfoHelper.GetTechAccountAsync();
            if (Account != null)
            {
                mainVars.IsAudienceInProgress = true;
                stats = da.AudienceStatsUpdate(stats, AccountStatus.Type.WORKING.ToString(), existAudience.Count, null, Account.GetLoggedUser().UserName, null); ;
                accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.WORKING);
                await GetCompetitorFollowers();
            }else
            {
                StopCollectingAudience();
                logs.Add("Cant get tech account!", MessageType.Type.ERROR, this.GetType().Name);
            }
        }
        public void StopCollectingAudience()
        {
            timerStop();
            mainVars.IsAudienceInProgress = false;
            du.UpdateProcess($"Collecting audience has been stopped", null, null, MessageType.Type.AUDIENCE,this.GetType().Name);
            AudiencePageViewModel.Instance.ButtonContent = "Start";
            audienceList.Clear();
            existAudience.Clear();
            followersList.Clear();
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            du.AudienceStatsUpdate(stats, AccountStatus.Type.OFF.ToString(), 0, "00:00:00", "", "");
        }

        private bool IsUserExist(string name, List<AudienceModel> existAudience)
        {
            try
            {
                if (existAudience != null)
                {
                    foreach (var existuser in existAudience)
                    {
                        if (existuser?.userName == name)
                            return true;
                    }
                }
                return false;
            }
            catch(Exception e)
            {
                du.UpdateProcess($"{e.Message}", followersList.Count, Double.Parse(stats.Count), MessageType.Type.ERROR, this.GetType().Name);
                return false;
            }
        }

        private async Task GetCompetitorFollowers()
        {        
            if (Properties.Settings.Default.SaveAudiencePath != "" && Account != null)
            {
                //GET AUDIENCE FROM FILE  
                existAudience = await txthelp.GetAudienceFromTxtFileShort(Properties.Settings.Default.SaveAudiencePath);
                stats = da.AudienceStatsUpdate(stats,null,existAudience.Count,null,null,null);

                du.UpdateProcess($"Begin getting competitor followers", null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                foreach (var competitor in AudiencePageViewModel.Instance.CompList)
                {
                    if (mainVars.IsAudienceInProgress == true)
                    {
                        stats = da.AudienceStatsUpdate(stats, null, null, null, null, competitor);
                        du.UpdateProcess($"Begin getting {competitor} followers", null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                        //GET COMPETITOR INFO 
                        var compet = await Account.UserProcessor.GetUserAsync(competitor);
                        CompetitorFollowersPassed = 0;

                        if (!compet.Succeeded)
                        {
                            du.UpdateProcess($"{compet.Info.Message}", null, null, MessageType.Type.ERROR, this.GetType().Name);
                            await Task.Delay(10000);
                        }
                        else
                        {
                            //GET COMPETITOR ID 
                            long UserId = compet.Value.Pk;
                            //GET COMPETITOR FOLLOWING LIST
                            followersList = await GetFollowersListByUserId(UserId);

                            if (mainVars.IsAudienceInProgress == true)
                            {
                                foreach (var user in followersList)
                                    await AddCompetitorFollowerAccount(user, competitor);
                                
                                du.UpdateProcess($"Getting audience from {competitor} was finished.", null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                            }
                        }
                    }
                    else
                        du.UpdateProcess($"Audience wasn`t collect!  Audience actions was stopped", null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                    Requests = 0;
                    audienceList.Clear();
                }
                await ChangeAccountByRequestLimit();
                du.UpdateProcess($"Getting audience from all competitor was finished.", null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                StopCollectingAudience();
            }
            else
            {
                stats = da.AudienceStatsUpdate(stats, null, null, null, null, null);
                MessageBox.Show($"Path to save {Properties.Settings.Default.SaveAudiencePath ?? "Not selected"}.\n Technical account: {Account?.GetLoggedUser().UserName ?? "No tech account"}","Error!",MessageBoxButton.OK,MessageBoxImage.Error);
                StopCollectingAudience();
            }
        }
        private async Task AddCompetitorFollowerAccount(InstaUserShort _user, string competitor)
        {
            if (!IsUserExist(_user.UserName, existAudience))
            {
                await Task.Delay(3000);
                try
                {
                    var userinfo = await Account.UserProcessor.GetFullUserInfoAsync(_user.Pk);

                    if (!userinfo.Succeeded) //IF NOT SECCEEDED
                    {
                        //CHANGE ACCOUNT
                        await CheckAccount(userinfo.Succeeded, competitor);
                        userinfo = await Account.UserProcessor.GetFullUserInfoAsync(_user.Pk);

                        if (!userinfo.Succeeded)
                            du.UpdateProcess($"{Account.GetLoggedUser().UserName} {userinfo.Info.ResponseType} {userinfo.Info.Message}", followersList.Count, Requests, MessageType.Type.ERROR, this.GetType().Name);
                        else
                        {
                            var u = userinfo.Value.UserDetail;

                            AudienceActionModel audience = new AudienceActionModel(u.Username, u.FullName, u.Pk, u.PublicPhoneNumber, u.PublicEmail, u.AccountType, u.Category, u.CityName, false,
                            Convert.ToInt32(u.MediaCount), Convert.ToInt32(userinfo.Value.UserDetail.FollowerCount),u.Biography, u.HasHighlightReels, u.ProfilePicUrl, false, false, "");

                            audienceList.Add(audience);

                            stats = da.AudienceStatsUpdate(stats, null, Int32.Parse(stats.Count) + 1, null, null, null);
                            du.UpdateProcess($"Getting followers from {competitor}", followersList.Count, CompetitorFollowersPassed++ , MessageType.Type.HIDDEN, this.GetType().Name);
                        }
                    }
                    else //IF SECCEEDED
                    {
                        var u = userinfo.Value.UserDetail;
                        //ADD TO VIEW
                        AudienceActionModel audience = new AudienceActionModel(u.Username, u.FullName, u.Pk, u.PublicPhoneNumber, u.PublicEmail, u.AccountType, u.Category, u.CityName, false,
                            Convert.ToInt32(u.MediaCount), Convert.ToInt32(userinfo.Value.UserDetail.FollowerCount),u.Biography, u.HasHighlightReels, u.ProfilePicUrl, false, false, "");
                        //ADD TO LIST TO SAVE
                        audienceList.Add(audience);

                        stats = da.AudienceStatsUpdate(stats, null, Int32.Parse(stats.Count) + 1, null, null, null);
                        du.UpdateProcess($"Getting followers from {competitor}", followersList.Count, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                    }
                    Requests++;
                    await ChangeAccountByRequestLimit();

                }
                catch (Exception e) { du.UpdateProcess($"{e.Message}", followersList.Count, CompetitorFollowersPassed++, MessageType.Type.ERROR, this.GetType().Name); }
            }
            else
            {
                du.UpdateProcess($"Getting followers from {competitor}", followersList.Count, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
            }
        }
     
        private async Task<List<InstaUserShort>> GetFollowersListByUserId(long _id)
        {
            string LatestMaxId = "0";
            followersList.Clear();
            var user = await Account.UserProcessor.GetUserInfoByIdAsync(_id);
            if (!user.Value.IsPrivate)
            {
                do
                {
                    PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(2);
                    var _result = await Account.UserProcessor.GetUserFollowersByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));

                    #region Checkers
                    if (_result.Info.ResponseType == ResponseType.UnExpectedResponse)
                    {
                        du.UpdateProcess($"{_result.Info.Message}.{_result.Info.ResponseType})", followersList.Count, Double.Parse(stats.Count), MessageType.Type.ERROR, this.GetType().Name);
                        StopCollectingAudience();
                        MessageBox.Show("UnExpectedResponse Error. May be there are too many followers on this account. Sorry.","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
                        break;
                    }
                    if (!_result.Succeeded)
                    {
                        await CheckAccount(_result.Succeeded, stats.Competitor);
                        _result = await Account.UserProcessor.GetUserFollowersByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));
                    }
                    #endregion

                    LatestMaxId = _result.Value.NextMaxId;
                    followersList.AddRange(_result.Value);

                    du.UpdateProcess($"Getting followers from {stats.Competitor}", followersList.Count, Double.Parse(stats.Count), MessageType.Type.HIDDEN, this.GetType().Name);
                    Requests++;
                    await ChangeAccountByRequestLimit();

                } while (LatestMaxId != null && mainVars.IsAudienceInProgress == true);
            }
            else
            {
                du.UpdateProcess($"{stats.Competitor} has private account. Can`t collect audience", null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                await Task.Delay(3000);
            }
            return followersList;
        }

        private async Task CheckAccount(bool result,string competitor)
        {
            if(!result)
            {
                Account = await accountInfoHelper.ChangeTechAccount(Account, competitor);
                if (Account == null)
                {
                    logs.Add($"All tech accounts banned", MessageType.Type.DEBUGINFO, this.GetType().Name);
                    do
                    {
                        du.UpdateProcess($"Waiting for any account unban...", null, null,MessageType.Type.AUDIENCE, this.GetType().Name);

                        await accountInfoHelper.GetAccountsStatus();
                        Account = await accountInfoHelper.GetTechAccountAsync();

                        await Task.Delay(limits.RestAfterChange);
                    } while (Account == null);
                    logs.Add($"Technical account has been changed to {Account.GetLoggedUser().UserName}", MessageType.Type.DEBUGINFO, this.GetType().Name);

                }
            }
        }
        private async Task ChangeAccountByRequestLimit()
        {
            if (Requests > Properties.Settings.Default.ActionsPerTechAccountLimit == true)
            {
                Account = await accountInfoHelper.NeededChangeTechAccount(Account);
                du.UpdateProcess($"Saving audience and changing technical account to {Account.GetLoggedUser().UserName}. Rest 40 sec", followersList.Count, CompetitorFollowersPassed++, MessageType.Type.DEBUGINFO, this.GetType().Name);
                await Task.Delay(40000);
                txthelp.SaveAudienceToTxtFile(Properties.Settings.Default.SaveAudiencePath, audienceList);
                audienceList.Clear();
                Requests = 0;
            }
        }


        private void timerStart()
        {
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Start();
        }
        private void timerStop()
        {
            Delay = 0;
            timer?.Stop();
        }
        private void timerTick(object sender, EventArgs e)
        {
            timepass++;
            stats = du.AudienceStatsUpdate(stats, null, null, th.GetNormalTime(timepass), null, null);
        }
    }
}
