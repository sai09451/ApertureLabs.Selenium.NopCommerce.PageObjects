using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Corresponds to the view CategoryTemplate.ProductsInGridOrLines.cshtml.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.SearchResultsTemplatePage" />
    public class ProductsByCategoryPage : SearchResultsTemplatePage, IProductsByCategoryPage
    {
        #region Fields

        #region Selectors

        private readonly By breadcrumbsSelector = By.CssSelector(".breadcrumb > ul > li > *:first-child");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsByCategoryPage"/> class.
        /// </summary>
        /// <param name="basePage">basePage</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public ProductsByCategoryPage(
            IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(basePage,
                  pageObjectFactory,
                  driver,
                  pageSettings)
        {
            Uri = new Uri(pageSettings.BaseUrl);
        }

        #endregion

        #region Properties

        #region Elements

        private IReadOnlyList<IWebElement> BreadcrumbElements => WrappedDriver.FindElements(breadcrumbsSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the breadcrumb.
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyList<string> GetBreadcrumb()
        {
            return BreadcrumbElements
                .Select(e => e.TextHelper().InnerText)
                .ToList()
                .AsReadOnly();
        }

        #endregion
    }
}
