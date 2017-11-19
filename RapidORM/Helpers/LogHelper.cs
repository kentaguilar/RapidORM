using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RapidORM.Helpers
{
    public static class LogHelper
    {
        public static void Log(string content)
        {
            string directory = @"logs\";
            string time = DateTime.Now.ToString("MMddyyyy");
            string path = directory + time + ".log";
            string contentToAppend = string.Format("\n[{0}] - {1}", DateTime.Now.ToString("HH:mm:ss"), content);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(path))
            {
                File.WriteAllText(path, contentToAppend);
            }
            else
            {
                File.AppendAllLines(path, new[] { contentToAppend });
            }
        }
    }
}
