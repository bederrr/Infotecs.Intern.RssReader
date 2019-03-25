using System;
using System.Collections.Generic;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="settingsService">Поставщик настроек.</param>
        public SettingsController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.feeds = string.Join("\n", settingsService.GetSettings().Feeds);

            //return View();
            return View(settingsService.GetSettings());
        }

        [HttpPost]
        public ActionResult Index(RssReaderOptions answeredOptions, string feeds)
        {
            answeredOptions.Feeds = new List<string>(feeds.Split("\n"));
            // do validation
            if (!settingsService.GetSettings().Equals(answeredOptions) && ValidateOptions(answeredOptions))
            {           
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
            if (options.EnableFormatting.Equals(null) ||
                options.UpdateInterval.Equals(null) ||
                options.UseProxy.Equals(null))
            {
                return false;
            }

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