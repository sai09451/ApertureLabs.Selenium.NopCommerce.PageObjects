using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order
{
    /// <summary>
    /// ICustomersOrderPage.
    /// </summary>
    public interface ICustomerOrdersPage : IBasePage,
        IHasAccountNavigation<ICustomerOrdersPage>
    {
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerOrderRowComponent> GetOrders();
    }
}
