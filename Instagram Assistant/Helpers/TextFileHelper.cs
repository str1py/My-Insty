using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class TextFileHelper
    {   
        //STATUS: NOT OK
        private LogsPageViewModel logs = LogsPageViewModel.Instance;
        private MainVars mainVars = new MainVars();
        private ConvertHelper convert = new ConvertHelper();
        private ImageHelpers imageHelper = new ImageHelpers();

        public string[] TxtToStrMassive(string path)
        {
            if (File.Exists(path))
                return File.ReadAllLines(path, Encoding.UTF8);
            else return null;

        }

        public void SaveAudienceToTxtFile(string path, List<AudienceActionModel> audience)
        {
            List<string> audienceList = new List<string>();

            foreach (var user in audience)
            {
                var line = $"{user.AccountName};{user.FullName};{user.AccountID};{user.Phone};{user.Email};{user.AccountType};{user.AccountCategory};{user.City};" +
                    $"{user.IsCity};{user.MediaCount};{user.FollowersCount};{user.HasBio};{user.HasHighlight};{user.ImageLink};" +
                    $"{user.HasImage};{user.HasStopWord};{user.HasGoWord}";

                audienceList.Add(line);
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(line);
                }
            }
            if (!File.Exists(path))
            {
                File.Create(path);
                File.WriteAllLines(path, audienceList.ToArray());
            }

            audienceList.Clear();
        }
        public void SaveAudienceToTxtFile(string path, List<AudienceModel> audience)
        {
            List<string> audienceList = new List<string>();

            foreach (var user in audience)
            {
                var line = $"{user.userName};{user.userId}";
                audienceList.Add(line);
            }

            if (File.Exists(path))
                File.WriteAllLines(path, audienceList.ToArray());
            else
            {
                File.Create(path);
                File.WriteAllLines(path, audienceList.ToArray());
            }

            audienceList.Clear();
        }
        public void SaveFilterAudienceToTxtFile(string path, List<AudienceModel> audience)
        {
            foreach (var user in audience)
            {
                string linetosave = "";
                var line = $"{user.userName};{user.userId};{user.phone};{user.email}";

                if (Properties.Settings.Default.IsNameSave)
                    linetosave = linetosave + $"{user.userName};";
                if (Properties.Settings.Default.IsIdSave)
                    linetosave = linetosave + $"{user.userId};";
                if (Properties.Settings.Default.IsPhoneSave)
                    linetosave = linetosave + $"{user.phone};";
                if (Properties.Settings.Default.IsEmailSave)
                    linetosave = linetosave + $"{user.email};";


                if (File.Exists(path))
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(linetosave);
                    }
                }
                else
                {
                    File.Create(path);
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(linetosave);
                    }
                }
            }

        }

        //FOR FILTER METHOD ONLY
        public async Task<List<AudienceActionModel>> GetAudienceFromTxtFile(string path)
        {
            List<AudienceActionModel> sepList = new List<AudienceActionModel>();
            string[] list;

            if (File.Exists(path))
            {
                list = File.ReadAllLines(path, Encoding.UTF8);

                CancellationTokenSource cts = new CancellationTokenSource();
                await Task.Factory.StartNew(() =>
                                     Parallel.ForEach(list, user =>
                                     {
                                         if (mainVars.IsAudienceFilterInProgress == false)
                                             cts.Cancel();
                                         try
                                         {
                                             string[] separate = user.Split(';');
                                             FilterAudiencePageViewModel.Instance.LastActionTextHelper = $"Get {separate[0]} info...";
                                             AudienceActionModel audience = new AudienceActionModel(separate[0], separate[1], long.Parse(separate[2]), separate[3], separate[4],
                                                       convert.AccTypeToInt(separate[5]), separate[6], separate[7], Convert.ToBoolean(separate[8]), Convert.ToInt32(separate[9]),
                                                       Convert.ToInt32(separate[10]), separate[11], convert.YesNoToBoolean(separate[12]),
                                                       separate[13], convert.YesNoToBoolean(separate[14]), convert.YesNoToBoolean(separate[15]), "Unknown");

                                             App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                             {
                                                 sepList.Add(audience);
                                             });

                                         }
                                         catch (Exception e)
                                         {
                                             logs.Add($"{e.Message}", MessageType.Type.ERROR, this.GetType().Name);
                                         }
                                     })
                                    );
                FilterAudiencePageViewModel.Instance.LastActionTextHelper = $"Getiing audience info finished.";
                await Task.Delay(3000);
                return sepList;
            }
            else return null;
        }
        public async Task<List<AudienceModel>> GetAudienceFromTxtFileShort(string path)
        {
            List<AudienceModel> sepList = new List<AudienceModel>();
            string[] list;
            logs.Add($"Get existing audience info...", MessageType.Type.AUDIENCE, this.GetType().Name);

            if (File.Exists(path))
            {
                list = File.ReadAllLines(path, Encoding.UTF8);

                await Task.Factory.StartNew(() =>
                                     Parallel.ForEach(list, user =>
                                     {
                                         Task.Delay(1000);
                                         try
                                         {
                                             string[] separate = user.Split(';');
                                             AudienceModel audience = new AudienceModel { userName = separate[0], userId = long.Parse(separate[2]) };
                                             sepList.Add(audience);
                                         }
                                         catch
                                         {
                                             string[] separate = user.Split(';');
                                         }

                                     })
                                    );
                return sepList;
            }
            else return null;
        }

        //FOR UNFOLLOW
        public async Task<List<UnfollowModel>> GetExistUnfollowList(string path)
        {        
            List<UnfollowModel> userunfollow = new List<UnfollowModel>(); 
            int count = 0;
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                logs.Add($"Loading list from file", MessageType.Type.UNFOLLOW, this.GetType().Name);

                await Task.Factory.StartNew(() =>
                {
                    count++;
                    UnfollowPageViewModel.Instance.LastActionTextHelper = $"Getting followers from file... ({count})";
                });
                 
                foreach (var line in lines)
                {
                    var data = line.Split(';');
                    userunfollow.Add(new UnfollowModel
                    {
                        user = data[0],
                        isfollowback = false,
                        userid = long.Parse(data[1]),
                        userPict = imageHelper.GetImage(data[2])
                    });
                }
                await Task.Factory.StartNew(() =>
                {
                    UnfollowPageViewModel.Instance.LastActionTextHelper = $"";
                });
                return userunfollow;
            }
            else return null;
        }
    }
}
