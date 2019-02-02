using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Corresponds to the ManufacturerTemplate.ProductsInGridOrLines.cshtml
    /// page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.SearchResultsTemplatePage" />
    public class ProductsByManufacturerPage : SearchResultsTemplatePage,
        IProductsByManufacturerPage
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsByManufacturerPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public ProductsByManufacturerPage(
            IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(basePage,
                  pageObjectFactory,
                  driver,
                  pageSettings)
        {
            Uri = new System.Uri(pageSettings.BaseUrl);
        }

        #endregion
    }
}
