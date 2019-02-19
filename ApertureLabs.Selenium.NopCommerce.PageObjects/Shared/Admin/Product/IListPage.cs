using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The product list page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    public interface IListPage : IBasePage
    {
        /// <summary>
        /// Gets the listed products.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ListPageProductRowComponent> GetListedProducts();

        /// <summary>
        /// Navigates to the create new product page.
        /// </summary>
        /// <returns></returns>
        ICreatePage AddNew();

        /// <summary>
        /// Locates the export format and begins the export.
        /// </summary>
        /// <param name="format">The format.</param>
        void ExportTo(string format);

        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="pathToFile">The path to file.</param>
        /// <returns></returns>
        IListPage Import(string pathToFile);

        /// <summary>
        /// Deletes the selected products.
        /// </summary>
        /// <returns></returns>
        IListPage DeleteSelected();
    }
}