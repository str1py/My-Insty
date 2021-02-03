using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Stories;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers.Story
{
    class StoriesCommon : HelperBase
    {
        protected List<StoryModel> userstoriesfeed; //List For Feeds from user Instagram

        protected async Task WatchStory(StoryModel story)
        {
            try
            {
                story.DeviceTimestamp = DateTime.UtcNow;
                await Account.StoryProcessor.MarkStoryAsSeenAsync(story.StoryId, th.GetInixTime());

                mainVars.IncrementActionsCount(mainInstanse);
                stats = du.StatsUpdate(stats, mainInstanse, null, mainVars.GetTotalCountFromProperties(mainInstanse), Convert.ToInt32(stats.SessionCount) + 1, null, null, null);

                logs.Add($"Story by {story.user} was successfully watched.", MessageType.Type.STORY, this.GetType().Name);
                Delay = GetStoryDelay();//ставим задержку 

                du.UpdateActions(actions, mainInstanse, story.userPict, story.user, "Eye", null, story.postPreview);
            }
            catch (Exception e)
            {
                logs.Add($"Story by {story.user} wasn`t watched. {e.Message}", MessageType.Type.ERROR, this.GetType().Name);
            }
            finally
            {
                await WorkTimeCheck(limits.WorkTimeLimitCheck());
                await HourPassedCheck();
                await MaxCountPassedCheck();
            }
        }
        protected void AddStoryForWatch(InstaReelFeed feedItem, InstaStoryItem story)
        {
            story.DeviceTimestamp = DateTime.Now;
            try
            {
                userstoriesfeed.Add(new StoryModel
                {
                    user = feedItem.User.UserName,
                    userPict = imghelp.GetImage(feedItem.User.ProfilePicUrl),
                    postPreview = imghelp.GetImage(story.ImageList[0].Uri) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                    StoryId = story.Id,
                    ExpiringAt = story.ExpiringAt,
                    DeviceTimestamp = story.DeviceTimestamp
                });
            }
            catch (Exception e) { logs.Add(e.ToString(), MessageType.Type.ERROR, this.GetType().Name); }
        }
        protected void AddStoryForWatch(InstaStoryItem story)
        {
            story.DeviceTimestamp = DateTime.Now;
            try
            {
                userstoriesfeed.Add(new StoryModel
                {
                    user = story.User.UserName,
                    userPict = imghelp.GetImage(story.User.ProfilePicUrl),
                    postPreview = imghelp.GetImage(story.ImageList[0].Uri) ?? new BitmapImage(new Uri("Images/instagram.png", UriKind.Relative)),
                    StoryId = story.Id,
                    ExpiringAt = story.ExpiringAt,
                    DeviceTimestamp = story.DeviceTimestamp
                });
            }
            catch (Exception e) { logs.Add(e.ToString(), MessageType.Type.ERROR, this.GetType().Name); }
        }
    }
}
