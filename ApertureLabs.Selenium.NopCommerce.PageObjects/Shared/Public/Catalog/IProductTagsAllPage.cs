using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// The 'All Tags' page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public interface IProductTagsAllPage : ICatalogTemplatePage
    {
        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllTags();

        /// <summary>
        /// Selects the tag.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SelectTag(string tagName, StringComparison stringComparison = StringComparison.Ordinal);
    }
}