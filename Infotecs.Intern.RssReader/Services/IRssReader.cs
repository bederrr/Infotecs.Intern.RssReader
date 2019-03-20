using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infotecs.Intern.RssReader.Services
{
    /// <summary>
    /// Логика.
    /// </summary>
    public interface IRssReader
    {
        /// <summary>
        /// Асинхронно читает Rss.
        /// </summary>
        /// <returns>Task-коллекция Rss новостей.</returns>
        Task<List<Models.RssFeed>> ReadRssAsync();
    }
}
