using System;
using Infotecs.Intern.RssReader.Models;

namespace Infotecs.Intern.RssReader.Services
{
    /// <summary>
    /// Управляет конфигурацией.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Получить конфигурацию.
        /// </summary>
        /// <returns>Конфигурация.</returns>
        RssReaderOptions GetSettings();

        /// <summary>
        /// Сохранить конфигурацию.
        /// </summary>
        /// <param name="options">Конфигурация.</param>
        void SaveSettings(RssReaderOptions options);
    }
}
