using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers.Like
{
    class GeoLikeHelper: LikeCommon
    {
        //STATUS: +OK
        private string NextMaxId { get; set; } = null;
     
        public GeoLikeHelper()
        {
            userfeed = new List<FeedModel>();
            feedactions = new ObservableCollection<ActionModel>();
            seenMediasMassive = new List<string>();
        }

        public async Task BeginLike(SearchResultModel usergeo)
        {       
            mainInstanse = GeoLikePageViewModel.Instanse;
            if (InitCommonData(mainInstanse))
            {
                timerStart();
                do
                {
                    var feed = await GetFeed(long.Parse(usergeo?.Id ?? "0"));
                    if (feed != null)
                    {
                        foreach (var a in feed)
                        {

                            await WorkTimeCheck(limits.WorkTimeLimitCheck());
                            await HourPassedCheck();
                            await MaxCountPassedCheck();

                            if (mainVars.IsGeoLikeInProgress == true)
                            {
                                try
                                {
                                    if (helper.SkipLikePost())
                                    {
                                        logs.Add($"Post by {a.user} was SKIPED", MessageType.Type.LIKE, this.GetType().Name);
                                        Delay = 5000;
                                        du.UpdateActions(feedactions, mainInstanse, 5, a.userPict, a.user, "Ban", null, a.postPreview);
                                    }
                                    else
                                    {
                                        await Account.MediaProcessor.LikeMediaAsync(a.InstaIdentifier); //ставим лайк
              
                                        Properties.Settings.Default.FeedLikesTotalCount++;
                                        Properties.Settings.Default.Save();
                                        _feedstats = du.StatsUpdate(_feedstats, this.GetType().Name, null, Properties.Settings.Default.FeedLikesTotalCount, int.Parse(_feedstats.SessionCount) + 1, null, null, null);

                                        logs.Add($"Post by {a.user} was successfully LIKED", MessageType.Type.LIKE, this.GetType().Name);
                                        Delay = helper.GetLikeDelay();//ставим задержку - сделать норм

                                        du.UpdateActions(feedactions, mainInstanse, 5, a.userPict, a.user, "Heart", null, a.postPreview);
                                        seenMediasMassive.Add(a.InstaIdentifier);
                                    }
                                    await Task.Delay(Delay);
                                }
                                catch (Exception e) { logs.Add($"Post by {a.user} CAN`T be liked! ERROR: {e.Message}", MessageType.Type.ERROR, this.GetType().Name); }
                            }
                            else
                                StopLike(mainInstanse);
                        }
                           
                    }

                } while (mainVars.IsGeoLikeInProgress == true);
                StopLike(mainInstanse);
            }
        }
        public async Task<InstaLocationShortList> SearchGeo(string geo)
        {
            if(Account == null)
                Account = accountInfoHelper.GetMainAccount();

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
                Account = accountInfoHelper.GetMainAccount();
                if (Account != null)
                {
                    //clear previous posts
                    userfeed.Clear();
                    //get new one
                    
                    PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                    paginationParameters.NextMaxId = NextMaxId;
                    var feed = await Account.LocationProcessor.GetRecentLocationFeedsAsync(location, paginationParameters);
                    NextMaxId = feed.Value.NextMaxId;

                    if (feed != null)
                    {
                        logs.Add($"Find {feed.Value.Medias.Count} posts to like!", MessageType.Type.DEBUGINFO, this.GetType().Name);
                        foreach (var media in feed.Value.Medias)
                        {
                            if (media.HasLiked != true && seenMediasMassive.Contains(media.InstaIdentifier) != true)
                            {
                                try
                                {
                                    //ADD if images or video
                                    userfeed.Add(new FeedModel
                                    {
                                        user = media.User.UserName,
                                        userPict = imghelp.GetImage(media.User.ProfilePicture) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                        postPreview = imghelp.GetImage(media.Images[0].Uri),
                                        InstaIdentifier = media.InstaIdentifier,
                                        MediaType = media.MediaType.ToString()
                                    });
                                }
                                catch
                                {
                                    //ADD if images or Carousel
                                    userfeed.Add(new FeedModel
                                    {
                                        user = media.User.UserName,
                                        userPict = imghelp.GetImage(media.User.ProfilePicture) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                        postPreview = imghelp.GetImage(media.Carousel[0].Images[0].Uri),
                                        InstaIdentifier = media.InstaIdentifier,
                                        MediaType = media.MediaType.ToString()
                                    });
                                }
                            }
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
                StopLike(mainInstanse);
                logs.Add("No geoposition! Please select!", MessageType.Type.ERROR, this.GetType().Name);
                return null; 
            }
        }
 
    }
}
