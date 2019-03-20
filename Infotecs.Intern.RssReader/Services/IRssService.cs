using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infotecs.Intern.RssReader.Services
{
    /// <summary>
    /// Логика.
    /// </summary>
    public interface IRssService
    {
        /// <summary>
        /// Асинхронно получает Rss.
        /// </summary>
        /// <returns>Task-коллекция Rss новостей.</returns>
        Task<List<Models.RssFeed>> GetRssFeedsAsync();
    }
}
