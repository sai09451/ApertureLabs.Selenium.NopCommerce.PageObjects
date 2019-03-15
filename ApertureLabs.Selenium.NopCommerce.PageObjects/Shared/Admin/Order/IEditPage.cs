using ApertureLabs.Selenium.Components.Boostrap.Navs;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The order edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    public interface IEditPage : IBasePage
    {
        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        NavsTabComponent<IEditPage> Tabs { get; }

        /// <summary>
        /// Gets the information tab.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        OrderDetailsInfoComponent InfoTab { get; }

        /// <summary>
        /// Backs to order list.
        /// </summary>
        /// <returns></returns>
        IListPage BackToOrderList();

        /// <summary>
        /// Downloads an invoice of the order.
        /// </summary>
        void InvoicePdf();

        /// <summary>
        /// Deletes this order.
        /// </summary>
        /// <returns></returns>
        IListPage Delete();
    }
}
