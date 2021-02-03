using Instagram_Assistant.Model;
using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Instagram_Assistant.Helpers.Like
{
    abstract class LikeCommon : HelperBase
    {
        //STATUS: OK
        protected List<string> seenMediasMassive;
        protected List<FeedModel> userfeed; //List For Feeds from user Instagram

        protected async Task SetLike(FeedModel user)
        {
            //skip post if true
            if (Skip() == true || seenMediasMassive.Contains(user.InstaIdentifier))
            {
                logs.Add($"Post by {user.user} was SKIPED", MessageType.Type.LIKE, this.GetType().Name);
                Delay = 5000;
                du.UpdateActions(actions, mainInstanse, user.userPict, user.user, "Ban", null, user.postPreview);
            }
            //like
            else
            {
                await Account.MediaProcessor.LikeMediaAsync(user.InstaIdentifier); //ставим лайк

                mainVars.IncrementActionsCount(mainInstanse);
                stats = du.StatsUpdate(stats, mainInstanse, null, mainVars.GetTotalCountFromProperties(mainInstanse), int.Parse(stats.SessionCount) + 1, null, null, null);

                logs.Add($"Post by {user.user} was successfully LIKED", MessageType.Type.LIKE, this.GetType().Name);
                Delay = GetLikeDelay();//ставим задержку

                du.UpdateActions(actions, mainInstanse, user.userPict, user.user, "Heart", null, user.postPreview);
                seenMediasMassive.Add(user.InstaIdentifier);

                Requests++;
            }
            await WorkTimeCheck(limits.WorkTimeLimitCheck());
            await HourPassedCheck();
            await MaxCountPassedCheck();
        }
        protected void AddPostForLike(InstaMedia media)
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
}
