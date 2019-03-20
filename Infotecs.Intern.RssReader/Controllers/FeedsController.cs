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
        private readonly IRssService serviceLogic;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="serviceLogic">Логика.</param>
        /// <param name="options">Конфигурация.</param>
        public FeedsController(IRssService serviceLogic, IOptions<RssReaderOptions> options)
        {
            this.serviceLogic = serviceLogic;
            this.options = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.listItems = await serviceLogic.GetRssFeedsAsync();
            ViewBag.updateInterval = options.UpdateInterval;
            ViewBag.enableFormatting = options.EnableFormatting;


            return View();
        }
    }
}
