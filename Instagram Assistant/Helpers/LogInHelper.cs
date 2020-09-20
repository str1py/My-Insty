using Instagram_Assistant.ViewModel;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    class LogInHelper
    {
        public static IInstaApi _instaApi;
        public static string phone;
        public static string email;

        private IRequestDelay delay;
        private AndroidDevice device;
        private UserSessionData usersession;


        public string path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private string stateFile = "state.bin";
        private string stateFilelocal = "state.bin";
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
                FirmwareType = "user"
            };
            usersession = new UserSessionData
            {
                UserName = "",
                Password = ""
            };
        }

        public bool LogedInCheck()
        {
            bool session = PrevSessionExist();

            //SetUp Login
            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(usersession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .SetRequestDelay(delay)
                .Build();
            _instaApi.SetDevice(device);

            if (session == true)
            {
                Console.WriteLine("Loading state from file");
                using (var fs = File.OpenRead(stateFilelocal))
                {
                    _instaApi.LoadStateDataFromStream(fs);
                }
                return true;
            }
            else
                return false;
        }

        public async Task<string> Login(string username, string password)
        {

            usersession = new UserSessionData
            {
                UserName = username,
                Password = password
            };

            //SetUp Login
            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(usersession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .SetRequestDelay(delay)
                .Build();
            _instaApi.SetDevice(device);

            if (!_instaApi.IsUserAuthenticated)
            {
                //   var asd = await _instaApi.SendRequestsBeforeLoginAsync();
                await Task.Delay(5000);
                var logInResult = await _instaApi.LoginAsync();

                logs.Add($"{ logInResult.Value}", MessageType.Type.DEBUGINFO);

                if (logInResult.Succeeded)
                {
                    var state = _instaApi.GetStateDataAsStream();
                    using (var fileStream = File.Create(stateFile))
                    {
                        state.Seek(0, SeekOrigin.Begin);
                        state.CopyTo(fileStream);
                    }

                    logs.Add($"You`ve been logIn, {username}", MessageType.Type.DEBUGINFO);
                    await accountInfo.GetInfo();
                    return logInResult.Value.ToString();
                }
                else
                {
                    if (logInResult.Value == InstaLoginResult.ChallengeRequired)
                    {
                        var challenge = await _instaApi.GetChallengeRequireVerifyMethodAsync();

                        phone = challenge.Value.StepData.PhoneNumber ?? "no phone";
                        email = challenge.Value.StepData.Email ?? "no email";
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

        public bool PrevSessionExist()
        {
            string a = path + "\\state.bin";
            stateFilelocal = a.Replace("\\", "/");
            try
            {
                // load session file if exists
                if (File.Exists(stateFilelocal))
                    return true;
                else
                    return false;
                
            }
            catch (Exception e) { Console.WriteLine(e); }
            return false;
        }

        public bool LogOut()
        {


            string a = path + "\\state.bin";
            stateFilelocal = a.Replace("\\", "/");
            try
            {
                // load session file if exists
                if (File.Exists(stateFilelocal))
                {
                    File.Delete(stateFilelocal);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e) { Console.WriteLine(e); }
            return false;
        }


        public async Task<bool> CodeCheck(string code)
        {
            var verifyLogin = await _instaApi.VerifyCodeForChallengeRequireAsync(code);

            if (verifyLogin.Succeeded)
            {
                var state = _instaApi.GetStateDataAsStream();
                using (var fileStream = File.Create(stateFile))
                {
                    state.Seek(0, SeekOrigin.Begin);
                    state.CopyTo(fileStream);
                }
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
