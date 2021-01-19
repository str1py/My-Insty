using Instagram_Assistant.Helpers;
using Instagram_Assistant.SplashScreen;
using Instagram_Assistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace Instagram_Assistant.AutoUpdate
{
    public class Update
    {
        private readonly string Login = "";
        private readonly string Password = "";
        private string HostUrl = "";
        private static string path = Directory.GetCurrentDirectory() + "\\";
        private string fileWithHashed = "FilesVersions.xml";
        private string fileWithVersion = "version.xml";

        private Dictionary<string, Uri> filesinfo = new Dictionary<string, Uri>();
        public static string NewVersion { get; set; }

        private Md5HashHelper md5helper = new Md5HashHelper();
        private LogsPageViewModel logs = new LogsPageViewModel();
        private SplashScreenViewModel splashScreen = SplashScreenViewModel.Instanse;

        public Update()
        {
          //  GetFilesInfoLocal();
        }

        private async Task<XmlDocument> GetXmlFromServer(string filename)
        {
            FtpWebRequest request = ConnectionForDownloadFile(HostUrl, filename);
            XmlDocument serverv = new XmlDocument();
            WebResponse response = await request.GetResponseAsync();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string xml = reader.ReadToEnd();
            serverv.LoadXml(xml);
            return serverv;
        }

        //WORKING
        public void GetFilesHashesLocal()
        {
            XDocument xDoc = XDocument.Load(path + fileWithHashed);
            FilesToGetInfo();

            foreach (KeyValuePair<string, Uri> pair in filesinfo)
            {
                {
                    var info = md5helper.GetMd5Hash(path + pair.Key);
                    xDoc.Root.Element(pair.Key).Value = info;
                }
            }

            var updinfo = md5helper.GetMd5Hash(path + "Update.exe");
            xDoc.Root.Element("Update.exe").Value = updinfo;

            var appdinfo = md5helper.GetMd5Hash(path + "Instagram Assistant.exe");
            xDoc.Root.Element("InstagramAssistant.exe").Value = appdinfo;

            xDoc.Save(path + fileWithHashed);
        }

        //WORKING
        public async Task<bool> IsFilesExists()
        {
            XDocument xDoc = XDocument.Load(path + fileWithHashed);
            FilesToGetInfo();

            foreach (KeyValuePair<string, Uri> pair in filesinfo)
            {
                if(!File.Exists(path + pair.Key))
                { 
                    await Task.Delay(2000);
                    await App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        splashScreen.SplashScreenText = $"Cant find {pair.Key}. Download missing file...";
                    }));

                    await Task.Delay(2000);
                    await DownloadFileAsync(pair.Key, pair.Key);
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CheckForNewFiles()
        {
            XmlDocument serverv = await GetXmlFromServer(fileWithHashed);
            try
            {
                var root = serverv.DocumentElement;
                foreach (XmlElement attr in root)
                {
                    if (!File.Exists(path + attr.Name))
                    {
                        if (!attr.Name.ToString().Contains(".exe"))
                        { 
                            if (await DownloadFileAsync(attr.Name,attr.Name))
                            {
                                XDocument xDoc = XDocument.Load(path + fileWithHashed);
                                XmlWriter writer = xDoc.Root.CreateWriter();
                                writer.WriteElementString(attr.Name, md5helper.GetMd5Hash(path + attr.Name));
                                writer.Close();
                                xDoc.Save(path + fileWithHashed);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logs.Add($"{ex}", MessageType.Type.ERROR, this.GetType().Name);
                return true;
            }
        }

        //WORKING
        public async Task<bool> CompareFilesHash()
        {
            XmlDocument localv = new XmlDocument();
            localv.Load(path + fileWithHashed);

            XmlDocument serverv = await GetXmlFromServer(fileWithHashed);

            try
            {
                foreach (KeyValuePair<string, Uri> pair in filesinfo)
                {
                    var hashFromServer = serverv.GetElementsByTagName(pair.Key)[0].InnerText;
                    var hashFromLocal = localv.GetElementsByTagName(pair.Key)[0].InnerText;
                    if (hashFromLocal != hashFromServer)
                        return false;                    
                }
                return true;
            }
            catch (Exception ex)
            {
                logs.Add($"{ex}", MessageType.Type.ERROR, this.GetType().Name);
                return true;
            }
        }

        public async Task<bool> RepairFiles()
        {
            XmlDocument localv = new XmlDocument();
            localv.Load(path + fileWithHashed);

            XmlDocument serverv = await GetXmlFromServer(fileWithHashed);

            try
            {
                foreach (KeyValuePair<string, Uri> pair in filesinfo)
                {
                    var hashFromServer = serverv.GetElementsByTagName(pair.Key)[0].InnerText;
                    var hashFromLocal = localv.GetElementsByTagName(pair.Key)[0].InnerText;
                    if (hashFromLocal != hashFromServer)
                       await DownloadFileAsync(pair.Key, pair.Key);
                }
                return true;
            }
            catch (Exception ex)
            {
                logs.Add($"{ex}", MessageType.Type.ERROR, this.GetType().Name);
                return false;
            }
        }


        //WORKING
        public async Task<bool> CompareProgrammVersions()
        {
            Version myVersion;
            try
            {
                var Version = FileVersionInfo.GetVersionInfo(path + "Instagram Assistant.exe");
                myVersion = new Version(Version.FileVersion);
            }catch(Exception e)
            {
                myVersion = null;
                MessageBox.Show(e.Message);
            }


            XmlDocument doc = await GetXmlFromServer(fileWithVersion);
            var versionFromServer = new Version(doc.GetElementsByTagName("version")[0].InnerText);

            Properties.Settings.Default.Save();

            NewVersion = versionFromServer.ToString();

            if (myVersion < versionFromServer)
                return true;            
            else
                return false;

        }


        public async void BeginUpdate()
        {
            string path1 = path + "Update.exe";

            System.Diagnostics.Process.Start(path1);

            await Task.Delay(3000);
            App.Current.Shutdown();
        }

        public async Task UpdateUpdater()
        {
            var myVersion = FileVersionInfo.GetVersionInfo(path + "Update.exe");
            Version Version = new Version(myVersion.FileVersion);

            XmlDocument doc = await GetXmlFromServer(fileWithVersion);
            var versionFromServer = new Version(doc.GetElementsByTagName("updaterversion")[0].InnerText);

            if (Version < versionFromServer)
            {
                bool a = await DownloadFileAsync("Update.exe","Update.update");
                bool result = await ReplaceFiles("Update.exe", "Update.update");
            }
        }

        public async Task UpdateAssistant()
        {
            var myVersion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;

            XmlDocument doc = await GetXmlFromServer(fileWithVersion);
            var versionFromServer = new Version(doc.GetElementsByTagName("version")[0].InnerText);

            if (myVersion < versionFromServer)
            {
                await DownloadFileAsync("Instagram Assistant.exe", "Instagram Assistant.update");
                bool result = await ReplaceFiles("Instagram Assistant.exe", "Instagram Assistant.update");
            }
        }

        void FilesToGetInfo()
        {
            filesinfo?.Clear();
            filesinfo.Add("FontAwesome.WPF.dll", new Uri(HostUrl + "FontAwesome.WPF.dll"));
            filesinfo.Add("InstagramApiSharp.dll", new Uri(HostUrl + "InstagramApiSharp.dll"));
            filesinfo.Add("Microsoft.WindowsAPICodePack.dll", new Uri(HostUrl + "Microsoft.WindowsAPICodePack.dll"));
            filesinfo.Add("Microsoft.WindowsAPICodePack.Shell.dll", new Uri(HostUrl + "Microsoft.WindowsAPICodePack.Shell.dll"));
            filesinfo.Add("Microsoft.WindowsAPICodePack.ShellExtensions.dll", new Uri(HostUrl + "Microsoft.WindowsAPICodePack.ShellExtensions.dll"));
            filesinfo.Add("Microsoft.Xaml.Behaviors.dll", new Uri(HostUrl + "Microsoft.Xaml.Behaviors.dll"));
            filesinfo.Add("Newtonsoft.Json.dll", new Uri(HostUrl + "Newtonsoft.Json.dll"));
            filesinfo.Add("PresentationFramework.Aero2.dll", new Uri(HostUrl + "PresentationFramework.Aero2.dll"));
            //filesinfo.Add("Update.exe ", new Uri(HostUrl + "Update.exe "));
            //filesinfo.Add("Instagram Assistant.exe ", new Uri(HostUrl + "Instagram Assistant.exe "));
           // filesinfo.Add("testfile.dll", new Uri(HostUrl + "testfile.dll"));
        }


        protected FtpWebRequest ConnectionForDownloadFile(string url, string file)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + file);
            request.Credentials = new NetworkCredential(Login.Normalize(), Password.Normalize());
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.KeepAlive = true;
            return request;
        }

        public async Task<bool> DownloadFileAsync(string filename, string downloadfilename)
        {
            try
            {
                FtpWebRequest request = ConnectionForDownloadFile(HostUrl, filename);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                await Task.Run(() =>
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Credentials = new NetworkCredential(Login, Password);
                        client.DownloadFile(new Uri(HostUrl + filename), downloadfilename);
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                //return ex.ToString();
                return false;
            }
        }

        private async Task<bool> ReplaceFiles(string oldfilename, string newfilename)
        {
            bool oldfile = false;
            bool newfile = false;

            string[] allFoundFiles = Directory.GetFiles(path, oldfilename);
            foreach (string file in allFoundFiles)
            {
                oldfile = true;
            }

            string[] allFoundFiles1 = Directory.GetFiles(path, newfilename);
            foreach (string file in allFoundFiles1)
            {
                newfile = true;
            }

            if (oldfile == true && newfile == true)
            {
                splashScreen.SplashScreenText = $"Changing old files on new...";
                await Task.Delay(3000);

                try
                {
                    try
                    {
                        foreach (Process proc in Process.GetProcessesByName("Instagram Assistant"))
                        {
                            proc.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    if (File.Exists(path + oldfilename))
                    {
                        File.SetAttributes(path + oldfilename, FileAttributes.Normal);

                        File.Delete(path + oldfilename);
                    }else
                    {
                        splashScreen.SplashScreenText = $"Ooooooopppppsss.....";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    splashScreen.SplashScreenText = $"{ex.ToString()}";
                    return false;
                }

                await Task.Delay(3000);
                try
                {
                    System.IO.File.Move(path + newfilename, path + oldfilename);
                    splashScreen.SplashScreenText = $"Changing complete";
                    await Task.Delay(3000);
                }
                catch (Exception ex)
                {
                    splashScreen.SplashScreenText = $"{ex.ToString()}";
                    return false;
                }
                return true;
            }else
            {
                splashScreen.SplashScreenText = $"Oooops... Something went wronq...";
                return false;
            }

        }

        public string GetChangelog()
        {
            string notes;
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(Login, Password);
                notes = client.DownloadString(HostUrl + "releasenotes_ru.html");
            }
            return notes;
        }
    }
}
