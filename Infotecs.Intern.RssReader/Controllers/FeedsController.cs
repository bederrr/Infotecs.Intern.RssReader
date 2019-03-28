using System;
using System.Threading.Tasks;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.AspNetCore.Mvc;

namespace Infotecs.Intern.RssReader.Controllers
{
    /// <inheritdoc/>
    public class FeedsController : Controller
    {
        private readonly ISettingsService settingsService;
        private readonly IRssService rssService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="rssService">Логика.</param>
        /// <param name="settingsService">Поставщик конфигураций.</param>
        public FeedsController(IRssService rssService, ISettingsService settingsService)
        {
            this.rssService = rssService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Индекс контроллер.
        /// </summary>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.listItems = await rssService.GetRssFeedsAsync();
            ViewBag.updateInterval = settingsService.GetSettings().UpdateInterval.TotalSeconds;
            ViewBag.enableFormatting = settingsService.GetSettings().EnableFormatting;

            return View();
        }
    }
}
