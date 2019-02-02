using System.Collections.Generic;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News
{
    /// <summary>
    /// INewsComponent.
    /// </summary>
    public interface INewsComponent : IPageComponent
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Retrieves the news items.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<NewsItem> NewsItems();
    }
}