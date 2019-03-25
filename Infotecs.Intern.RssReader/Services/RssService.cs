using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.XPath;
using Infotecs.Intern.RssReader.Models;

namespace Infotecs.Intern.RssReader.Services
{
    /// <inheritdoc />
    public class RssService : IRssService
    {
        private readonly RssReaderOptions options;
        private readonly IHttpProxyClientService httpProxyClientService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="httpProxyClientService">Параметры proxy.</param>
        /// <param name="settingsService">Поставщик параметров.</param>
        public RssService(IHttpProxyClientService httpProxyClientService, ISettingsService settingsService)
        {
            options = settingsService.GetSettings();
            this.httpProxyClientService = httpProxyClientService;
        }

        public async Task<ConcurrentBag<RssFeed>> GetRssFeedsAsync()
        {
            var result = new ConcurrentBag<RssFeed>();
            List<Task> downloads = options.Feeds.ConvertAll(x => GetDownloadTaskAsync(x, result));
            await Task.WhenAll(downloads);

            return result;
        }

        private async Task GetDownloadTaskAsync(string url, ConcurrentBag<RssFeed> listBag)
        {
            var httpClient = httpProxyClientService.CreateHttpClient();

            using (var stream = await httpClient.GetStreamAsync(url))
            {
                XPathDocument doc = new XPathDocument(stream);
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator nodes = navigator.Select("//item");

                while (nodes.MoveNext())
                {
                    listBag.Add(GetRssFeed(nodes));
                }
            }
        }

        private RssFeed GetRssFeed(XPathNodeIterator nodes)
        {
            return new RssFeed
            {
                Title = nodes.Current.SelectSingleNode("title").Value,
                Description = nodes.Current.SelectSingleNode("description").Value,
                Link = nodes.Current.SelectSingleNode("link").Value,
                Guid = nodes.Current.SelectSingleNode("guid").Value,
                Category = nodes.Current.SelectSingleNode("category").Value,
                PubDate = DateTime.Parse(nodes.Current.SelectSingleNode("pubDate").Value)
            };
        }
    }
}
