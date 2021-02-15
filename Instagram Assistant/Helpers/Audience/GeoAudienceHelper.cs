using Instagram_Assistant.Enums;
using Instagram_Assistant.Helpers.Common;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp;
using InstagramApiSharp.Classes.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers.Audience
{
    class GeoAudienceHelper : AudienceCommon
    {
        public GeoAudienceHelper(AudienceViewModelBase model)
        {
            mainInstanse = model;
            stats = new AudienceStatsModel();
        }

        private delegate Task GetGeoAudienceDelegate(InstaLocationShort Geo);

        private async Task<InstaLocationShort> GetGeo(string geo)
        {
            if (Account == null)
                Account = await accountInfoHelper.GetTechAccountAsync();

            if (Account != null)
            {
                var locations = await Account.LocationProcessor.SearchLocationAsync(0, 0, geo);
                return locations.Value.FirstOrDefault();
            }
            else return null;
        }

        public override async Task BeginCollectingAudience(int choise)
        {
            timer = new DispatcherTimer();
            timerStart();
            Account = await accountInfoHelper.GetTechAccountAsync();
            stats = mainInstanse.Stats;
            if (Account != null)
            {
                mainVars.IsGeoAudienceInProgress = true;
                stats = da.AudienceStatsUpdate(stats, mainInstanse, AccountStatus.Type.WORKING.ToString(), existAudience.Count, null, Account.GetLoggedUser().UserName, null); ;
                await GetGeoAudience(choise);
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
            mainVars.IsGeoAudienceInProgress = false;
            du.UpdateProcess($"Collecting audience has been stopped", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
            timepass = 0;
            audienceList.Clear();
            existAudience.Clear();
            userList.Clear();
            mainInstanse.ButtonContent = "Start";
            accountInfoHelper.UpdateAccountStatus(Account, AccountStatus.Type.REST);
            stats = du.AudienceStatsUpdate(stats, mainInstanse, AccountStatus.Type.OFF.ToString(), 0, "00:00:00", "", "");
        }

        private async Task GetGeoAudience(int choise)
        {
            if (Properties.Settings.Default.SaveAudiencePath != "")
            {
                existAudience = await GetExistAudienceList();
                stats.Count += existAudience.Count;

                foreach (var location in GeoAudiencePageViewModel.Instance.CollectFromList)
                {
                    if (mainVars.IsGeoAudienceInProgress == true)
                    {
                        stats = da.AudienceStatsUpdate(stats, mainInstanse, null, null, null, null, "📌" + location);
                        if (choise == 0)
                            du.UpdateProcess($"Begin getting 📌{location} audience by recent posts", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                        else
                            du.UpdateProcess($"Begin getting 📌{location} audience by top posts", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);

                        GetGeoAudienceDelegate getAudience;
                        switch (choise)
                        {
                            case 0:
                                getAudience = GetRecentGeoMedia;
                                break;
                            case 1:
                                getAudience = GetTopGeoMedia;
                                break;
                            default:
                                getAudience = null;
                                break;
                        }

                        if (getAudience != null)
                            await getAudience(await GetGeo(location));

                        du.UpdateProcess($"Getting audience from 📌{location} was finished.", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                    }
                    else
                        du.UpdateProcess($"Audience wasn`t collect!  Audience actions was stopped", mainInstanse, null, null, MessageType.Type.AUDIENCE, this.GetType().Name);
                }
                StopCollectingAudience();
            }
            else
            {
                logs.Add("Please selecte path to save audience", MessageType.Type.ERROR, this.GetType().ToString());
                StopCollectingAudience();
            }
        }

        private async Task GetRecentGeoMedia(InstaLocationShort location)
        {
            do
            {
                PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                paginationParameters.NextMaxId = NextMaxId;
                var feed = await Account.LocationProcessor.GetRecentLocationFeedsAsync(long.Parse(location.ExternalId), paginationParameters);
                NextMaxId = feed?.Value.NextMaxId;
                TotalMediasCount += feed.Value.Medias.Count;

                foreach (var user in feed.Value.Medias)
                {
                    if (mainVars.IsGeoAudienceInProgress)
                    {
                        if (!seenMedias.Contains(user.Pk))
                        {
                            await AddCompetitorFollowerAccountToList(user.User);
                            seenMedias.Add(user.Pk);
                            du.UpdateProcess($"Got {user.User.UserName} while search by 📌{location.Name} in top medias", mainInstanse, TotalMediasCount, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                        }
                    }
                    else break;
                }
            } while (mainVars.IsGeoAudienceInProgress && NextMaxId != null);

        }
        private async Task GetTopGeoMedia(InstaLocationShort location)
        {
            do
            {
                PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                paginationParameters.NextMaxId = NextMaxId;
                var feed = await Account.LocationProcessor.GetTopLocationFeedsAsync(long.Parse(location.ExternalId), paginationParameters);
                NextMaxId = feed?.Value.NextMaxId;
                TotalMediasCount += feed.Value.Medias.Count;

                foreach (var user in feed.Value.Medias)
                {
                    if (mainVars.IsGeoAudienceInProgress)
                    {
                        if (!seenMedias.Contains(user.Pk))
                        {
                            await AddCompetitorFollowerAccountToList(user.User);
                            seenMedias.Add(user.Pk);
                            du.UpdateProcess($"Got {user.User.UserName} while search by 📌{location.Name} in top medias", mainInstanse, TotalMediasCount, CompetitorFollowersPassed++, MessageType.Type.HIDDEN, this.GetType().Name);
                        }
                    } else break;
                }

            } while (mainVars.IsGeoAudienceInProgress && NextMaxId != null);
        }

        protected override void timerTick(object sender, EventArgs e)
        {
            if (mainVars.IsGeoAudienceInProgress)
            {
                timepass++;
                stats = du.AudienceStatsUpdate(stats, mainInstanse, null, null, th.GetNormalTime(timepass), null, null);
            }
        }


    }
}
