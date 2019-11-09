using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Manga4U.Models;

using ParserLayer;

namespace Manga4U.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = ParserClass.MainParserFunction();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = $"Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
