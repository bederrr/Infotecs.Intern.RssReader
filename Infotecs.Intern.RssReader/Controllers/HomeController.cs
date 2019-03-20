using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssReader readerLogic;
        private readonly RssReaderOptions options;

        /// <summary>
        /// Главная страница.
        /// </summary>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.listItems = await readerLogic.ReadRssAsync();
            ViewBag.options = options;

            return View();
        }

        [HttpGet]
        public IActionResult Settings()
        {
            return View(options);
        }

        [HttpPost]
        public ActionResult Settings(RssReaderOptions answeredOptions)
        {
            // do validation
            if (options != answeredOptions)
            {
                if (/*answeredOptions.IsValid()*/ true)
                {
                    // save

                }
            }
            // redirect
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static void SaveSettingsChanges(RssReaderOptions options)
        {

        }
    }
}
