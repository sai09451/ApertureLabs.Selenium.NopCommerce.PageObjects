using System;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// The 'Products tagged with X' page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public class ProductsByTagPage : SearchResultsTemplatePage, IProductsByTagPage
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsByTagPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public ProductsByTagPage(
            IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(
                  basePage,
                  pageObjectFactory,
                  driver,
                  pageSettings,
                  new UriTemplate("{tag}"))
        { }

        #endregion
    }
}