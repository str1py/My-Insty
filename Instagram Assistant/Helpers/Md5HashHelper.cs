using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Instagram_Assistant.Helpers
{
    class Md5HashHelper
    {
        public string GetMd5Hash(string source)
        {
            FileStream filestream;
            using (filestream = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Read))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(filestream);


                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public bool VerifyMd5Hash(string localfile, string serverfile)
        {

            string localhash = GetMd5Hash(localfile);
            string serverhash = GetMd5Hash(serverfile);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(localhash, serverhash))    
                return true;      
            else          
                return false;
            

        }
    }
}
