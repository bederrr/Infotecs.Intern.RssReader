using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infotecs.Intern.RssReader
{
    /// <summary>
    /// Первично запускаемый класс приложения net core.
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Параметры конфигурации приложения.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Метод для добавления сервисов в контейнер.
        /// </summary>
        /// <param name="services">Контейнер коллекции сервисов.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRssService, RssService>();
            services.AddTransient<IHttpProxyClientService, HttpProxyClientService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<RssReaderOptions>(Configuration.GetSection("RssReaderOptions"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// Метод вызывается во время выполнения. Используйте этот метод для настройки конвейера HTTP-запроса.
        /// </summary>
        /// <param name="app">Поставщик конфигурации.</param>
        /// <param name="env">Поставщик информации веб-хостинга.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Feeds}/{action=Index}/{id?}");
            });
        }
    }
}
