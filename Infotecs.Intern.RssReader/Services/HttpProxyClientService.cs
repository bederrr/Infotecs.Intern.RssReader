using System;
using System.Net;
using System.Net.Http;
using Infotecs.Intern.RssReader.Models;
using Microsoft.Extensions.Options;

namespace Infotecs.Intern.RssReader.Services
{
    /// <inheritdoc />
    public class HttpProxyClientService : IHttpProxyClientService
    {
        private readonly IOptions<RssReaderOptions> config;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="config">Конфигурация из appconfig.</param>
        public HttpProxyClientService(IOptions<RssReaderOptions> config)
        {
            this.config = config;
        }

        public HttpClient CreateHttpClient()
        {
                if (!config.Value.UseProxy)
                {
                    return new HttpClient();
                }

                var proxy = new WebProxy()
                {
                    Address = new Uri($"http://{config.Value.ProxyHost}:{config.Value.ProxyPort.ToString()}"),
                    UseDefaultCredentials = true
                };

                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy,
                };

                var client = new HttpClient(httpClientHandler,true);
                return client;
        }

        public HttpWebRequest CreateHttpWebRequest(string requestUri)
        {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

                if (!config.Value.UseProxy)
                {
                    return request;
                }

                request.Proxy = new WebProxy(config.Value.ProxyHost, config.Value.ProxyPort);

                return request;
        }
    }
}
