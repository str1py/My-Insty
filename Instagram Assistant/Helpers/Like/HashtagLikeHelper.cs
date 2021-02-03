using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
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
    sealed class HashtagLikeHelper: LikeCommon
    {
        //STATUS: +OK
        private string NextMaxId { get; set; } = null;
        
        public HashtagLikeHelper(CommonViewModel model)
        {
            userfeed = new List<FeedModel>();
            actions = new ObservableCollection<ActionModel>();
            seenMediasMassive = new List<string>();
            mainInstanse = model;
        }

        public async Task<InstaHashtagSearch> SearchHashtag(string hashtag)
        {
            Account = await accountInfoHelper.GetMainAccountAsync();
            if (Account != null)
            {
                var ht = await Account.HashtagProcessor.SearchHashtagAsync(hashtag);
                return ht.Value;
            }
            else return null;
        }

        public async Task BeginLike(string hashtag)
        {          
            if (await InitCommonData(mainInstanse))
            {
                timerStart();
                do
                {
                    var feed = await GetFeed(hashtag);
                    if (feed != null)
                    {
                        foreach (var user in feed)
                        {
                            if (mainVars.IsHashtagLikeInProgres == true)
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
                } while (mainVars.IsHashtagLikeInProgres == true);
                Stop(mainInstanse);
            }
        }
        private async Task<List<FeedModel>> GetFeed(string hashtag)
        {
            if (!String.IsNullOrEmpty(hashtag))
            {
                Account = await accountInfoHelper.GetMainAccountAsync();
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
                logs.Add("No #hashtag! Please select!", MessageType.Type.ERROR, this.GetType().Name);
                return null;
            }
        }
    }
}
