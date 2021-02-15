using Instagram_Assistant.Enums;
using Instagram_Assistant.Helpers.Common;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    sealed class AudienceHelper: AudienceCommon
    {
        //STATUS : NOT OK
        public AudienceHelper(AudienceViewModelBase model)
        {
            mainInstanse = model;
            stats = (new AudienceStatsModel{
                TechAccount = "n/a",
                Status = "OFF",
                Competitor = "n/a",
                Count = "0",
                TimeInWork = "00:00:00"
            });
        }

        private delegate Task GetUsersById(long id);

        public override async Task BeginCollectingAudience(int choise)
        {
            timer = new DispatcherTimer();
            timerStart();
            Account = await accountInfoHelper.GetTechAccountAsync();
            stats = AudiencePageViewModel.Instance.Stats;
            if (Account != null)
            {
                mainVars.IsAudienceInProgress = true;
                stats = da.AudienceStatsUpdate(stats, mainInstanse, AccountStatus.Type.WORKING.ToString(), existAudience.Count, null, Account.GetLoggedUser().UserName, null); ;           
                await GetCompetitorFollowers(choise);
            }else
            {
                StopCollectingAudience();
                logs.Add("Cant get tech account!", MessageType.Type.ERROR, this.GetType().Name);
            }
        }
        public override void StopCollectingAudience()
        {
            timerStop();
            mainVars.IsAudienceInProgress = false;
            du.UpdateProcess($"Collecting audience has been stopped",mainInstanse,null, null, MessageType.Type.AUDIENCE,this.GetType().Name);
            timepass = 0;
            audienceList.Clear();
            existAudience.Clear();
            userList.Clear();
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            du.AudienceStatsUpdate(stats, mainInstanse, AccountStatus.Type.OFF.ToString(), 0, "00:00:00", "", "");
        }

        private async Task GetCompetitorFollowers(int choise)
        {        
            if (Properties.Settings.Default.SaveAudiencePath != "" && Account != null)
            {
                existAudience =  await GetExistAudienceList();

                foreach (var competitor in AudiencePageViewModel.Instance.CollectFromList)
                {
                    if (mainVars.IsAudienceInProgress == true)
                    {
                        stats = da.AudienceStatsUpdate(stats, mainInstanse, null, null, null, null, competitor);
                        if(choise == 0)
                            du.UpdateProcess($"Begin getting {competitor} followers", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                        else
                            du.UpdateProcess($"Begin getting {competitor} following", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                        //GET COMPETITOR ID 
                        long UserId = await GetCompetitorId(competitor);

                        //GET COMPETITOR FOLLOWING/FOLLOWS LIST | 0-FOLLOWING 1-FOLLOWS
                        GetUsersById getUsers;
                        switch (choise)
                        {
                            case 0:
                                getUsers = GetFollowersByUserId;
                                break;
                            case 1:
                                getUsers = GetFollowingByUserId;
                                break;
                            default:
                                getUsers = null;
                                break;
                        }

                        if (getUsers != null)
                            await getUsers(UserId);

                        if (mainVars.IsAudienceInProgress == true && userList != null)
                        {
                            foreach (var user in userList)
                            {
                                await AddCompetitorFollowerAccountToList(user);
                                du.UpdateProcess($"Getting followers from {competitor}", mainInstanse, userList.Count, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                            }

                            du.UpdateProcess($"Getting audience from {competitor} was finished.", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                        }                      
                    }
                    else
                        du.UpdateProcess($"Audience wasn`t collect!  Audience actions was stopped", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                    Requests = 0;
                    userList.Clear();
                }
                await ChangeAccountByRequestLimit();
                du.UpdateProcess($"Getting audience from all competitor was finished.", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                StopCollectingAudience();
            }
            else
            {
                stats = da.AudienceStatsUpdate(stats, mainInstanse, null, null, null, null, null);
                MessageBox.Show($"Path to save {Properties.Settings.Default.SaveAudiencePath ?? "Not selected"}.\n Technical account: {Account?.GetLoggedUser().UserName ?? "No tech account"}","Error!",MessageBoxButton.OK,MessageBoxImage.Error);
                StopCollectingAudience();
            }
        }

        private async Task GetFollowersByUserId(long _id)
        {
            string LatestMaxId = "0";
            PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(3);
            do
            {
                var _result = await Account.UserProcessor.GetUserFollowersByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));

                #region Checkers
                if (_result != null)
                {
                    if (_result.Info.ResponseType == ResponseType.UnExpectedResponse)
                        du.UpdateProcess($"{_result.Info.Message}. May be there are too many followers on this account. Skip", mainInstanse, userList?.Count, Double.Parse(stats.Count), MessageType.Type.ERROR, this.GetType().Name);               
                    if (!_result.Succeeded)
                    {
                        await ChangeAccount(_result.Succeeded);
                        _result = await Account.UserProcessor.GetUserFollowersByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));
                    }
                }
                #endregion

                LatestMaxId = _result.Value.NextMaxId;
                if (userList is null)
                {
                    userList = new List<InstaUserShort>();
                    userList?.AddRange(_result.Value);
                } else userList?.AddRange(_result.Value);

                du.UpdateProcess($"Getting followers from {stats.Competitor}", mainInstanse, userList.Count, Double.Parse(stats.Count), MessageType.Type.HIDDEN, this.GetType().Name);
                Requests++;
                await ChangeAccountByRequestLimit();
            } while (LatestMaxId != null && mainVars.IsAudienceInProgress == true);
        }
        private async Task GetFollowingByUserId(long _id)
        {
            string LatestMaxId = "0";
            PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(3);
            do
            {
                var _result = await Account.UserProcessor.GetUserFollowingByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));

                LatestMaxId = _result.Value.NextMaxId;
                userList.AddRange(_result.Value);
                #region Checkers
                if (_result != null)
                {
                    if (_result.Info.ResponseType == ResponseType.UnExpectedResponse)
                    {
                        du.UpdateProcess($"{_result.Info.Message}.{_result.Info.ResponseType})", mainInstanse, userList.Count, Double.Parse(stats.Count), MessageType.Type.ERROR, this.GetType().Name);
                        MessageBox.Show("UnExpectedResponse Error. May be there are too many followers on this account. Skip", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    if (!_result.Succeeded)
                    {
                        await ChangeAccount(_result.Succeeded);
                        _result = await Account.UserProcessor.GetUserFollowersByIdAsync(_id, paginationParameters.StartFromMaxId(LatestMaxId));
                    }
                }
                #endregion

                du.UpdateProcess($"Getting followers from {stats.Competitor}", mainInstanse, userList.Count, Double.Parse(stats.Count), MessageType.Type.HIDDEN, this.GetType().Name);
                Requests++;
                await ChangeAccountByRequestLimit();
            } while (LatestMaxId != null && mainVars.IsAudienceInProgress == true);           

        }

    }
}
