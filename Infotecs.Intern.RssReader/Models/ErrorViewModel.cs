using System;

namespace Infotecs.Intern.RssReader.Models
{
    /// <summary>
    /// Класс ошибки.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Id запроса.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Показ. Id запроса.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}