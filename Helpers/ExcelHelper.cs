using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Covid19Texas.Models;
using OfficeOpenXml;

namespace Covid19Texas.Helpers
{
    public class ExcelHelper
    {

        private static int Hours = 6;

        public static async Task<ExcelPackage> GetPackage(string file, string link)
        {

            if (!Directory.Exists(Constants.RootPath + "\\DataSheets"))
            {
                Directory.CreateDirectory(Constants.RootPath + "\\" + "DataSheets");
            }
            
            file = Constants.RootPath + "\\DataSheets\\" + file;

            // Check if Date Time is greater hours set
            TimeSpan time = DateTime.Now - ((File.Exists(file)) ? File.GetLastWriteTime(file) : DateTime.Now);

            if (!File.Exists(file) || (File.Exists(file) && time.TotalSeconds >= 60 * (60 * Hours)))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                await new WebClient().DownloadFileTaskAsync(new Uri(link), file);

                return new ExcelPackage(new FileInfo(file));

            }
            else
            {
                return new ExcelPackage(new FileInfo(file));
            }


        }
    }
}
