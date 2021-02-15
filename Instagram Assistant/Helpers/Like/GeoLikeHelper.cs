using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Instagram_Assistant.Helpers.Like
{
    sealed class GeoLikeHelper: LikeCommon
    {
        //STATUS: +OK
        private string NextMaxId { get; set; } = null;
     
        public GeoLikeHelper(CommonViewModel model)
        {
            userfeed = new List<FeedModel>();
            actions = new ObservableCollection<ActionModel>();
            seenMediasMassive = new List<string>();
            mainInstanse = model;
        }

        public async Task BeginLike(SearchResultModel usergeo)
        {       

            if (await InitCommonData(mainInstanse))
            {
                timerStart();
                do
                {
                    var feed = await GetFeed(long.Parse(usergeo?.Id ?? "0"));
                    if (feed != null)
                    {
                        foreach (var user in feed)
                        {
                            if (mainVars.IsGeoLikeInProgress == true)
                            {
                                try
                                {
                                    await SetLike(user);
                                    await Task.Delay(Delay);
                                }
                                catch (Exception e) { logs.Add($"Post by {user.user} CAN`T be liked! ERROR: {e.Message}", MessageType.Type.ERROR, this.GetType().Name); }
                            }
                            else
                                Stop(mainInstanse);
                        }                          
                    }
                } while (mainVars.IsGeoLikeInProgress == true);
                Stop(mainInstanse);
            }
        }
        public async Task<InstaLocationShortList> SearchGeo(string geo)
        {
            if(Account == null)
                Account = await accountInfoHelper.GetMainAccountAsync();

            if (Account != null)
            {
                var locations = await Account.LocationProcessor.SearchLocationAsync(0, 0, geo);
                return locations.Value;
            }
            else return null;
        }
        private async Task<List<FeedModel>> GetFeed(long location)
        {
            if (location != 0)
            {
                Account = await accountInfoHelper.GetMainAccountAsync();
                if (Account != null)
                {
                    //clear previous posts
                    userfeed.Clear();
                    //get new one                  
                    PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                    paginationParameters.NextMaxId = NextMaxId;
                    var feed = await Account.LocationProcessor.GetRecentLocationFeedsAsync(location, paginationParameters);
                    NextMaxId = feed?.Value.NextMaxId;

                    if (feed != null)
                    {
                        logs.Add($"Find {feed.Value.Medias.Count} posts to like!", MessageType.Type.DEBUGINFO, this.GetType().Name);
                        foreach (var media in feed.Value.Medias)
                        {
                            if (media.HasLiked != true && seenMediasMassive.Contains(media.InstaIdentifier) != true)
                                AddPostForLike(media);
                        }
                        return userfeed;
                    }
                    else
                    {
                        logs.Add($"Error while loading posts", MessageType.Type.ERROR, this.GetType().Name);
                        return null;
                    }
                }
                else return null;
            }
            else
            { 
                Stop(mainInstanse);
                logs.Add("No geoposition! Please select!", MessageType.Type.ERROR, this.GetType().Name);
                return null; 
            }
        }
 
    }
}
