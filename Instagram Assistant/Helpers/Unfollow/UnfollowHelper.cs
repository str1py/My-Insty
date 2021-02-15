using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using Instagram_Assistant.ViewModel.BaseModels;
using InstagramApiSharp;
using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Instagram_Assistant.Helpers.Unfollow
{
    class UnfollowHelper : HelperBase
    {
        #region VARS
        private string username { get; set; }
        private string fileName = @"followinglist";
        private string path { get; set; }

        private List<UnfollowModel> userunfollow = new List<UnfollowModel>(); //List For Feeds from user Instagram
        private TextFileHelper textFileHelper = new TextFileHelper();
        #endregion

        public UnfollowHelper(CommonViewModel model)
        {
            path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            stats = new StatsModelBase(); 
            actions = new ObservableCollection<ActionModel>(); //What app doing 
            mainInstanse = model;
        }

        private async Task<List<UnfollowModel>> GetUserFollowing()
        {
            Account = await accountInfoHelper.GetMainAccountAsync();
            int count = 0;
            if (Account != null)
            {
                userunfollow.Clear();
                username = Account.GetLoggedUser().LoggedInUser.UserName;
                fileName = fileName + "-" + username + ".txt";

                var unfollow = await textFileHelper.GetExistUnfollowList(fileName);

                if(unfollow != null && unfollow.Count != 0)
                {
                    return unfollow;
                }
                else
                {
                    try
                    {
                        File.Delete(path + "\\" + fileName);
                    }
                    catch(Exception e)
                    {
                        logs.Add(e.Message, MessageType.Type.ERROR, this.GetType().Name);
                    };

                    File.Create(path + "\\" + fileName);
                    try
                    {
                        var result = await Account.UserProcessor.GetUserFollowingAsync(username, PaginationParameters.MaxPagesToLoad(1));
                        if (result.Succeeded == true)
                        {
                            using (StreamWriter file = new StreamWriter(path + "\\" + fileName))
                            {
                                foreach (var user in result.Value)
                                {
                                    await Task.Factory.StartNew(() =>
                                    {
                                        count++;
                                        UnfollowPageViewModel.Instance.LastActionTextHelper = $"Getting followers... ({count})";
                                    });
                                    await Task.Delay(100);
                                    userunfollow.Add(new UnfollowModel
                                    {
                                        user = user.UserName,
                                        isfollowback = false,
                                        userid = user.Pk,
                                        userPict = imageHelper.GetImage(user.ProfilePicture)
                                    });
                                    await file.WriteLineAsync($"{user.UserName};{user.Pk};{user.ProfilePicture}");
                                }
                            }
                            return userunfollow;
                        }
                        else
                        {
                            logs.Add($"{result.Info}", MessageType.Type.ERROR, this.GetType().Name);
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        logs.Add($"{e.Message}", MessageType.Type.ERROR, this.GetType().Name);
                        return null;
                    }
                }
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Stop(mainInstanse);
                return null;
            }
        }
        private async Task<bool> IsUserFollowBack(long userid)
        {
            try
            {
                var result = await Account.UserProcessor.GetFriendshipStatusAsync(userid);
                if (!result.Succeeded)
                {
                    logs.Add($"{result.Info.Message}", MessageType.Type.ERROR, this.GetType().Name);
                    Stop(mainInstanse);
                    return false;
                }
                else
                {
                    return result.Value.FollowedBy;
                }
            }
            catch (Exception e) 
            { 
                logs.Add($"{e.Message}", MessageType.Type.ERROR, this.GetType().Name); 
                return false; 
            }
        }
        public async Task BeginUnfollow()
        {
            if (await InitCommonData(mainInstanse))
            {
                timerStart();

                var following = await GetUserFollowing();
                if (following != null && mainVars.IsUnfollowInProgress == true)
                {
                    logs.Add($"Find {following.Count} users you follow", MessageType.Type.UNFOLLOW, this.GetType().Name);
                    foreach (var user in following)
                    {
                        await WorkTimeCheck(WorkTimeLimitCheck());
                        await HourPassedCheck();
                        await MaxCountPassedCheck();

                        if (mainVars.IsUnfollowInProgress == false)
                            break;

                        if (Properties.Settings.Default.IsUnfollowIfFollowing == true)
                        {
                            var result = await Account.UserProcessor.UnFollowUserAsync(user.userid);
                            if (result.Succeeded)
                                await Unfollow(user, "Unfollow All");
                            else
                            {
                                    logs.Add($"Unsucceeded! {result.Info.ResponseType}",MessageType.Type.ERROR, this.GetType().ToString());
                                    Stop(mainInstanse);                              
                            }
                        }
                        else
                        {
                            if (await IsUserFollowBack(user.userid) == false)
                            {
                                var result = await Account.UserProcessor.UnFollowUserAsync(user.userid);
                                if (result.Succeeded)
                                    await Unfollow(user, "Not Following");
                                else
                                {
                                    logs.Add($"Unsucceeded! {result.Info.ResponseType}", MessageType.Type.ERROR, this.GetType().ToString());
                                    Stop(mainInstanse);
                                }
                            }
                            else
                            {
                                du.UpdateActions(actions, mainInstanse, user.userPict, user.user, "Follower", "UserCheck");
                                logs.Add($"{user.user} is your follower", MessageType.Type.UNFOLLOW, this.GetType().Name);
                                UpdateFileAndList(user.userid);
                                await Task.Delay(5000);
                            }
                        }
                    }
                    Stop(mainInstanse);
                    File.Delete(path + "\\" + fileName);
                    logs.Add($"Unfollow has been stopped and list has been deleted. ", MessageType.Type.UNFOLLOW, this.GetType().Name);
                }
                else
                {
                    logs.Add($"There is no user list loaded. Unfollow was stopped", MessageType.Type.UNFOLLOW, this.GetType().Name);
                    Stop(mainInstanse);
                }
            }
            else
            {
                MessageBox.Show($"There is no main account. Please log in and try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Stop(mainInstanse);
            }
        }

        private void UpdateFileAndList(long removestr)
        {

            List<string> linesList = File.ReadAllLines(path +"\\"+ fileName).ToList();

            foreach(var line in linesList)
            {
                bool cont = line.Contains(removestr.ToString());
                if (cont)
                {
                    linesList.Remove(line);
                    break;
                }
            }

            File.WriteAllLines((path + "\\" + fileName), linesList.ToArray());

        }
        private async Task Unfollow(UnfollowModel user,string _status)
        {
            logs.Add($"Unfollow from {user.user}", MessageType.Type.UNFOLLOW, this.GetType().Name);

            Properties.Settings.Default.UnfollowCount++;
            Properties.Settings.Default.Save();

            int? sess = Convert.ToInt32(stats.SessionCount);
            stats.SessionCount = (sess+1).ToString();
            du.StatsUpdate(stats, mainInstanse, null, Properties.Settings.Default.UnfollowCount, sess, null, null, null);

            du.UpdateActions(actions, mainInstanse, user.userPict, user.user, "UserTimes", _status );
            await Task.Delay(1000);

            Delay = GetUnfollowDelay();
            logs.Add($"Next unfollow in {Delay / 1000} seconds", MessageType.Type.DEBUGINFO, this.GetType().Name);

            await Task.Delay(Delay);
            UpdateFileAndList(user.userid);
        }

    }
}
