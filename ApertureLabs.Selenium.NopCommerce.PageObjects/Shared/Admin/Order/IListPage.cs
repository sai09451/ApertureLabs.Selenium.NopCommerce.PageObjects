using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Orders;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The order list page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    public interface IListPage : IBasePage
    {
        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        KGridComponent<IListPage> Orders { get; }

        /// <summary>
        /// Searches the specified order search model.
        /// </summary>
        /// <param name="orderSearchModel">The order search model.</param>
        /// <returns></returns>
        IListPage Search(OrderSearchModel orderSearchModel);

        /// <summary>
        /// Goes the directly to order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        IEditPage GoDirectlyToOrderNumber(int orderNumber);
    }
}
