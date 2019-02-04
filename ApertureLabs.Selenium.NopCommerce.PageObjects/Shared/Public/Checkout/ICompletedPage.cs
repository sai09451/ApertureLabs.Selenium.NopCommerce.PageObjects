using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// ICompletedPage.
    /// </summary>
    public interface ICompletedPage
    {
        /// <summary>
        /// Gets the order number.
        /// </summary>
        /// <returns></returns>
        int GetOrderNumber();

        /// <summary>
        /// Views the order details.
        /// </summary>
        /// <returns></returns>
        IOrderDetailsPage ViewOrderDetails();

        /// <summary>
        /// Continues to the home page.
        /// </summary>
        /// <returns></returns>
        IHomePage Continue();
    }
}
