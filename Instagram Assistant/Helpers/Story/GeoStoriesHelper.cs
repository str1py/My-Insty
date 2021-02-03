using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Stories;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers.Story
{
    class GeoStoriesHelper : StoriesCommon
    {
        public GeoStoriesHelper(CommonViewModel model)
        {
            userstoriesfeed = new List<StoryModel>(); //List For Feeds from user Instagram
            actions = new ObservableCollection<ActionModel>(); //What app doing 
            mainInstanse = model;
        }
        public async Task<bool> BeginWatch(SearchResultModel usergeo)
        {
            if (await InitCommonData(mainInstanse))
            {
                timerStart();
                do
                {
                    var stories = await GetStories(long.Parse(usergeo?.Id ?? "0"));
                    if (stories != null && stories?.Count != 0)
                    {
                        foreach (var story in stories)
                        {
                            if (mainVars.IsGeoStoriesWatching == true)
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
                } while (mainVars.IsGeoStoriesWatching == true);
                Stop(mainInstanse);
                return false;
            }
            else return false;
        }

        public async Task<InstaLocationShortList> SearchGeo(string geo)
        {
            if (Account == null)
                Account = await accountInfoHelper.GetMainAccountAsync();

            if (Account != null)
            {
                var locations = await Account.LocationProcessor.SearchLocationAsync(0, 0, geo);
                return locations.Value;
            }
            else return null;

        }

        private async Task<List<StoryModel>> GetStories(long location)
        {
            if (location != 0)
            {
                userstoriesfeed.Clear();
     
                var result = await Account.LocationProcessor.GetLocationStoriesAsync(location);
                var storyFeed = result.Value;
                if (result.Succeeded)
                {
                    foreach (var feedItem in storyFeed.Items)
                    {
                        if (mainVars.IsGeoStoriesWatching == false)
                            break;
                        feedItem.DeviceTimestamp = DateTime.Now;
                        try
                        {
                            userstoriesfeed.Add(new StoryModel
                            {
                                user = feedItem.User.UserName,
                                userPict = imghelp.GetImage(feedItem.User.ProfilePicUrl),
                                postPreview = imghelp.GetImage(feedItem.ImageList[0].Uri) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                                StoryId = feedItem.Id,
                                ExpiringAt = feedItem.ExpiringAt,
                                DeviceTimestamp = feedItem.DeviceTimestamp
                            });
                        }
                        catch (Exception e) { logs.Add(e.ToString(), MessageType.Type.ERROR, this.GetType().Name); }
                    }

                    if (userstoriesfeed.Count == 0)
                    {
                        MessageBox.Show("No stories...");
                    }
                }
                logs.Add($"Got {userstoriesfeed.Count} story reels.", MessageType.Type.DEBUGINFO, this.GetType().Name);
                return userstoriesfeed;
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
