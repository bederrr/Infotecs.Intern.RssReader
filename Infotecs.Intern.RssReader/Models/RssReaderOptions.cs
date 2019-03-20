using System;
using System.Collections.Generic;

namespace Infotecs.Intern.RssReader.Models
{
    /// <summary>
    /// Модель параметров.
    /// </summary>
    public class RssReaderOptions
    {
        public List<string> Feeds { get; set; }
        public TimeSpan UpdateInterval { get; set; }
        public bool EnableFormatting { get; set; }
        public bool UseProxy { get; set; }
        public string ProxyHost { get; set; }
        public int ProxyPort { get; set; }
    }
}
