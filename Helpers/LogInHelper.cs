using Instagram_Assistant.ViewModel;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Classes.SessionHandlers;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram_Assistant.Helpers
{
    class LogInHelper
    {
        //STATUS: NOT OK - SEPARATE LOGIN AND LOGOUT 
        public static List<IInstaApi> LoggedInUsers = new List<IInstaApi>();
        private static List<string> sessions = new List<string>();

        public IInstaApi _instaApi { get; private set; }
        public static bool isSessionsLoaded { get; private set; } = false;

        public static string phone;
        public static string email;

        private IRequestDelay delay;
        private AndroidDevice device;
        private UserSessionData usersession;
        private string Timezone = "Europe/Moscow";

        public string path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private LogsPageViewModel logs = LogsPageViewModel.Instanse;
        private AccountInfoHelper accountInfo = new AccountInfoHelper();


        public LogInHelper()
        {
            delay = RequestDelay.FromSeconds(2, 2);
            device = new AndroidDevice
            {
                // Device name
                AndroidBoardName = "HONOR",
                // Device brand
                DeviceBrand = "HUAWEI",
                // Hardware manufacturer
                HardwareManufacturer = "HUAWEI",
                // Device model
                DeviceModel = "PRA-LA1",
                // Device model identifier
                DeviceModelIdentifier = "PRA-LA1",
                // Firmware brand
                FirmwareBrand = "HWPRA-H",
                // Hardware model
                HardwareModel = "hi6250",
                // Device guid
                DeviceGuid = new Guid("be897499-c663-492e-a125-f4c8d3785ebf"),
                // Phone guid
                PhoneGuid = new Guid("7b72321f-dd9a-425e-b3ee-d4aaf476ec52"),
                // Device id based on Device guid
                DeviceId = ApiRequestMessage.GenerateDeviceIdFromGuid(new Guid("be897499-c663-492e-a125-f4c8d3785ebf")),
                // Resolution
                Resolution = "1080x1812",
                // Dpi
                Dpi = "480dpi",
                // Device brand/android board name/device model : android version/hardware model/95414346:user/release-keys
                FirmwareFingerprint = "HUAWEI/HONOR/PRA-LA1:7.0/hi6250/95414346:user/release-keys",
                // don't change this
                AndroidBootloader = "4.23",
                // don't change this
                DeviceModelBoot = "qcom",
                // don't change this
                FirmwareTags = "release-keys",
                // don't change this
                FirmwareType = "user",
                
            };
            usersession = new UserSessionData
            {
                UserName = "",
                Password = ""
            };
        }

        public async Task<bool> LogedInCheck()
        {
            SplashScreen.SplashScreenViewModel splash = SplashScreen.SplashScreenViewModel.Instanse;

            splash.SplashScreenText = $"Checking account to login...";

            sessions.Clear();
            LoggedInUsers.Clear();
            bool session = PrevSessionExist();

            if (session == true)
            {
                foreach (var file in sessions)
                {
                    var api = BuildApi();
                 
                    var sessionHandler = new FileSessionHandler { FilePath = file, InstaApi = api };
                    api.SessionHandler = sessionHandler;
                    _instaApi = api;
                    api.SessionHandler.Load();

                    if (api.IsUserAuthenticated)
                        LoggedInUsers.Add(api);

                    isSessionsLoaded = true;
                }
                SaveSessions();
                return true;
            }
            else return false;

        }

        public IInstaApi PrevNamedSessionExist(string username)
        {
            var files = Directory.GetFiles(path, "*.bin");

            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    if (file.Contains(username))
                    {
                        var api = BuildApi();
                        var sessionHandler = new FileSessionHandler { FilePath = file, InstaApi = api };
                        api.SessionHandler = sessionHandler;
                        api.SessionHandler.Load();
                        return api;
                    }
                    else return null;
                }
            }
            else return null;

            return null;
        }

        public bool PrevSessionExist()
        {
            var files = Directory.GetFiles(path, "*.bin");
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    if (file.Contains("session"))
                        sessions.Add(file);
                }
                return true;
            }
            else return false;
        }


        public void SaveSessions()
        {
            var files = Directory.GetFiles(path, "*.bin");
            foreach (var instaApi in LoggedInUsers)
            {
                var state = instaApi.GetStateDataAsStream();
                var pathToSave = path +"\\" + instaApi.GetLoggedUser().UserName + "-session.bin";
                using (var fileStream = File.Create(pathToSave))
                {
                    state.Seek(0, SeekOrigin.Begin);
                    state.CopyTo(fileStream);
                }
            }

        }
        private void SaveSession()
        {
            var state = _instaApi.GetStateDataAsStream();
            using (var fileStream = File.Create(_instaApi.GetLoggedUser().UserName.ToLower() + "-session.bin"))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
            }

            if(!LoggedInUsers.Contains(_instaApi))
            {
                LoggedInUsers.Add(_instaApi);
                sessions.Add(_instaApi.GetLoggedUser().UserName);
            }
        }

        private IInstaApi BuildApi(string username = null, string password = null)
        {
            var fakeUserData = UserSessionData.ForUsername(username ?? "FAKEUSER").WithPassword(password ?? "FAKEPASS");
            var instapi =  InstaApiBuilder.CreateBuilder()
                             .SetUser(fakeUserData)
                             .UseLogger(new DebugLogger(LogLevel.All))
                             .SetRequestDelay(delay)
                             .Build();
            instapi.SetDevice(device);
            instapi.SetTimezone(Timezone);
            return instapi;
        }

        public async Task<string> Login(string username, string password)
        {
            _instaApi = null;
            usersession = new UserSessionData
            {
                UserName = username,
                Password = password
            };

            _instaApi = PrevNamedSessionExist(username);

            if (_instaApi == null)
            {
                _instaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(usersession)
                    .UseLogger(new DebugLogger(LogLevel.All))
                    .SetRequestDelay(delay)
                    .Build();
                _instaApi.SetDevice(device);
                _instaApi.SetTimezone(Timezone);
            }

            if (!_instaApi.IsUserAuthenticated)
            {
                var logInResult = await _instaApi.LoginAsync();

                logs.Add($"{ logInResult.Value}", MessageType.Type.DEBUGINFO, this.GetType().Name);

                if (logInResult.Succeeded)
                {
                    SaveSession();
                    logs.Add($"You`ve been logIn, {username}", MessageType.Type.DEBUGINFO, this.GetType().Name);
                    await accountInfo.UpdateInfo(_instaApi);
                    return logInResult.Value.ToString();
                }
                else
                {
                    if (logInResult.Value == InstaLoginResult.ChallengeRequired)
                    {
                        var twoFactorLogin = await _instaApi.GetChallengeRequireVerifyMethodAsync();

                        phone = twoFactorLogin.Value.StepData.PhoneNumber ?? "no phone";
                        email = twoFactorLogin.Value.StepData.Email ?? "no email";
                        return logInResult.Value.ToString();
                    }
                    return logInResult.Value.ToString();
                }
            }
            else if(_instaApi.IsUserAuthenticated ==true)
                return "Success";
            else
                return "Error!";
        }

        public void Login(string username)
        {
            LoginPageViewModel loginPage = LoginPageViewModel.Instanse;
            MainWindowViewModel.instance.SelectedViewModel = loginPage;
            MainWindowViewModel.instance.MainSelectedViewModel = null;
            loginPage.LoginVisibility = Visibility.Visible;
            loginPage.ChallengesVisibility = Visibility.Hidden;
            loginPage.CodeCheckVisibility = Visibility.Hidden;
            loginPage.LoginGridIsEnable = true;
            loginPage.ChallengesGridIsEnabel = true;
            loginPage.CodeCheckGridIsEnabel = true;
            LoginPageViewModel.Instanse.Login = username;
            loginPage.CancleLoginVisibility = Visibility.Visible;

        }

        public bool LogOut(string username)
        {
            var files = Directory.GetFiles(path, "*.bin");
            foreach(var file in files)
            {
                if (file.Contains(username))
                {
                    int index = sessions.FindIndex(x => x.Contains(username));
                    LoggedInUsers.RemoveAt(index);
                    sessions.RemoveAt(index);
                    accountInfo.DeleteAccount(username);
                    File.Delete(file);
                    return true;
                }
                else return false;
            }
            return false;
        }

        public async Task<bool> CodeCheck(string code)
        {
            var verifyLogin = await _instaApi.VerifyCodeForChallengeRequireAsync(code);

            if (verifyLogin.Succeeded)
            {
                SaveSession();
                await accountInfo.GetInfo();
                return true;
            }
            else 
                return false;
            
        }

        public async Task<bool> SendCode(bool ischecked)
        {
            var challenge = await _instaApi.GetChallengeRequireVerifyMethodAsync();

            if (challenge.Succeeded && challenge.Value.StepData != null)
            {
                if (ischecked == true && phone != "no phone")
                {
                    var result = await _instaApi.RequestVerifyCodeToSMSForChallengeRequireAsync();
                    return result.Succeeded;
                }
                else if ((ischecked == false && email != "no email"))
                {
                    var result = await _instaApi.RequestVerifyCodeToEmailForChallengeRequireAsync();
                    return result.Succeeded;
                }
            }
            else
                return false;

            return false;
        }

    }
   
}
