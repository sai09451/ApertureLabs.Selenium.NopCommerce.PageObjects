using System.Collections.Generic;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home
{
    /// <summary>
    /// HomePage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public interface IHomePage : IBasePage
    {
        /// <summary>
        /// Gets the featured products.
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<IWebElement> GetFeaturedProducts();

        /// <summary>
        /// Gets the news.
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<IWebElement> GetNews();

        /// <summary>
        /// Gets the top menu categories.
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<IWebElement> GetTopMenuCategories();
    }
}