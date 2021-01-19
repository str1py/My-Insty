using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers.Like
{
    class HashtagLikeHelper: LikeCommon
    {
        //STATUS: +OK
        private string NextMaxId { get; set; } = null;
        
        public HashtagLikeHelper()
        {
            userfeed = new List<FeedModel>();
            feedactions = new ObservableCollection<ActionModel>();
            seenMediasMassive = new List<string>();
        }

        public async Task<InstaHashtagSearch> SearchHashtag(string hashtag)
        {
            Account = accountInfoHelper.GetMainAccount();
            if (Account != null)
            {
                var ht = await Account.HashtagProcessor.SearchHashtagAsync(hashtag);
                return ht.Value;
            }
            else return null;
        }

        public async Task BeginLike(string hashtag)
        {          
            mainInstanse = HashtagLikePageViewModel.Instanse;
            if (InitCommonData(mainInstanse))
            {
                timerStart();
                do
                {
                    var feed = await GetFeed(hashtag);
                    if (feed != null)
                    {
                        foreach (var a in feed)
                        {
                            await WorkTimeCheck(limits.WorkTimeLimitCheck());
                            await HourPassedCheck();
                            await MaxCountPassedCheck();

                            if (mainVars.IsHashtagLikeInProgres == true)
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
                                        Delay = helper.GetLikeDelay();

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
                } while (mainVars.IsHashtagLikeInProgres == true);
                StopLike(mainInstanse);
            }
        } 
        private async Task<List<FeedModel>> GetFeed(string hashtag)
        {
            if (!String.IsNullOrEmpty(hashtag))
            {
                Account = accountInfoHelper.GetMainAccount();
            if (Account != null)
            {
                //clear previous posts
                userfeed.Clear();
                //get new one
                PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(1);
                paginationParameters.NextMaxId = NextMaxId;
                var feed = await Account.HashtagProcessor.GetRecentHashtagMediaListAsync(hashtag, paginationParameters);
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
                logs.Add("No #hashtag! Please select!", MessageType.Type.ERROR, this.GetType().Name);
                return null;
            }
        }
    }
}
