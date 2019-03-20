using System;
using Infotecs.Intern.RssReader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Controllers
{
    public class SettingsController : Controller
    {
        private readonly RssReaderOptions options;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Конфигурация.</param>
        public SettingsController(IOptions<RssReaderOptions> options)
        {
            this.options = options.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(options);
        }

        [HttpPost]
        public ActionResult Index(RssReaderOptions answeredOptions)
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
    }
}