using System;

namespace Infotecs.Intern.RssReader.Models
{
    /// <summary>
    /// Класс ошибки.
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}