using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Home
{
    /// <summary>
    /// The home page of the Admin area.
    /// </summary>
    public interface IHomePage : IBasePage
    {
        /// <summary>
        /// The news widget.
        /// </summary>
        /// <value>
        /// The news.
        /// </value>
        INewsComponent News { get; }
    }
}