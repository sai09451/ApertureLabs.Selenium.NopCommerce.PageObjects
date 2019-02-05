using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart
{
    /// <summary>
    /// CartPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IPageObject" />
    public interface ICartPage : IBasePage
    {
        /// <summary>
        /// Gets the order summary.
        /// </summary>
        /// <value>
        /// The order summary.
        /// </value>
        OrderSummaryComponent OrderSummary { get; }
    }
}