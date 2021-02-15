using Instagram_Assistant.Enums;
using Instagram_Assistant.Helpers.Common;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers.Audience
{
    class HashtagAudienceHelper : AudienceCommon
    {
        public HashtagAudienceHelper(AudienceViewModelBase model)
        {
            mainInstanse = model;
            stats = new AudienceStatsModel();
        }

        private delegate Task GetHashtagAudienceDelegate(string hashtag);

        public override async Task BeginCollectingAudience(int choise)
        {
            timer = new DispatcherTimer();
            timerStart();
            Account = await accountInfoHelper.GetTechAccountAsync();
            stats = mainInstanse.Stats;
            if (Account != null)
            {
                mainVars.IsHashtagAudienceInProgress = true;
                stats = da.AudienceStatsUpdate(stats, mainInstanse, AccountStatus.Type.WORKING.ToString(), existAudience.Count, null, Account.GetLoggedUser().UserName, null); ;
                await GetHashtagAudience(choise);
            }
            else
            {
                StopCollectingAudience();
                logs.Add("Cant get tech account!", MessageType.Type.ERROR, this.GetType().Name);
            }
        }
        public override void StopCollectingAudience()
        {
            timerStop();
            mainVars.IsHashtagAudienceInProgress = false;
            du.UpdateProcess($"Collecting audience has been stopped", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
            timepass = 0;
            audienceList.Clear();
            existAudience.Clear();
            userList.Clear();
            HashtagAudiencePageViewModel.Instance.ButtonContent = "Start";
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            du.AudienceStatsUpdate(stats, mainInstanse, AccountStatus.Type.OFF.ToString(), 0, "00:00:00", "", "");
        }

        private async Task GetHashtagAudience(int choise)
        {
            if (Properties.Settings.Default.SaveAudiencePath != "")
            {
                existAudience = await GetExistAudienceList();
                stats.Count += existAudience.Count;

                foreach (var hastag in HashtagAudiencePageViewModel.Instance.CollectFromList)
                {
                    if (mainVars.IsHashtagAudienceInProgress == true)
                    {
                        stats = da.AudienceStatsUpdate(stats, mainInstanse, null, null, null, null, "#" + hastag);
                        if (choise == 0)
                            du.UpdateProcess($"Begin getting {hastag} audience by recent posts", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                        else
                            du.UpdateProcess($"Begin getting {hastag} audience by top posts", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                        GetHashtagAudienceDelegate getAudience;
                        switch (choise)
                        {
                            case 0:
                                getAudience = GetAudienceByRecentMedia;
                                break;
                            case 1:
                                getAudience = GetAudienceByTopMedia;
                                break;

                            default:
                                getAudience = null;
                                break;
                        }

                        if (getAudience != null)
                            await getAudience(hastag);

                            du.UpdateProcess($"Getting audience from {hastag} was finished.", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                        
                    }
                    else
                        du.UpdateProcess($"Audience wasn`t collect!  Audience actions was stopped", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                    Requests = 0;
                    userList.Clear();
                }
                StopCollectingAudience();
            }
            else
            {
                logs.Add("Please selecte path to save audience", MessageType.Type.ERROR, this.GetType().ToString());
                StopCollectingAudience();
            }
        }

        private async Task GetAudienceByTopMedia(string hashtag)
        {
            do
            {
                PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                paginationParameters.NextMaxId = NextMaxId;
                var _result = await Account.HashtagProcessor.GetTopHashtagMediaListAsync(hashtag, paginationParameters);
                
                NextMaxId = _result.Value.NextMaxId;
                TotalMediasCount += _result.Value.Medias.Count;

                foreach (var user in _result.Value.Medias)
                {
                    if (mainVars.IsHashtagAudienceInProgress)
                    {
                        if (!seenMedias.Contains(user.InstaIdentifier))
                        {
                            await AddCompetitorFollowerAccountToList(user.User);
                            seenMedias.Add(user.InstaIdentifier);
                            du.UpdateProcess($"Got {user.User.UserName} while search by #{hashtag} in top medias", mainInstanse, TotalMediasCount, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                        }
                        else
                            du.UpdateProcess($"{user.User.UserName} already exist", mainInstanse, TotalMediasCount, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                    } else break;                  
                }
            } while (mainVars.IsHashtagAudienceInProgress && NextMaxId != null);

        }
        private async Task GetAudienceByRecentMedia(string hashtag)
        {
            do
            {
                PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                paginationParameters.NextMaxId = NextMaxId;
                var _result = await Account.HashtagProcessor.GetRecentHashtagMediaListAsync(hashtag, paginationParameters);
                NextMaxId = _result.Value.NextMaxId; 
                TotalMediasCount += _result.Value.Medias.Count;
                foreach (var user in _result.Value.Medias)
                {
                    if (mainVars.IsHashtagAudienceInProgress)
                    {
                        if (!seenMedias.Contains(user.Pk))
                        {
                            await AddCompetitorFollowerAccountToList(user.User);
                            seenMedias.Add(user.Pk);
                            du.UpdateProcess($"Got {user.User.UserName} while search by #{hashtag} in recent medias", mainInstanse, TotalMediasCount, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                        }
                        else
                            du.UpdateProcess($"{user.User.UserName} already exist", mainInstanse, TotalMediasCount, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                    } else break;        
                }
            } while (mainVars.IsHashtagAudienceInProgress && NextMaxId != null);
        }

        protected override async Task Rest(string message, int delay)
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
            } while (_delay > 0 && mainVars.IsHashtagAudienceInProgress);
        }

        protected override void timerTick(object sender, EventArgs e)
        {
            if (mainVars.IsHashtagAudienceInProgress)
            {
                timepass++;
                stats = du.AudienceStatsUpdate(stats, mainInstanse, null, null, th.GetNormalTime(timepass), null, null);
            }
        }
    }
}
