using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Infotecs.Intern.RssReader.Models;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Services
{
    /// <inheritdoc />
    public class RssService : IRssService
    {
        private readonly RssReaderOptions options;
        private readonly List<RssFeed> listItems = new List<RssFeed>();
        private readonly IHttpProxyClientService httpProxyClientService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Параметры из appsettings.</param>
        /// <param name="httpProxyClientService">Параметры proxy.</param>
        public RssService(IOptions<RssReaderOptions> options, IHttpProxyClientService httpProxyClientService)
        {
            this.options = options.Value;
            this.httpProxyClientService = httpProxyClientService;
        }

        public async Task<List<RssFeed>> GetRssFeedsAsync()
        {
            IEnumerable<Task> downloadTasksQuery = 
                from url in options.Feeds select AddRssToListAsync(url);

            Task[] downloadTasks = downloadTasksQuery.ToArray();

            await Task.WhenAll(downloadTasks);

            return listItems;
        }

        private async Task AddRssToListAsync(string url)
        {
            var httpClient = httpProxyClientService.CreateHttpClient();

            using (var stream = await httpClient.GetStreamAsync(url))
            {
                XPathDocument doc = new XPathDocument(stream);
                XPathNavigator navigator = doc.CreateNavigator();
                XPathNodeIterator nodes = navigator.Select("//item");

                while (nodes.MoveNext())
                {
                    AddNext(nodes);
                }
            }
        }

        private void AddNext(XPathNodeIterator nodes)
        {
            listItems.Add(new RssFeed
            {
                Title = nodes.Current.SelectSingleNode("title").Value,
                Description = nodes.Current.SelectSingleNode("description").Value,
                Link = nodes.Current.SelectSingleNode("link").Value,
                Guid = nodes.Current.SelectSingleNode("guid").Value,
                Category = nodes.Current.SelectSingleNode("category").Value,
                PubDate = DateTime.Parse(nodes.Current.SelectSingleNode("pubDate").Value)
            });
        }
    }
}
