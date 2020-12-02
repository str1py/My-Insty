using Instagram_Assistant.Enums;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class AccountInfoHelper
    {
        //STATUS: +- OK 

        public static ObservableCollection<AccountInfo> info { get; private set; } = new ObservableCollection<AccountInfo>();
        private ImageHelpers img = new ImageHelpers();
        private ConvertHelper convert = new ConvertHelper();

        //Get Instagram Account Info
        public async Task GetInfo()
        {
            StringCollection stringCollection = Properties.Settings.Default.AccountsRoles;
            info.Clear();
            IResult<InstaUserInfo> userinfo;
            if (stringCollection == null)
            {
                foreach (var users in LogInHelper.LoggedInUsers)
                {
                    stringCollection = new StringCollection();
                    userinfo = await users.UserProcessor.GetUserInfoByUsernameAsync(users.GetLoggedUser().UserName);
                    info.Add(new AccountInfo
                    {
                        userName = users.GetLoggedUser().LoggedInUser.UserName,
                        postCount = userinfo.Value.MediaCount.ToString() + " " + "posts",
                        followersCount = userinfo.Value.FollowerCount.ToString() + " " + "followers",
                        followingCount = userinfo.Value.FollowingCount.ToString() + " " + "following",
                        accountRole = 1,
                        accountStatus = await GetAccountStatus(users),
                        image = img.GetImage(users.GetLoggedUser().LoggedInUser.ProfilePicUrl),
                        ChallengeReqVisibility = convert.CoverterBoolToVisibility(false),
                        LoginReqVisibility = convert.CoverterBoolToVisibility(false)
                    });
                    stringCollection.Add(users.GetLoggedUser().UserName + ";1");
                    Properties.Settings.Default.AccountsRoles = stringCollection;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                var list = stringCollection.Cast<string>().ToList();
                foreach (var users in LogInHelper.LoggedInUsers)
                {
                    string _string = list.Where(x => x.Contains(users.GetLoggedUser().UserName)).FirstOrDefault();
                    if (_string == null)
                    {
                        userinfo = await users.UserProcessor.GetUserInfoByUsernameAsync(users.GetLoggedUser().UserName);
                        info.Add(new AccountInfo
                        {
                            userName = users.GetLoggedUser().LoggedInUser.UserName,
                            postCount = userinfo.Value.MediaCount.ToString() + " " + "posts",
                            followersCount = userinfo.Value.FollowerCount.ToString() + " " + "followers",
                            followingCount = userinfo.Value.FollowingCount.ToString() + " " + "following",
                            accountRole = 1,
                            accountStatus = await GetAccountStatus(users),
                            image = img.GetImage(users.GetLoggedUser().LoggedInUser.ProfilePicUrl),
                            ChallengeReqVisibility = convert.CoverterBoolToVisibility(false),
                            LoginReqVisibility = convert.CoverterBoolToVisibility(false)
                        });
                        stringCollection.Add(users.GetLoggedUser().UserName + ";1");
                        Properties.Settings.Default.AccountsRoles = stringCollection;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        var sep = _string.Split(';');

                        userinfo = await users.UserProcessor.GetUserInfoByUsernameAsync(users.GetLoggedUser().UserName);
                        if (userinfo.Succeeded)
                            {
                            info.Add(new AccountInfo
                            {
                                userName = users.GetLoggedUser().UserName,
                                postCount = userinfo.Value.MediaCount.ToString() + " " + "posts",
                                followersCount = userinfo.Value.FollowerCount.ToString() + " " + "followers",
                                followingCount = userinfo.Value.FollowingCount.ToString() + " " + "following",
                                accountRole = Convert.ToInt32(sep[1]),
                                accountStatus = await GetAccountStatus(users),
                                image = img.GetImage(users.GetLoggedUser().LoggedInUser.ProfilePicUrl),
                                ChallengeReqVisibility = convert.CoverterBoolToVisibility(false),
                                LoginReqVisibility = convert.CoverterBoolToVisibility(false)
                            });
                        }
                        else
                        {
                            var response = userinfo.Info.ResponseType;

                            info.Add(new AccountInfo
                            {
                                userName = users.GetLoggedUser().UserName,
                                postCount = "n/a" + " " + "posts",
                                followersCount = "n/a" + " " + "followers",
                                followingCount = "n/a" + " " + "following",
                                accountRole = Convert.ToInt32(sep[1]),
                                accountStatus = convert.ResponseToAccountType(userinfo.Info.ResponseType.ToString()),
                                image = img.GetImage(users.GetLoggedUser().LoggedInUser.ProfilePicUrl),
                                ChallengeReqVisibility = convert.CoverterBoolToVisibility((response == ResponseType.ChallengeRequired) ? true : false),
                                LoginReqVisibility = convert.CoverterBoolToVisibility((response == ResponseType.LoginRequired) ? true : false),
                            });
                        }
                    }
                }
            }
        }

        public void UpdateInfo(AccountInfo account)
        {
            var acc = info.Where(x => x.userName == account.userName).FirstOrDefault();

            var stringCollection = Properties.Settings.Default.AccountsRoles;
            var list = stringCollection.Cast<string>().ToList();
            var _string = list.Where(x => x.Contains(account.userName)).FirstOrDefault();
            int index = list.IndexOf(_string.ToString());
            list[index] = account.userName + ";" + account.accountRole;
            StringCollection collection = new StringCollection();
            collection.AddRange(list.ToArray());
            Properties.Settings.Default.AccountsRoles.Clear();
            Properties.Settings.Default.AccountsRoles = collection;
            Properties.Settings.Default.Save();
            //DOESNT SAVE
        }
        public async Task UpdateInfo(IInstaApi user)
        {
            if (user != null)
            {
                var userinfo = await user.UserProcessor.GetUserInfoByUsernameAsync(user.GetLoggedUser().UserName);
                var response = userinfo.Info.ResponseType;
                var acc = info.Where(x => x.userName == user.GetLoggedUser().UserName).FirstOrDefault();
                if (acc == null)
                {

                    info.Add(new AccountInfo
                    {
                        userName = user.GetLoggedUser().LoggedInUser.UserName,
                        postCount = userinfo.Value.MediaCount.ToString() + " " + "posts",
                        followersCount = userinfo.Value.FollowerCount.ToString() + " " + "followers",
                        followingCount = userinfo.Value.FollowingCount.ToString() + " " + "following",
                        accountRole = 1,
                        accountStatus = await GetAccountStatus(user),
                        image = img.GetImage(user.GetLoggedUser().LoggedInUser.ProfilePicUrl),
                        ChallengeReqVisibility = convert.CoverterBoolToVisibility((response == ResponseType.ChallengeRequired) ? true : false),
                        LoginReqVisibility = convert.CoverterBoolToVisibility((response == ResponseType.LoginRequired) ? true : false)
                    });
                }
                else
                {
                    acc.postCount = userinfo.Value.MediaCount.ToString() + " " + "posts";
                    acc.followersCount = userinfo.Value.FollowerCount.ToString() + " " + "followers";
                    acc.followingCount = userinfo.Value.FollowingCount.ToString() + " " + "following";
                    acc.ChallengeReqVisibility = convert.CoverterBoolToVisibility((response == ResponseType.ChallengeRequired) ? true : false);
                    acc.LoginReqVisibility = convert.CoverterBoolToVisibility((response == ResponseType.LoginRequired) ? true : false);
                    acc.accountStatus = await GetAccountStatus(user);
                }
                AccountPageViewModel.Instanse.GetInfo();
            }
        }

        private async Task<AccountStatus.Type> GetAccountStatus(IInstaApi user, string competitor = null)
        {
            var result = await user.UserProcessor.GetUserInfoByUsernameAsync(competitor ?? "instagram");
            var result1 = await user.UserProcessor.GetUserFollowersAsync(competitor ?? "instagram", PaginationParameters.MaxPagesToLoad(1));
            if (result.Succeeded == true && result1.Succeeded == true)
            {
                if (result.Info.NeedsChallenge == false & result1.Info.NeedsChallenge == false)
                    return AccountStatus.Type.REST;
                else if (result.Info.ResponseType == ResponseType.Spam)
                    return AccountStatus.Type.SPAM;
                else return AccountStatus.Type.BAN;
            }
            else return AccountStatus.Type.BAN;
        }
        public async Task<AccountStatus.Type> GetAccountsStatus()
        {
            foreach(var user in LogInHelper.LoggedInUsers)
            {
                var result = await user.UserProcessor.GetUserInfoByUsernameAsync("instagram");
                var result1 = await user.UserProcessor.GetUserFollowersAsync("instagram", PaginationParameters.MaxPagesToLoad(1));
                if (result.Succeeded == true && result1.Succeeded == true)
                {
                    if (result.Info.NeedsChallenge == false && result1.Info.NeedsChallenge == false)
                        return AccountStatus.Type.REST;
                    else return AccountStatus.Type.BAN;
                }
                else return AccountStatus.Type.BAN;
            }
            return AccountStatus.Type.BAN;
        }

        public IInstaApi ChangeTechAccount()
        {
            var acc = info.Where(x => x.accountRole == 1).Where(x => x.accountStatus == AccountStatus.Type.REST).FirstOrDefault();
            if (acc != null)
            {
                foreach (var user in LogInHelper.LoggedInUsers)
                    if (user.GetLoggedUser().UserName == acc.userName)
                        return user;
                    
            }
            else return null;

            return null;
        }

        public async Task<IInstaApi> ChangeTechAccount(IInstaApi account,string competitor = null)
        {
            foreach (var user in info)
            {
                if (account.GetLoggedUser().UserName == user.userName)
                    user.accountStatus = await GetAccountStatus(account, competitor ?? "pavelanulyev");
            }

            var accounts = info.Where(x => x.accountRole == 1);
            var acc = accounts.Where(x => x.accountStatus == AccountStatus.Type.REST).FirstOrDefault();
            if (acc != null)
            {
                foreach (var user in LogInHelper.LoggedInUsers)
                    if (user.GetLoggedUser().UserName == acc.userName)
                    {
                        acc.accountStatus = AccountStatus.Type.WORKING;
                        return user;
                    }
            }
            else return null;

            return null;
        }
        public async Task<IInstaApi> NeededChangeTechAccount(IInstaApi account)
        {
            foreach (var user in info)
            {
                if (account.GetLoggedUser().UserName == user.userName)
                    user.accountStatus = await GetAccountStatus(account);
            }

            var accounts = info.Where(x => x.accountRole == 1);
            var acc = accounts.Where(x => x.accountStatus == AccountStatus.Type.REST).Where(x => x.userName != account.GetLoggedUser().UserName).ToList();

            Random rnd = new Random();
            int select = rnd.Next(0, acc.Count);

            foreach (var user in LogInHelper.LoggedInUsers)
                if (user.GetLoggedUser().UserName == acc[select].userName)
                {
                    acc[select].accountStatus = AccountStatus.Type.WORKING;
                    return user;
                }

            return null;
        }

        public IInstaApi GetMainAccount()
        {
            var accounts = info.Where(x => x.accountRole == 0);
            var acc = accounts.Where(x => x.accountStatus != AccountStatus.Type.BAN).FirstOrDefault();
            if (acc != null)
            {
                foreach (var user in LogInHelper.LoggedInUsers)
                    if (user.GetLoggedUser().UserName == acc.userName)
                        return user;
            }
            else return null;

            return null;
        }

        public void UpdateAccountStatus(IInstaApi api, AccountStatus.Type type)
        {
            if (api != null)
            {
                var acc = info.Where(x => x.userName == api.GetLoggedUser().UserName).FirstOrDefault();
                acc.accountStatus = type;
            }
        }

        public void DeleteAccount(string username)
        {
            var acc = info.Where(x => x.userName == username).FirstOrDefault();
            info.Remove(acc);
        }

    }
}
