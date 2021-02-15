using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Instagram_Assistant.Helpers
{
    class ServerHelper
    {
        private readonly string Login = "";
        private readonly string Password = "";
        public readonly string HostUrl = "";

        public FtpWebRequest ConnectionForDownloadFile(string url, string file)
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
            catch (Exception e)
            {
                //return ex.ToString();
                return false;
            }
        }

        public async Task<XmlDocument> GetXmlFromServer(string filename)
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

        public string GetChangelogFromServer()
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
