using Instagram_Assistant.Helpers.Like;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;


namespace Instagram_Assistant.Helpers
{
    sealed class FeedLikeHelper: LikeCommon
    {
        //STATUS: +-OK
        public FeedLikeHelper(CommonViewModel model)
        {
            userfeed = new List<FeedModel>();
            actions = new ObservableCollection<ActionModel>();
            seenMediasMassive = new List<string>();
            mainInstanse = model;
        }

        private async Task<List<FeedModel>> GetFeed()
        {
            Account = await accountInfoHelper.GetMainAccountAsync();
            if (Account != null)
            {
                //clear previous posts
                userfeed.Clear();
                //get new one
                var feed = await Account.FeedProcessor.GetUserTimelineFeedAsync(null, seenMediasMassive.ToArray()) ?? null;

                if (feed != null)
                {
                    logs.Add($"Find {feed.Value.MediaItemsCount} posts to like!", MessageType.Type.DEBUGINFO, this.GetType().Name);
                    //Every posts
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
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Stop(mainInstanse);
                return null;
            }

        }
        public async Task BeginLike()
        {
            if (await InitCommonData(mainInstanse))
            {
                timerStart();
                do //do while feedlike is on and time isnt endtime
                {
                    //get user feed
                    var feed = await GetFeed();
                    if (feed != null)
                    {
                        foreach (var user in feed)
                        {
                            if (mainVars.IsFeedLikeInProgress == true)
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
                } while (mainVars.IsFeedLikeInProgress == true);
                Stop(mainInstanse);
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Stop(mainInstanse);
            }
        }
     }
}
