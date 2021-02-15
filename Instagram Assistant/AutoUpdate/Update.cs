using Instagram_Assistant.Helpers;
using Instagram_Assistant.SplashScreen;
using Instagram_Assistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace Instagram_Assistant.AutoUpdate
{

    public class Update
    {
        private readonly static string path = Directory.GetCurrentDirectory() + "\\";
        private readonly string fileWithHashes = "FilesVersions.xml";
        private readonly string fileWithVersion = "version.xml";

        private Dictionary<string, Uri> filesinfo = new Dictionary<string, Uri>();
        public static string NewVersion { get; set; }

        private Md5HashHelper md5helper = new Md5HashHelper();
        private LogsPageViewModel logs = new LogsPageViewModel();
        private SplashScreenViewModel splashScreen = SplashScreenViewModel.Instanse;
        private ServerHelper server = new ServerHelper();

        //WORKING
        public void GetFilesHashesLocal()
        {
            XDocument xDoc = XDocument.Load(path + fileWithHashes);
            FilesToGetInfo();

            foreach (KeyValuePair<string, Uri> pair in filesinfo)
            {
                {
                    var info = md5helper.GetMd5Hash(path + pair.Key);
                    var result = xDoc.Descendants().FirstOrDefault(e => e.Name.LocalName.Equals(pair.Key)) != null;
                    if (result)
                        xDoc.Root.Element(pair.Key).Value = info;
                    else
                    {
                        XElement element = new XElement(pair.Key, info);;
                        xDoc.Root.Add(element);
                    }
                }
            }

            var updinfo = md5helper.GetMd5Hash(path + "Update.exe");
            xDoc.Root.Element("Update.exe").Value = updinfo;

            var appdinfo = md5helper.GetMd5Hash(path + "Instagram Assistant.exe");
            xDoc.Root.Element("InstagramAssistant.exe").Value = appdinfo;

            xDoc.Save(path + fileWithHashes);
        }
        public async Task<bool> IsFilesExists()
        {
            if (File.Exists(path + fileWithHashes))
            {
                XDocument xDoc = XDocument.Load(path + fileWithHashes);
                FilesToGetInfo();

                foreach (KeyValuePair<string, Uri> pair in filesinfo)
                {
                    if (!File.Exists(path + pair.Key))
                    {
                        await Task.Delay(2000);
                        await App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            splashScreen.SplashScreenText = $"Cant find {pair.Key}. Download missing file...";
                        }));

                        await Task.Delay(2000);
                        await server.DownloadFileAsync(pair.Key, pair.Key);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                await server.DownloadFileAsync(fileWithHashes, fileWithHashes);
                return false;
            }
        }
        public async Task<bool> CheckForNewFiles()
        {
            XmlDocument serverv = await server.GetXmlFromServer(fileWithHashes);
            try
            {
                var root = serverv.DocumentElement;
                foreach (XmlElement attr in root)
                {
                    if (!File.Exists(path + attr.Name))
                    {
                        if (!attr.Name.ToString().Contains(".exe"))
                        { 
                            if (await server.DownloadFileAsync(attr.Name,attr.Name))
                            {
                                XDocument xDoc = XDocument.Load(path + fileWithHashes);
                                XmlWriter writer = xDoc.Root.CreateWriter();
                                writer.WriteElementString(attr.Name, md5helper.GetMd5Hash(path + attr.Name));
                                writer.Close();
                                xDoc.Save(path + fileWithHashes);
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


        //COMPARE
        public async Task<bool> CompareFilesHash()
        {
            XmlDocument localv = new XmlDocument();
            localv.Load(path + fileWithHashes);

            XmlDocument serverv = await server.GetXmlFromServer(fileWithHashes);

            try
            {
                foreach (KeyValuePair<string, Uri> pair in filesinfo)
                {
                    var hashFromServer = serverv.GetElementsByTagName(pair.Key)[0]?.InnerText;
                    var hashFromLocal = localv.GetElementsByTagName(pair.Key)[0]?.InnerText;
                    if (hashFromLocal != hashFromServer && hashFromServer != null && hashFromLocal != null)
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
        public async Task<bool> CompareProgrammVersions()
        {
            Version myVersion;
            try
            {
                var Version = FileVersionInfo.GetVersionInfo(path + "Instagram Assistant.exe");
                myVersion = new Version(Version.FileVersion);
            }
            catch (Exception e)
            {
                myVersion = null;
                MessageBox.Show(e.Message);
            }


            XmlDocument doc = await server.GetXmlFromServer(fileWithVersion);
            var versionFromServer = new Version(doc.GetElementsByTagName("version")[0].InnerText);

            Properties.Settings.Default.Save();

            NewVersion = versionFromServer.ToString();

            if (myVersion < versionFromServer)
                return true;
            else
                return false;

        }
        public async Task<bool> RepairFiles()
        {
            XmlDocument localv = new XmlDocument();
            localv.Load(path + fileWithHashes);

            XmlDocument serverv = await server.GetXmlFromServer(fileWithHashes);

            try
            {
                foreach (KeyValuePair<string, Uri> pair in filesinfo)
                {
                    var hashFromServer = serverv.GetElementsByTagName(pair.Key)[0].InnerText;
                    var hashFromLocal = localv.GetElementsByTagName(pair.Key)[0].InnerText;
                    if (hashFromLocal != hashFromServer)
                       await server.DownloadFileAsync(pair.Key, pair.Key);
                }
                return true;
            }
            catch (Exception ex)
            {
                logs.Add($"{ex}", MessageType.Type.ERROR, this.GetType().Name);
                return false;
            }
        }
        void FilesToGetInfo()
        {
            filesinfo?.Clear();
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.dll"); //Getting Text files

            foreach (FileInfo file in Files)
                filesinfo.Add(file.Name, new Uri(server.HostUrl + file.Name));
        }


        //UPDATE
        public async void BeginUpdate()
        {
            string path1 = path + "Update.exe";

            Process.Start(path1);

            await Task.Delay(3000);
            App.Current.Shutdown();
        }
        public async Task UpdateUpdater()
        {
            string updaterpath = path + "Update.exe";
            if (File.Exists(updaterpath) && new FileInfo(updaterpath).Length != 0)
            {
                var myVersion = FileVersionInfo.GetVersionInfo(updaterpath);
                Version Version = new Version(myVersion.FileVersion);

                XmlDocument doc = await server.GetXmlFromServer(fileWithVersion);
                var versionFromServer = new Version(doc.GetElementsByTagName("updaterversion")[0].InnerText);

                if (Version < versionFromServer)
                {
                    await server.DownloadFileAsync("Update.exe", "Update.update");
                    //TODO: ПРОВЕРКА
                    await ReplaceFiles("Update.exe", "Update.update");
                    //TODO: ПРОВЕРКА
                }
            }
            else
                await server.DownloadFileAsync("Update.exe", "Update.exe");
        }
        public async Task UpdateAssistant()
        {
            var myVersion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;

            XmlDocument doc = await server.GetXmlFromServer(fileWithVersion);
            var versionFromServer = new Version(doc.GetElementsByTagName("version")[0].InnerText);

            if (myVersion < versionFromServer)
            {
                await server.DownloadFileAsync("Instagram Assistant.exe", "Instagram Assistant.update");
                await ReplaceFiles("Instagram Assistant.exe", "Instagram Assistant.update");
             //TODO: ПРОВЕРКА
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
                    }
                    else
                    {
                        splashScreen.SplashScreenText = $"Ooooooopppppsss.....";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    splashScreen.SplashScreenText = $"{ex}";
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
                    splashScreen.SplashScreenText = $"{ex}";
                    return false;
                }
                return true;
            }
            else
            {
                splashScreen.SplashScreenText = $"Oooops... Something went wronq...";
                return false;
            }

        }
    
    }
}
