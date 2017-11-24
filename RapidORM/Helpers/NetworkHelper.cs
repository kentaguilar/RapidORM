using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace RapidORM.Helpers
{
    public class NetworkHelper
    {
        /// <summary>
        /// Gets public ip of app
        /// </summary>
        /// <param name=""></param>
        public static string GetPublicIp()
        {
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }

            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");
            direction = direction.Substring(first, last - first);

            return direction;
        }

        /// <summary>
        /// Uploads file to FTP
        /// </summary>
        /// <param name="sourceLocalLocation"></param>
        /// <param name="destinationServerLocation"></param>
        public static bool UploadFileToFtp(string sourceLocalLocation, string destinationServerLocation, string ftpUsername, string ftpPassword)
        {
            bool result = false;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    client.UploadFile(destinationServerLocation, "STOR", sourceLocalLocation);
                }

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Download file from FTP Server
        /// </summary>
        /// <param name="sourceServerLocation"></param>
        /// <param name="destinationLocalLocation"></param>
        /// <param name="ftpUsername"></param>
        /// <param name="ftppassword"></param>
        public static bool DownloadFileFromFtp(string sourceServerLocation, string destinationLocalLocation, string ftpUsername, string ftpPassword)
        {
            bool result = false;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(sourceServerLocation));
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.Proxy = null;
                request.UsePassive = false;
                request.UseBinary = true;
                request.KeepAlive = false;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                FileStream file = File.Create(destinationLocalLocation);
                byte[] buffer = new byte[32 * 1024];
                int read;

                while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    file.Write(buffer, 0, read);
                }

                file.Close();
                responseStream.Close();
                response.Close();

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
