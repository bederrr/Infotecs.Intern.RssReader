using System;
using System.Threading.Tasks;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Controllers
{
    /// <inheritdoc/>
    public class FeedsController : Controller
    {
        private readonly RssReaderOptions options;
        private readonly IRssService rssService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="rssService">Логика.</param>
        /// <param name="settingsService">Поставщик конфигураций.</param>
        public FeedsController(IRssService rssService, ISettingsService settingsService)
        {
            this.rssService = rssService;
            options = settingsService.GetSettings();
        }

        /// <summary>
        /// Индекс.
        /// </summary>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.listItems = await rssService.GetRssFeedsAsync();
            ViewBag.updateInterval = options.UpdateInterval;
            ViewBag.enableFormatting = options.EnableFormatting;

            return View();
        }
    }
}
