using System;
using System.Net;
using System.Net.Http;

namespace Infotecs.Intern.RssReader.Services
{
    /// <summary>
    /// Прокси.
    /// </summary>
    public interface IHttpProxyClientService
    {
        /// <summary>
        /// Создает новый http клиент.
        /// </summary>
        /// <returns></returns>
        HttpClient CreateHttpClient();
    }
}
