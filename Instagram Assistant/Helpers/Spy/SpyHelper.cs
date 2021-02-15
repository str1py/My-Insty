using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Spy;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers
{
    class SpyHelper : HelperBase
    {
        private ObservableCollection<SpyModel> spyCollection;
        private IInstaApi spyAccount;
        public IInstaApi SpyAccount
        {
            get { return spyAccount; }
            set
            {
                accountInfoHelper.UpdateAccountStatus(spyAccount, AccountStatus.Type.REST);
                spyAccount = value;
                accountInfoHelper.UpdateAccountStatus(spyAccount, AccountStatus.Type.WORKING);
            }
        }
        private new SpyStatsModel stats;
        private readonly int DelayActionsSeconds = 300;
        private readonly int MaxTries = 5;

        private ConvertHelper convert = new ConvertHelper();
        private AccountInfoHelper accountInfo = new AccountInfoHelper();
        private TextFileHelper txtHelper = new TextFileHelper();

        public SpyHelper(CommonViewModel model)
        {
            spyCollection = new ObservableCollection<SpyModel>();
            actions = new ObservableCollection<ActionModel>();
            stats = new SpyStatsModel();
            mainInstanse = model;
        }
        
        public async Task StartSpy(int select)
        {
            timer = new DispatcherTimer();
            timerStart();

            //get accounts from exist
            SpyAccount = await accountInfoHelper.GetSpyAccountAsync();
            Account = await accountInfoHelper.GetMainAccountAsync();

            //if no accounts
            if (SpyAccount == null || Account == null)
            {
                du.UpdateActions(actions, mainInstanse,  null, "", "Info", $"Cant get spy/main account...");
                StopSpy();
            }
            else
            {
                //Set common info
                //accountInfoHelper.UpdateAccountStatus(SpyAccount, AccountStatus.Type.WORKING);
                logs.Add($"Begin spy...", MessageType.Type.DEBUGINFO, this.GetType().ToString());
                du.UpdateActions(actions, mainInstanse, null, "", "Info", $"Getting your following list");
                mainVars.ChangeProgressToTrue(mainInstanse);
                stats = SpyStatsUpdate(stats, AccountStatus.Type.WORKING.ToString(), 0, 0, "00:00:00", "00:00:00", Account.GetLoggedUser().UserName, SpyAccount.GetLoggedUser().UserName);
        
                var result = await GetSpyOnUsers(select);

                Delay = th.GetExecuteTimeSeconds(result.Count);
                du.UpdateActions(actions, mainInstanse, null, "", "Info", $"So, I will spy for {result.Count} users.");
                stats = SpyStatsUpdate(stats, AccountStatus.Type.WORKING.ToString(), 0, 0, null, th.GetNormalTime(Delay), null, null);

                if ((result != null && result.Count != 0) && mainVars.GetProgressStatus(mainInstanse))
                {
                    du.UpdateActions(actions, mainInstanse,null, "", "Info", $"Getting primary info, latest post and stories. It may takes {th.GetExecuteTime(Delay)}");
                    //Get primary users info for spy
                    foreach (var user in result)
                        await GetUserInfo(user.UserName);

                    du.UpdateActions(actions, mainInstanse, null, "", "Info", $"I`m have been get all info, I spying at them :)");
                    await Task.Delay(2000);
                    logs.Add($"I spying at them :)", MessageType.Type.DEBUGINFO, this.GetType().ToString());
                    Delay = 0;

                    Delay = DelayActionsSeconds;
                    stats = SpyStatsUpdate(stats, null, null, null, null, th.GetNormalTime(DelayActionsSeconds), null, null);
                    await Task.Delay(DelayActionsSeconds * 1000);

                    //LOOP FOR LOOKING NEW POST AND STORIES
                    do
                    {
                        await CheckForNewPosts();
                        stats = SpyStatsUpdate(stats, null, null, null, null, th.GetNormalTime(DelayActionsSeconds), null, null);
                        Delay = DelayActionsSeconds;
                        await Task.Delay(DelayActionsSeconds * 1000);

                        SpyAccount =  accountInfo.ChangeSpyAccount(SpyAccount);
                        stats = SpyStatsUpdate(stats, null, null, null, null, null, null, SpyAccount.GetLoggedUser().UserName);
                    } while (mainVars.IsSpyInProgress);
                }
                else
                {
                    logs.Add($"Cant begin spy! May couse of users count {result.Count}", MessageType.Type.ERROR, this.GetType().ToString());
                    StopSpy();
                }
            }
        }

        public void StopSpy()
        {
            du.UpdateActions(actions, mainInstanse,null, "", "Info", $"Spying has been stopped");
            accountInfoHelper.UpdateAccountStatus(SpyAccount, AccountStatus.Type.REST);
            mainVars.ChangeProgressToFalse(mainInstanse);
            stats = SpyStatsUpdate(stats, AccountStatus.Type.OFF.ToString(), 0, 0, null, null, "n/a", "n/a");
            mainInstanse.ButtonContent = "Start";
            timer.Stop();
            Delay = 0;
            timepass = 0;
        }

        private async Task<InstaUserShortList> GetSpyOnUsers(int select)
        {
            switch (select)
            {
                //FOLLOWING
                case 0:
                    var result = await Account.UserProcessor.GetUserFollowingAsync(Account.GetLoggedUser().UserName, PaginationParameters.MaxPagesToLoad(2));
                    if (result.Succeeded)
                        return result.Value;
                    else
                    {
                        logs.Add(result.Info.Message, MessageType.Type.ERROR, this.GetType().Name);
                        return null;
                    }
                //FOLLOWERS
                case 1:
                    var resultFollowers = await Account.UserProcessor.GetUserFollowersAsync(Account.GetLoggedUser().UserName, PaginationParameters.MaxPagesToLoad(2));
                    if (resultFollowers.Succeeded)
                        return resultFollowers.Value;
                    else
                    {
                        logs.Add(resultFollowers.Info.Message, MessageType.Type.ERROR, this.GetType().Name);
                        return null;
                    }
                //Best Friends
                case 2:
                    var resultBesty = await Account.UserProcessor.GetBestFriendsAsync(PaginationParameters.MaxPagesToLoad(10));
                    if (resultBesty.Succeeded)
                        return resultBesty.Value;
                    else
                    {
                        logs.Add(resultBesty.Info.Message, MessageType.Type.ERROR, this.GetType().Name);
                        return null;
                    }
                case 3:
                    var usersArray = txtHelper.TxtToStrMassive(SpyPageViewModel.Instance.SpyUsersList);
                    InstaUserShortList list = new InstaUserShortList();
                    foreach (var user in usersArray)
                    {
                        var res = await Account.UserProcessor.GetUserAsync(user);
                        if (res.Succeeded)
                        {
                            list.Add(new InstaUserShort
                            {
                                FullName = res.Value.FullName,
                                IsPrivate = res.Value.IsPrivate,
                                IsVerified = res.Value.IsVerified,
                                Pk = res.Value.Pk,
                                ProfilePicture = res.Value.ProfilePicture,
                                ProfilePictureId = res.Value.ProfilePictureId,
                                ProfilePicUrl = res.Value.ProfilePicUrl,
                                UserName = res.Value.UserName
                            });
                        }
                        else
                            logs.Add(res.Info.Message, MessageType.Type.ERROR, this.GetType().ToString());
                    }
                    return list;
                default:
                    return null;
            }
        }
        private async Task GetUserInfo(string name)
        {
            await Task.Run(async () =>
            {
                if (mainVars.GetProgressStatus(mainInstanse))
                {
                    var user = await Account.UserProcessor.GetUserAsync(name);
                    if (!user.Value.IsPrivate)
                    {
                        var id = user.Value.Pk;
                        spyCollection.Add(new SpyModel
                        {
                            user = name,
                            userPict = imageHelper.GetImage(user.Value.ProfilePicture),
                            userid = user.Value.Pk,
                            Posts = await GetLastPosts(id),
                            Stories = await GetLastStories(id)
                        });
                    }
                }
            });
        }

        private async Task<List<string>> GetLastStories(long id)
        {
            int triesCount = 0;
            if (mainVars.GetProgressStatus(mainInstanse))
            {
                List<string> stories = new List<string>();
                do
                {
                    IResult<InstaReelFeed> latestStories = null;
                    try
                    {
                        triesCount++;
                        latestStories = await SpyAccount.StoryProcessor.GetUserStoryFeedAsync(id);

                        if (latestStories.Succeeded)
                        {
                            var latest = latestStories.Value;
                            foreach (var story in latest.Items)
                                stories.Add(story.Id);
                            return stories;
                        }
                        else du.UpdateActions(actions, mainInstanse, null, "", "Info", $"{latestStories?.Info.Message}. Try №{triesCount}/{MaxTries}");
                    }
                    catch { du.UpdateActions(actions, mainInstanse, null, "", "Info", $"{latestStories?.Info.Message}. Try №{triesCount}/{MaxTries}"); }
                } while (triesCount < MaxTries);
            }
            else return null;
            return null;
        }
        private async Task<List<string>> GetLastPosts(long id)
        {
            int triesCount = 0;
            if (mainVars.GetProgressStatus(mainInstanse))
            {
                List<string> posts = new List<string>();
                do
                {
                    triesCount++;
                    IResult<InstaMediaList> latestPosts = null;
                    try
                    {
                        latestPosts = await SpyAccount.UserProcessor.GetUserMediaByIdAsync(id, PaginationParameters.MaxPagesToLoad(1));
                        if (latestPosts.Succeeded)
                        {
                            foreach (var post in latestPosts.Value)
                                posts.Add(post.InstaIdentifier);
                            return posts;
                        }
                        else du.UpdateActions(actions, mainInstanse, null, "", "Info", $"{latestPosts?.Info.Message}");
                    }
                    catch { du.UpdateActions(actions, mainInstanse, null, "", "Info", $"{latestPosts?.Info.Message}. Try №{triesCount}/{MaxTries}"); }

                } while (triesCount < MaxTries);
            }
            else return null;
            return null;
        }

        private async Task CheckForNewPosts()
        {
            try
            {
                if (mainVars.GetProgressStatus(mainInstanse))
                {
                    Delay = th.GetExecuteTimeForCheck(spyCollection.Count);
                    du.UpdateActions(actions, mainInstanse, null, "", "Info", $"Begin cheking new posts and stories");

                    foreach (var item in spyCollection)
                    {
                        //Get exist User info
                        var existUsersInfo = spyCollection.Where(x => x.userid == item.userid).FirstOrDefault();

                        //Get new post and stories
                        var posts = await GetLastPosts(item.userid); //~3 sec
                        var stories = await GetLastStories(item.userid); //2.3

                        if (posts != null && stories != null && existUsersInfo != null) 
                        {
                            //Look for new post
                            foreach (var post in posts)
                            {
                                if (existUsersInfo.Posts != null)
                                {
                                    if (!existUsersInfo.Posts.Contains(post))
                                    {
                                        logs.Add($"New post by {item.user}", MessageType.Type.DEBUGINFO, this.GetType().ToString());
                                        //like post
                                        bool result = await LikePost(post.ToString());
                                        if (result)
                                        {
                                            du.UpdateActions(actions, mainInstanse, item.userPict, item.user, "Heart", $"New post by {item.user} has been liked");
                                            logs.Add($"New post by {item.user} has been liked", MessageType.Type.DEBUGINFO, this.GetType().ToString());
                                            stats = SpyStatsUpdate(stats, null, null, int.Parse(stats.PostsCount) + 1, null, th.GetNormalTime(DelayActionsSeconds), null, null);
                                            await Task.Delay(5000);
                                        }
                                        var linqPosts = spyCollection.Where(x => x.user == item.user).FirstOrDefault();
                                        linqPosts?.Posts.Add(post.ToString());
                                    }
                                }
                            }

                            //Look for new stories
                            foreach (var story in stories)
                            {
                                if (existUsersInfo.Stories != null)
                                {
                                    if (!existUsersInfo.Stories.Contains(story))
                                    {
                                        logs.Add($"New stoty by {item.user}", MessageType.Type.DEBUGINFO, this.GetType().ToString());
                                        //wathc story
                                        bool result = await WatchStory(story.ToString());
                                        if (result)
                                        {
                                            du.UpdateActions(actions, mainInstanse, item.userPict, item.user, "Eye", $"New story by {item.user} has been watched");
                                            logs.Add($"New story by {item.user} was watched", MessageType.Type.DEBUGINFO, this.GetType().ToString());
                                            stats = SpyStatsUpdate(stats, null, int.Parse(stats.StoriesCount) + 1, null, null, th.GetNormalTime(DelayActionsSeconds), null, null);
                                            await Task.Delay(5000);
                                        }
                                        var linqStories = spyCollection.Where(x => x.user == item.user).FirstOrDefault();
                                        linqStories?.Stories.Add(story.ToString());
                                    }
                                }
                            }
                        }    
                    }
                    du.UpdateActions(actions, mainInstanse, null, "", "Info", $"Cheking new posts and stories has been seccusesfully completed. Rest for a while");
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async Task<bool> LikePost(string postId)
        {
            if (mainVars.GetProgressStatus(mainInstanse))
            {
                var result = await Account.MediaProcessor.LikeMediaAsync(postId); //ставим лайк
                await Task.Delay(4000);
                return result.Succeeded;
            }
            else return false;
        }
        private async Task<bool> WatchStory(string storyId)
        {
            if (mainVars.GetProgressStatus(mainInstanse))
            {
                var result = await Account.StoryProcessor.MarkStoryAsSeenAsync(storyId, th.GetInixTime());
                await Task.Delay(4000);
                return result.Succeeded;
            }
            else return false;
        }

        private SpyStatsModel SpyStatsUpdate(SpyStatsModel _stats,string _status,int? _scount,int? _pcount,string _worktime,string _nextsessionin,string _mainacc,string _techacc)
        {
            var stats = new SpyStatsModel()
            {
                Status = _status ?? _stats.Status,
                NextSessionIn = _nextsessionin ?? _stats.NextSessionIn ?? "00:00:00",
                StoriesCount = convert.BigNumbersCutting(_scount) ?? convert.BigNumbersCutting(_stats.StoriesCount),
                PostsCount = convert.BigNumbersCutting(_pcount) ?? convert.BigNumbersCutting(_stats.PostsCount),
                TimeInWork = _worktime ?? _stats.TimeInWork ?? "00:00:00",
                MainAccount = _mainacc ?? _stats.MainAccount,
                TechAccount = _techacc ?? _stats.TechAccount
            };

            SpyPageViewModel.Instance.Stats = stats;
            return stats;
        }
        protected override void timerTick(object sender, EventArgs e)
        {
            timepass++;
            this.stats.TimeInWork = th.GetNormalTime(timepass);
            stats = SpyStatsUpdate(stats, null, null, null, null, null, null, null);
            if (Delay > 0)
            {
                if (Delay > 1000)
                    Delay /= 1000;

                Delay--;
                stats =  SpyStatsUpdate(stats, null, null, null, null, th.GetNormalTime(Delay), null, null);
            }
        }
    }
}
