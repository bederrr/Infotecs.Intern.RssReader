using System;
using System.Collections.Generic;

namespace Infotecs.Intern.RssReader.Models
{
    /// <summary>
    /// Модель параметров.
    /// </summary>
    public class RssReaderOptions
    {
        /// <summary>
        /// Коллекция Rss ссылок.
        /// </summary>
        public List<string> Feeds { get; set; }
        /// <summary>
        /// Интервал обновления.
        /// </summary>
        public TimeSpan UpdateInterval { get; set; }
        /// <summary>
        /// Включить форматирование.
        /// </summary>
        public bool EnableFormatting { get; set; }
        /// <summary>
        /// Включить прокси.
        /// </summary>
        public bool UseProxy { get; set; }
        /// <summary>
        /// Хост.
        /// </summary>
        public string ProxyHost { get; set; }
        /// <summary>
        /// Порт.
        /// </summary>
        public int ProxyPort { get; set; }
    }
}
