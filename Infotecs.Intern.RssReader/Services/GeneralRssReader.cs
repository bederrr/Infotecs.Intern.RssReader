using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.XPath;
using Infotecs.Intern.RssReader.Models;
using Infotecs.Intern.RssReader.Services;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Services
{
    /// <inheritdoc />
    public class GeneralRssReader : IRssReader
    {
        private readonly RssReaderOptions options;
        private readonly List<RssFeed> listItems = new List<RssFeed>();
        private readonly IHttpProxyClientService httpProxyClientService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Параметры из appsettings.</param>
        /// <param name="httpProxyClientService">Параметры proxy.</param>
        public GeneralRssReader(IOptions<RssReaderOptions> options, IHttpProxyClientService httpProxyClientService)
        {
            this.options = options.Value;
            this.httpProxyClientService = httpProxyClientService;
        }

        public async Task<List<RssFeed>> ReadRssAsync()
        {
            foreach (var url in options.Feeds)
            {
                await AddRssToListAsync(url);
            }
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
                    XPathNavigator node = nodes.Current;
                    listItems.Add(new RssFeed
                    {
                        Title = node.SelectSingleNode("title").Value,
                        Description = node.SelectSingleNode("description").Value,
                        Link = node.SelectSingleNode("link").Value,
                        Guid = node.SelectSingleNode("guid").Value,
                        Category = node.SelectSingleNode("category").Value,
                        PubDate = DateTime.Parse(node.SelectSingleNode("pubDate").Value)
                    });
                }
            }
        }
    }
}
