using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using HtmlAgilityPack;

using DownloaderLayer;

namespace ParserLayer
{
    public class ParserClass
    {
        public static string MainParserFunction(string urlToParse = "https://www.mangapanda.com/one-piece", string startChapterNumber = "1", string endChapterNumber = "1")
        {
            string ChapterImageListInstring = "";
            // TODO: change the loop to the for each in an list format as some chapter can have *.5 at the end.
            for (float chapter = float.Parse(startChapterNumber); chapter <= float.Parse(endChapterNumber); chapter+=1)
            {
                string imageListInstring = "";
                // TODO: change the loop to the for each in an list format as to not take chanmces to miss any page and it will simplify code a bit, I guess?!??.
                for (int page = 1; page <= getTotalPageCountForChapter($"{urlToParse}/{chapter}"); page++)
                {
                    imageListInstring = getMangaPageURL(urlToParse, chapter.ToString(), page.ToString(), imageListInstring);
                }

                ChapterImageListInstring = string.Concat($"{ChapterImageListInstring}, \"{chapter}\": {{ {imageListInstring.Substring(1)} }}");
            }
            return $"{{ {ChapterImageListInstring.Substring(1)} }}";
        }

        public static List<string> getMangaChapterList(string url)
        {
            List<string> chapterNameList = (new HtmlWeb()).Load(url).DocumentNode
                .SelectNodes("//*[@id=\"listing\"]//a")
                .SelectMany(a => a.Attributes)
                .Where(b => (b.Value.Contains(url.Split("/").ElementAt(url.Split("/").Length - 1))))
                .Select(c => c.Value.Split("/")[c.Value.Split("/").Length - 1]).ToList<string>();
            if (chapterNameList.Count != 0) {
                return chapterNameList;
            }
            return new List<string> {"1"};
        }

        public static string getMangaPageURL(string urlToParse, string chapter, string page, string imageListInstring)
        {
            string url = $"{urlToParse}/{chapter}/{page}";
            string urlForPage = getElementByReleativeXPath(url, "//*[@id='img']").Attributes.AttributesWithName("src").Select(a => a.Value).ToList()[0].ToString();
            if (!string.IsNullOrEmpty(urlForPage))
            {
                DownloaderClass.MainDownloaderFunction(urlForPage, urlToParse.Split("/").ElementAt(urlToParse.Split("/").Length - 1), chapter, page);
                return string.Concat($"{imageListInstring}, \"{page.ToString()}\":  \"{urlForPage}\"");
            }
            return "";
        }

        public static int getTotalPageCountForChapter(string url)
        {
            HtmlNode pageCountElement = getElementByReleativeXPath(url, "//*[@id='selectpage']");
            if (!string.IsNullOrEmpty(pageCountElement.InnerText.Trim().Split(" ")[2]))
            {
                return int.Parse(pageCountElement.InnerText.Trim().Split(" ")[2]);
            }
            return 0;
        }

        public static HtmlNode getElementByReleativeXPath(string url, string xPath)
        {
            try
            {
                return (new HtmlWeb()).Load(url).DocumentNode.SelectSingleNode(xPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:\n {ex}\nEntering sleep mode for 1 min.");
                Thread.Sleep(60000);    //Sleep for a min
                return (new HtmlWeb()).Load(url).DocumentNode.SelectSingleNode(xPath);
            }
        }
    }
}
