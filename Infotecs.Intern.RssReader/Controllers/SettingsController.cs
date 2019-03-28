using System;
using System.Collections.Generic;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// HttpGet для настроек.
        /// </summary>
        /// <returns>View.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View(settingsService.GetSettings());
        }

        /// <summary>
        /// HttpPost для настроек.
        /// </summary>
        /// <param name="answeredOptions">Измененные настройки.</param>
        /// <param name="feeds">Измененная строка ссылок.</param>
        /// <returns>Redirect на главную.</returns>
        [HttpPost]
        public ActionResult Index(RssReaderOptions answeredOptions, string feeds)
        {
            answeredOptions.Feeds = new List<string>(feeds.Split("\n"));

            if (settingsService.GetSettings() != answeredOptions)
            {
                if (ValidateOptions(answeredOptions))
                {
                    settingsService.SaveSettings(answeredOptions);
                }
                else
                {
                    ModelState.AddModelError("", "Неверный параметр.");
                }
            }

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