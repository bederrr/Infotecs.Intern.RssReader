using System;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Controllers
{
    public class SettingsController : Controller
    {
        private readonly RssReaderOptions options;
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="settingsService">Поставщик настроек.</param>
        public SettingsController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
            options = this.settingsService.GetSettings();
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Feeds = string.Join("\n", options.Feeds);

            //return View();
            return View(options);
        }

        [HttpPost]
        public ActionResult Index(RssReaderOptions answeredOptions)
        {
            // do validation
            if (!options.Equals(answeredOptions) && ValidateOptions(answeredOptions))
            {           
                //парс строки
                //save
                settingsService.SaveSettings(answeredOptions);
            }
            else
            {
                ModelState.AddModelError("", "Неверный параметр.");
            }
            //return
            return Redirect("/");
        }

        private bool ValidateOptions(RssReaderOptions options)
        {
            foreach (var item in options.Feeds)
            {
                if (!Uri.TryCreate(item, UriKind.Absolute, out var a))
                {
                    return false;
                }
            }
            return true;
        }
    }
}