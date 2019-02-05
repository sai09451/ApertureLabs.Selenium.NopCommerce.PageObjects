using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// IParentCategoryPage.
    /// </summary>
    public interface IParentCategoryPage : ICatalogTemplatePage
    {
        /// <summary>
        /// Gets the sub categories.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSubCategories();
    }
}
