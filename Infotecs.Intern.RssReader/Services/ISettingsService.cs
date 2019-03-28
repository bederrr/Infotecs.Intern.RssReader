using System;
using Infotecs.Intern.RssReader.Models;

namespace Infotecs.Intern.RssReader.Services
{
    /// <summary>
    /// Управляет конфигурацией.
    /// </summary>
    public interface ISettingsService : IComparable
    {
        /// <summary>
        /// Метод сравнения параметров.
        /// </summary>
        /// <param name="options">Входящие параметры для сравнения.</param>
        bool CompareTo(RssReaderOptions options);

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
