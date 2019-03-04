using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
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
        /// Gets the customers grid.
        /// </summary>
        /// <value>
        /// The customers grid.
        /// </value>
        KGridComponent<IListPage> CustomersGrid { get; }

        /// <summary>
        /// Navigates to the customer creation page.
        /// </summary>
        /// <returns></returns>
        ICreatePage AddNew();

        /// <summary>
        /// Locates and selects the format to export to.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="downloadsPath">The downloads path.</param>
        /// <param name="expectedFileName">The expected file name.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void ExportTo(string type,
            string downloadsPath,
            string expectedFileName,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Searches the using the search model.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        IListPage Search(CustomerSearchModel searchModel);

        /// <summary>
        /// Gets the listed customers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ListPageCustomerRowComponent> GetListedCustomers();
    }
}
