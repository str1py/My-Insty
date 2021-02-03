using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Stories;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers.Story
{
    class StoriesFeedHelper : StoriesCommon
    {
        //STATUS: +-OK
        public StoriesFeedHelper(CommonViewModel model)
        {
            userstoriesfeed = new List<StoryModel>(); //List For Feeds from user Instagram
            actions = new ObservableCollection<ActionModel>(); //What app doing 
            mainInstanse = model;
        }   
        public async Task<bool> BeginWatch()
        {
            if(await InitCommonData(mainInstanse))
            {
                timerStart();//запуск таймера
                do
                {
                    var stories = await GetStories();
                    if (stories != null && stories?.Count != 0)
                    {
                        foreach (var story in stories)
                        {
                            if (mainVars.IsFeedStoriesWatching == true)
                            {
                                await WatchStory(story);
                                await Task.Delay(Delay);
                            }
                        }
                    }
                    else
                    {
                        logs.Add($"Seems to be there are no stories. Waiting 30 sec and try one again", MessageType.Type.STORY, this.GetType().Name);
                        await Task.Delay(30000);
                    }
                } while (mainVars.IsFeedStoriesWatching == true);

                Stop(mainInstanse);
                return false;
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Stop(mainInstanse);
                return false;
            }
        }

        public async Task<List<StoryModel>> GetStories()
        {
            userstoriesfeed.Clear();
            var result = await Account.StoryProcessor.GetStoryFeedAsync();

            var storyFeed = result.Value;
            foreach (var feedItem in storyFeed.Items)
            {
                if (mainVars.IsFeedStoriesWatching == false)
                    break;

                if (feedItem.Seen == 0)
                {
                    foreach (var story in feedItem.Items)
                        AddStoryForWatch(feedItem, story);                  
                }
            }

            if (userstoriesfeed.Count == 0)
            {
                var dee = GetUserIdsWithStories(result);
                var stories = await GetUsersStories(dee);

                foreach (var story in stories)                
                    AddStoryForWatch(story);             
            }

            logs.Add($"Got {userstoriesfeed.Count} story reels.", MessageType.Type.DEBUGINFO, this.GetType().Name);
            return userstoriesfeed;
        }
        private List<long> GetUserIdsWithStories(IResult<InstaStoryFeed> feed)
        {
            List<long> userids = new List<long>();
            foreach (var ids in feed.Value.Items)                   
                userids.Add(long.Parse(ids.Id));          

            return userids;
        }
        private async Task<List<InstaStoryItem>> GetUsersStories(List<long> ids)
        {
            List<InstaStoryItem> stories = new List<InstaStoryItem>();
            List<IResult<InstaReelFeed>> _story = new List<IResult<InstaReelFeed>>();

            int count = 0;

            foreach (var id in ids)
            {
                if(count > 50)
                        break;

                _story.Clear();
                var result = await Account.StoryProcessor.GetUserStoryFeedAsync(id);
                if (result.Succeeded) 
                { 
                _story.Add(result);

                FeedStoriesPageViewModel.Instance.LastActionTextHelper = $"Searching stories";
                du.ClearActions(mainInstanse);

                    foreach (var story in _story)
                    {
                        if (mainVars.IsFeedStoriesWatching == false)
                        {
                            FeedStoriesPageViewModel.Instance.LastActionTextHelper = $"No actions yet";
                            break;
                        }
                        if (story?.Value.Seen == 0)
                        {
                            FeedStoriesPageViewModel.Instance.LastActionTextHelper = $"Getting stories by {story.Value.User.UserName}";
                            foreach (var item in _story)
                            {
                                foreach (var storiesitems in item.Value.Items)
                                {
                                    stories.Add(storiesitems);
                                    count++;
                                }
                            }
                        }
                        else
                        {
                            int rnd = new Random().Next(0, 3);
                            switch (rnd)
                            {
                                case 0:
                                    FeedStoriesPageViewModel.Instance.LastActionTextHelper = $"Seem to be we saw stories by {story.Value.User.UserName} already";
                                    break;
                                case 1:
                                    FeedStoriesPageViewModel.Instance.LastActionTextHelper = $"Hmmmm... stories by {story.Value.User.UserName} we already saw";
                                    break;
                                case 2:
                                    FeedStoriesPageViewModel.Instance.LastActionTextHelper = $"I think stories by {story.Value.User.UserName} we already watched";
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Stop(mainInstanse);
                    logs.Add(result.Info.ToString(), MessageType.Type.ERROR, this.GetType().ToString());
                }
            }
            FeedStoriesPageViewModel.Instance.LastActionTextHelper = $"";
            return stories;
        }


    }
}
