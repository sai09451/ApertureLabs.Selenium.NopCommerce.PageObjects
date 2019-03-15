using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.Components.Boostrap.Navs;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// Corresponds to the "Admin/Views/Product/Edit.cshtml" page.
    /// </summary>
    public interface IEditPage : IBasePage, IHasAdvancedOptionsPage
    {
        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        NavsTabComponent<IEditPage> Tabs { get; }

        /// <summary>
        /// Back to product list.
        /// </summary>
        /// <returns></returns>
        IListPage BackToProductList();

        /// <summary>
        /// Copies the product.
        /// </summary>
        /// <returns></returns>
        PageComponent CopyProduct();

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns></returns>
        IListPage Delete();

        /// <summary>
        /// Gets the name of the active tab.
        /// </summary>
        /// <returns></returns>
        string GetActiveTabName();

        /// <summary>
        /// Gets the tab names.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetTabNames();

        /// <summary>
        /// Goes to tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void GoToTab(string tabName, StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Previews the specified switch to new window.
        /// </summary>
        /// <param name="switchToNewWindow">if set to <c>true</c> [switch to new window].</param>
        /// <returns></returns>
        string Preview(bool switchToNewWindow = true);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        IListPage Save();

        /// <summary>
        /// Saves and continue edit.
        /// </summary>
        /// <returns></returns>
        IEditPage SaveAndContinueEdit();
    }
}