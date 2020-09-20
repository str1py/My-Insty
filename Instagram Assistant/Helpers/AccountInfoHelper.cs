using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class AccountInfoHelper
    {
        public static AccountInfo info;
        private ImageHelpers img = new ImageHelpers();

        public async Task GetInfo()
        {
            var user =  LogInHelper._instaApi.GetLoggedUser().LoggedInUser.UserName;
            var photo = LogInHelper._instaApi.GetLoggedUser().LoggedInUser.ProfilePicUrl;
            var userinfo = await LogInHelper._instaApi.UserProcessor.GetUserInfoByUsernameAsync(user);

            info = new AccountInfo
            {
                userName = user,
                postCount = userinfo.Value.MediaCount.ToString() + " " + "posts" ,
                followersCount = userinfo.Value.FollowerCount.ToString() + " " + "followers",
                followingCount = userinfo.Value.FollowingCount.ToString() + " " + "following",
                image = img.GetImage(photo)
            };
        }

        public AccountInfo GetAccount()
        {
            return info;
        }
    }
}
