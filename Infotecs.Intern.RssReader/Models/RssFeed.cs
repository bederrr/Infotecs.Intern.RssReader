using System;

namespace Infotecs.Intern.RssReader.Models
{
    /// <summary>
    /// Модель Rss новости.
    /// </summary>
    public class RssFeed
    {
        /// <summary>
        /// Категория.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Директива.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Ссылка.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Дата публикации.
        /// </summary>
        public DateTime PubDate { get; set; }

        /// <summary>
        /// Заголовок.
        /// </summary>
        public string Title { get; set; }
    }
}
