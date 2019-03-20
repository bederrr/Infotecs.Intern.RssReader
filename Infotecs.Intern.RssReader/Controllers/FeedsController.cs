using System;
using System.Threading.Tasks;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Controllers
{
    /// <summary>
    /// Контроллер основной страницы.
    /// </summary>
    public class FeedsController : Controller
    {
        private readonly IRssReader readerLogic;
        private readonly RssReaderOptions options;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="readerLogic">Логика.</param>
        /// <param name="options">Конфигурация.</param>
        public FeedsController(IRssReader readerLogic, IOptions<RssReaderOptions> options)
        {
            this.readerLogic = readerLogic;
            this.options = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.listItems = await readerLogic.ReadRssAsync();
            ViewBag.options = options;

            return View();
        }

    }
}