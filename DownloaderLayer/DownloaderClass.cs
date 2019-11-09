using System.IO;
using System.Net;
using System.Globalization;

namespace DownloaderLayer
{
    public class DownloaderClass
    {
        public static bool MainDownloaderFunction(string url, string mangaName, string chapterNumber, string pageNumber)
        {
            WebClient webClient = new WebClient();

            string directoryName = $"D:\\Manga\\{new CultureInfo("en-us", false).TextInfo.ToTitleCase(mangaName.Replace("-", " "))}\\{chapterNumber}";
            Directory.CreateDirectory(directoryName);
            webClient.DownloadFile(url, $"{directoryName}\\{pageNumber}.{url.Split(".")[url.Split(".").Length - 1]}");
            return true;
        }
    }
}
