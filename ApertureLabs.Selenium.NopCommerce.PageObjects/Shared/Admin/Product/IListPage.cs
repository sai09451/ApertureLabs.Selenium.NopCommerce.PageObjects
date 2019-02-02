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
        IEnumerable<object> GetListedProducts();
    }
}