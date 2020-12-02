using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram_Assistant.Helpers
{
    class AudienceHelper:ViewModelBase
    {
        //STATUS : OK

        #region VARS INIT
        private IInstaApi _account;
        public IInstaApi Account
        {
            get { return _account; }
            set
            { 
                _account = value;
                OnPropertyChanged();
                AudiencePageViewModel.Instanse.AccountInUse = value?.GetLoggedUser().UserName ?? "loading...";
            }
        }

        private int countpassed = 0;

        private int RequestLimit { get; set; } = 10;
        private int Requests { get; set; } = 0;
        
        List<AudienceActionModel> audienceList = new List<AudienceActionModel>();
        List<AudienceModel> existAudience = new List<AudienceModel>();
        private List<InstaUserShort> followingList = new List<InstaUserShort>();
        #endregion

        #region HELPERS INIT
        private LogsPageViewModel logs = LogsPageViewModel.Instanse;
        private MainVars mainVars = new MainVars();
        private AccountInfoHelper accountInfoHelper = new AccountInfoHelper();
        private TextFileHelper txthelp = new TextFileHelper();
        private DataUpdate du = new DataUpdate();
        private Limits limits = new Limits();
        #endregion


        public async Task BeginCollectingAudience()
        {
            mainVars.IsAudienceInProgress = true;
            await GetCompetitorFollowers();
        }
        public void StopCollectingAudience()
        {
            mainVars.IsAudienceInProgress = false;
            du.UpdateProcess($"Collecting audience was stopped ", null, null, MessageType.Type.AUDIENCE);
            AudiencePageViewModel.Instanse.ButtonContent = "Start";
            audienceList.Clear();
            existAudience.Clear();
            followingList.Clear();
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
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
            }catch(Exception e)
            {
                du.UpdateProcess($"{e.Message}", followingList.Count, countpassed, MessageType.Type.ERROR);
                return false;
            }
        }

        private async Task GetCompetitorFollowers()
        {
            Account = null;
            Account = accountInfoHelper.ChangeTechAccount();
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.WORKING);

            if (Properties.Settings.Default.SaveAudiencePath != "" && Account != null)
            {
                //GET AUDIENCE FROM FILE  
                existAudience = await txthelp.GetAudienceFromTxtFileShort(Properties.Settings.Default.SaveAudiencePath);
                AudiencePageViewModel.Instanse.AudienceCount = existAudience.Count;

                du.UpdateProcess($"Begin getting competitor followers", null, null, MessageType.Type.AUDIENCE);

                foreach (var competitor in AudiencePageViewModel.Instanse.CompList)
                {
                    if (mainVars.IsAudienceInProgress == true)
                    {
                        AudiencePageViewModel.Instanse.CompetitorInUse = competitor;
                        du.UpdateProcess($"Begin getting {competitor} followers", null, null, MessageType.Type.AUDIENCE);

                        //GET COMPETITOR INFO 
                        var compet = await Account.UserProcessor.GetUserAsync(competitor);

                        if (!compet.Succeeded)
                        {
                            du.UpdateProcess($"{compet.Info.Message}", null, null, MessageType.Type.ERROR);
                            await Task.Delay(10000);
                        }
                        else
                        {
                            //GET COMPETITOR ID 
                            long UserId = compet.Value.Pk;
                            //GET COMPETITOR FOLLOWING LIST
                            followingList = await GetFollowingList(UserId);

                            if (mainVars.IsAudienceInProgress == true)
                            {
                                foreach (var user in followingList)
                                {
                                    await AddCompetitorFollowerAccount(user, competitor);
                                    Requests++;
                                    await ChangeAccountByRequestLimit(Requests);
                                }
                                du.UpdateProcess($"Getting audience from {competitor} was finished.", null, null, MessageType.Type.AUDIENCE);
                            }
                        }
                    }
                    else
                        du.UpdateProcess($"Audience wasn`t collect!  Audience actions was stopped", null, null, MessageType.Type.AUDIENCE);

                    countpassed = 0;
                    audienceList.Clear();
                }
                du.UpdateProcess($"Getting audience from all competitor was finished.", null, null, MessageType.Type.AUDIENCE);
                StopCollectingAudience();
            }
            else
            {
                AudiencePageViewModel.Instanse.AccountInUse = Account?.GetLoggedUser().UserName ?? "Failed...";
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
                            du.UpdateProcess($"{Account.GetLoggedUser().UserName} {userinfo.Info.ResponseType} {userinfo.Info.Message}", followingList.Count, countpassed, MessageType.Type.ERROR);
                        else
                        {
                            var u = userinfo.Value.UserDetail;
                            AudienceActionModel audience = new AudienceActionModel(u.Username, u.FullName, u.Pk, u.PublicPhoneNumber, u.PublicEmail, u.AccountType, u.Category, u.CityName, false,
                                Convert.ToInt32(u.MediaCount), Convert.ToInt32(userinfo.Value.UserDetail.FollowerCount),
                                u.Biography, u.HasHighlightReels, u.ProfilePicUrl, false, false, "");
                            audienceList.Add(audience);
                            countpassed++; AudiencePageViewModel.Instanse.AudienceCount++;
                            du.UpdateProcess($"Getting followers from {competitor}", followingList.Count, countpassed, MessageType.Type.HIDDEN);
                        }
                    }
                    else //IF SECCEEDED
                    {
                        var u = userinfo.Value.UserDetail;
                        //ADD TO VIEW
                        AudienceActionModel audience = new AudienceActionModel(u.Username, u.FullName, u.Pk, u.PublicPhoneNumber, u.PublicEmail, u.AccountType, u.Category, u.CityName, false,
                            Convert.ToInt32(u.MediaCount), Convert.ToInt32(userinfo.Value.UserDetail.FollowerCount),
                            u.Biography, u.HasHighlightReels, u.ProfilePicUrl, false, false, "");

                        //ADD TO LIST TI SAVE
                        audienceList.Add(audience);

                        countpassed++; AudiencePageViewModel.Instanse.AudienceCount++;
                        du.UpdateProcess($"Getting followers from {competitor}", followingList.Count, countpassed, MessageType.Type.HIDDEN);
                    }
                }
                catch (Exception e) { du.UpdateProcess($"{e.Message}", followingList.Count, countpassed, MessageType.Type.ERROR); }
            }
            else
            {
                du.UpdateProcess($"Getting followers from {competitor}", followingList.Count, countpassed, MessageType.Type.HIDDEN);
                countpassed++; 
            }
        }
        private async Task<List<InstaUserShort>> GetFollowingList(long _id)
        {
            string LatestMaxId = "0";
            followingList.Clear();
            var user = await Account.UserProcessor.GetUserInfoByIdAsync(_id);
            if (!user.Value.IsPrivate)
            {
                do
                {
                    PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                    var _result = await Account.UserProcessor.GetUserFollowersByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));

                    if (_result.Info.ResponseType == ResponseType.UnExpectedResponse)
                    {

                        du.UpdateProcess($"{_result.Info.Message}.{_result.Info.ResponseType})", followingList.Count, countpassed, MessageType.Type.ERROR);
                        StopCollectingAudience();
                    }

                    if (!_result.Succeeded)
                    {
                        await CheckAccount(_result.Succeeded, AudiencePageViewModel.Instanse.CompetitorInUse);
                        _result = await Account.UserProcessor.GetUserFollowersByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));
                    }

                    LatestMaxId = _result.Value.NextMaxId;
                    followingList.AddRange(_result.Value);

                    du.UpdateProcess($"Getting followers from {AudiencePageViewModel.Instanse.CompetitorInUse}", followingList.Count, countpassed, MessageType.Type.HIDDEN);

                    Requests++;
                    await ChangeAccountByRequestLimit(Requests);

                } while (LatestMaxId != null && mainVars.IsAudienceInProgress == true);
            }
            else
            {
                du.UpdateProcess($"{AudiencePageViewModel.Instanse.CompetitorInUse} has private account. Can`t collect audience", null, null, MessageType.Type.AUDIENCE);
                await Task.Delay(3000);
            }
            return followingList;
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
                        du.UpdateProcess($"Waiting for any account unban...", null, null,MessageType.Type.AUDIENCE);

                        await accountInfoHelper.GetAccountsStatus();
                        Account = accountInfoHelper.ChangeTechAccount();

                        await Task.Delay(limits.RestAfterChange);
                    } while (Account == null);
                    logs.Add($"Technical account has been changed to {Account.GetLoggedUser().UserName}", MessageType.Type.DEBUGINFO, this.GetType().Name);

                }
            }
        }
        private async Task ChangeAccountByRequestLimit(int count)
        {
            if (count > RequestLimit)
            {
                txthelp.SaveAudienceToTxtFile(Properties.Settings.Default.SaveAudiencePath, audienceList);
                audienceList.Clear();
                Account = await accountInfoHelper.NeededChangeTechAccount(Account);
                du.UpdateProcess($"Saving audience and changing technical account to {Account.GetLoggedUser().UserName}. Rest 40 sec", followingList.Count, countpassed,MessageType.Type.DEBUGINFO);
                logs.Add($"Changing technical account to {Account.GetLoggedUser().UserName} and rest 40 seconds", MessageType.Type.DEBUGINFO, this.GetType().Name);
                await Task.Delay(limits.RestAfterChange);
            }
        }


        
    }
}
