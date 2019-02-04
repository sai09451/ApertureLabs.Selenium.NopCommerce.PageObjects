using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order
{
    /// <summary>
    /// ICustomersOrderPage.
    /// </summary>
    public interface ICustomerOrdersPage : IBasePage
    {
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetOrders();
    }
}
