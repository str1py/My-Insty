using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers.Common
{
    class AudienceCommon : VarsCommon
    {
        protected AudienceStatsModel stats;
        protected int Requests;
        protected AudienceViewModelBase mainInstanse;
        protected int ActionsPerTechAccount = 20;
        protected int AccountChangedTimes = 0;
        private readonly int RestAfterChange = 3000;
        protected int CompetitorFollowersPassed;

        protected string NextMaxId = "";
        protected List<string> seenMedias = new List<string>();
        protected int TotalMediasCount = 0;

        protected TextFileHelper txthelp = new TextFileHelper();
        protected DataUpdate da = new DataUpdate();
        protected List<AudienceActionModel> audienceList = new List<AudienceActionModel>();
        protected List<AudienceModel> existAudience = new List<AudienceModel>();
        protected List<InstaUserShort> userList = new List<InstaUserShort>();

        public virtual async Task BeginCollectingAudience(int choise) { }

        public virtual void StopCollectingAudience() { }

        protected bool IsUserExist(string name)
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
            catch (Exception e)
            {
                du.UpdateProcess($"{e.Message}", mainInstanse, userList.Count, Double.Parse(stats.Count), MessageType.Type.ERROR, this.GetType().Name);
                return false;
            }
        }

        protected async Task AddCompetitorFollowerAccountToList(InstaUserShort _user)
        {
            if (!IsUserExist(_user.UserName))
            {
                await Task.Delay(3000);
                try
                {
                    var userinfo = await Account.UserProcessor.GetFullUserInfoAsync(_user.Pk);

                    if (!userinfo.Succeeded && userinfo.Info.ResponseType == ResponseType.Spam) //IF NOT SECCEEDED
                    {
                        //CHANGE ACCOUNT
                        Account = await ChangeAccount(userinfo.Succeeded);
                        userinfo = await Account.UserProcessor.GetFullUserInfoAsync(_user.Pk);

                        if (!userinfo.Succeeded)
                            du.UpdateProcess($"{Account.GetLoggedUser().UserName} {userinfo.Info.ResponseType} {userinfo.Info.Message}", mainInstanse, userList.Count, Requests, MessageType.Type.ERROR, this.GetType().Name);
                        else
                        {
                            var u = userinfo.Value?.UserDetail ?? null;
                            if (u != null)
                            {
                                AudienceActionModel audience = new AudienceActionModel(u.Username, u.FullName, u.Pk, u.PublicPhoneNumber, u.PublicEmail, u.AccountType, u.Category, u.CityName, false,
                                Convert.ToInt32(u.MediaCount), Convert.ToInt32(userinfo.Value.UserDetail.FollowerCount), u.Biography, u.HasHighlightReels, u.ProfilePicUrl, false, false, "");
                                audienceList.Add(audience);
                                stats = da.AudienceStatsUpdate(stats, mainInstanse, null, Int32.Parse(stats.Count) + 1, null, null, null);
                            }
                        }
                    }
                    else //IF SECCEEDED
                    {
                        var u = userinfo.Value?.UserDetail ?? null;

                        if (u != null)
                        {
                            //ADD TO VIEW
                            AudienceActionModel audience = new AudienceActionModel(u.Username, u.FullName, u.Pk, u.PublicPhoneNumber, u.PublicEmail, u.AccountType, u.Category, u.CityName, false,
                                Convert.ToInt32(u.MediaCount), Convert.ToInt32(userinfo.Value.UserDetail.FollowerCount), u.Biography, u.HasHighlightReels, u.ProfilePicUrl, false, false, "");
                            //ADD TO LIST TO SAVE
                            audienceList.Add(audience);
                            stats = da.AudienceStatsUpdate(stats, mainInstanse, null, Int32.Parse(stats.Count) + 1, null, null, null);
                        }
                    }
                    Requests++;
                    await ChangeAccountByRequestLimit();
                }
                catch (Exception e) { du.UpdateProcess($"{e.Message}", mainInstanse, userList.Count, CompetitorFollowersPassed, MessageType.Type.ERROR, this.GetType().Name); }
            }
            else
                du.UpdateProcess($"{_user.UserName} already exist", mainInstanse, userList.Count, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
        }

        protected async Task<IInstaApi> ChangeAccount(bool result)
        {
            if (!result)
            {
                var account = await accountInfoHelper.NeededChangeTechAccount(Account);
                stats = du.AudienceStatsUpdate(stats, mainInstanse, null, null, null, account.GetLoggedUser().UserName, null);
                if (Account == null)
                {
                    logs.Add($"All tech accounts banned", MessageType.Type.DEBUGINFO, this.GetType().Name);
                    do
                    {
                        du.UpdateProcess($"Waiting for any account unban...", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                        await accountInfoHelper.GetAccountsStatus();
                        account = await accountInfoHelper.GetTechAccountAsync();

                        await Rest("", RestAfterChange);
                    } while (account == null);
                    logs.Add($"Technical account has been changed to {Account.GetLoggedUser().UserName}", MessageType.Type.DEBUGINFO, this.GetType().Name);
                    return account;
                }
                else
                    return account;
            }
            else return Account;
        }
        protected async Task ChangeAccountByRequestLimit()
        {
            if (Requests > ActionsPerTechAccount == true)
            {
                SetRandomRequestCount();

                Account = await accountInfoHelper.NeededChangeTechAccount(Account);
                stats = du.AudienceStatsUpdate(stats, mainInstanse, null, null, null, Account.GetLoggedUser().UserName, null);

                txthelp.SaveAudienceToTxtFile(Properties.Settings.Default.SaveAudiencePath, audienceList);

                await Rest("Save audience. Change account", SetRandomRestDelay());

                audienceList.Clear();
                Requests = 0;
                AccountChangedTimes++;

                if (AccountChangedTimes > 10)
                {
                    //0.5 hour await
                    logs.Add("Rest for 30 min", MessageType.Type.AUDIENCE, this.GetType().ToString());
                    await Rest("", 1800000);
                    AccountChangedTimes = 0;
                }
            }
        }

        protected virtual async Task Rest(string message, int delay)
        {
            var _delay = delay / 1000;
            do
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    du.UpdateProcess($"{message}. Rest {th.GetNormalTime(_delay)} seconds", mainInstanse, null, null, MessageType.Type.DEBUGINFO, this.GetType().Name);
                });

                await Task.Delay(1000);
                _delay--;
            } while (_delay > 0 && mainVars.IsAudienceInProgress);
        }

        private int SetRandomRestDelay()
        {
            Random random = new Random();
            return random.Next(60, 180) * 1000;
        }
        private void SetRandomRequestCount()
        {
            Random random = new Random();
            int count = random.Next(5, 30);
            ActionsPerTechAccount = count;
        }

        protected async Task<long> GetCompetitorId(string competitor)
        {
            //GET COMPETITOR INFO 
            var compet = await Account.UserProcessor.GetUserAsync(competitor);
            CompetitorFollowersPassed = 0;

            if (!compet.Succeeded)
            {
                du.UpdateProcess($"{compet.Info.Message}", mainInstanse, null, null, MessageType.Type.ERROR, this.GetType().Name);
                await Task.Delay(10000);
                return 0;
            }
            else
                return compet.Value.Pk;
        }

        protected async Task<List<AudienceModel>> GetExistAudienceList()
        {
            //GET AUDIENCE FROM FILE  
            var list = await txthelp.GetAudienceFromTxtFileShort(Properties.Settings.Default.SaveAudiencePath);
            stats = da.AudienceStatsUpdate(stats, mainInstanse, null, existAudience.Count, null, null, null);
            du.UpdateProcess($"Begin getting competitor followers", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
            return list;
        }

        protected void timerStart()
        {
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Start();
        }
        protected void timerStop()
        {
            Delay = 0;
            timer?.Stop();
        }
        protected virtual void timerTick(object sender, EventArgs e)
        {
            timepass++;
            stats = du.AudienceStatsUpdate(stats, mainInstanse, null, null, th.GetNormalTime(timepass), null, null);
        }
    }


}
