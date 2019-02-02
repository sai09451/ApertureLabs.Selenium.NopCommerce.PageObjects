using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// SearchPage.
    /// </summary>
    public interface ISearchPage : ISearchResultsTemplatePage
    {
        /// <summary>
        /// Searchs for a product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ISearchPage Search(SearchModel model);
    }
}