using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Customers;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// The admin customer list page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    public interface IListPage : IBasePage
    {
        /// <summary>
        /// Navigates to the customer creation page.
        /// </summary>
        /// <returns></returns>
        ICreatePage AddNew();

        /// <summary>
        /// Locates and selects the format to export to.
        /// </summary>
        /// <param name="type">The type.</param>
        void ExportTo(string type);

        /// <summary>
        /// Searches the using the search model.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        IListPage Search(CustomerSearchModel searchModel);

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ListPageCustomerRowComponent> GetCustomers();
    }
}
