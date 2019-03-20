using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Infotecs.Intern.RssReader
{
    /// <summary>
    /// Стандартный класс.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа.
        /// </summary>
        /// <param name="args">Передаваемые аргументы.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
