using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// The page that lists all manufacturers.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public interface IManufacturerAllPage : ICatalogTemplatePage
    {
        /// <summary>
        /// Selects the manufacturer.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        ProductsByManufacturerPage SelectManufacturer(string manufacturer, StringComparison stringComparison = StringComparison.Ordinal);
    }
}