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
            try
            {
                if (!config.Value.UseProxy)
                {
                    return new HttpClient();
                }

                var proxyHost = config.Value.ProxyHost;
                var proxyPort = config.Value.ProxyPort.ToString();

                var proxy = new WebProxy()
                {
                    Address = new Uri($"http://{proxyHost}:{proxyPort}"),
                    UseDefaultCredentials = true
                };

                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy,
                };

                var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        public HttpWebRequest CreateHttpWebRequest(string requestUri)
        {
            try
            {
                HttpWebRequest res = (HttpWebRequest)WebRequest.Create(requestUri);

                if (!config.Value.UseProxy)
                {
                    return res;
                }

                var proxyHost = config.Value.ProxyHost;
                var proxyPort = config.Value.ProxyPort;
                var proxy = new WebProxy(proxyHost, proxyPort);
                res.Proxy = proxy;

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }
    }
}
