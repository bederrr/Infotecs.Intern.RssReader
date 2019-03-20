using System;

namespace Infotecs.Intern.RssReader.Models
{
    /// <summary>
    /// Модель Rss новости.
    /// </summary>
    public class RssFeed
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }
        public string Title { get; set; }
    }
}
