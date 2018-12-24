using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Factories;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.ProductBreadCrumb
{
    /// <summary>
    /// ProductBreadCrumbComponent.
    /// </summary>
    public class ProductBreadCrumbComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By categoriesSelector = By.CssSelector("li:not(:first-child) a");

        #endregion

        private readonly PageSettings pageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="pageSettings"></param>
        /// <param name="driver"></param>
        /// <param name="selector"></param>
        public ProductBreadCrumbComponent(PageSettings pageSettings,
            IWebDriver driver,
            By selector = null)
            : base(driver, selector ?? By.CssSelector(".breadcrumb"))
        {
            this.pageSettings = pageSettings;
        }

        #endregion

        #region Properties

        #region Elements

        /// <summary>
        /// Excludes the 'Home' category.
        /// </summary>
        private IList<IWebElement> CategoryElements
        {
            get
            {
                return WrappedElement.FindElements(categoriesSelector);
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves the names of the categories (doens't include 'Home').
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCategories()
        {
            return CategoryElements.Select(el => el.Text).ToList();
        }

        /// <summary>
        /// Clicks on the category link
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public SearchPage ViewCategory(string categoryName)
        {
            CategoryElements
                .First(el => string.Equals(
                    el.Text,
                    categoryName,
                    StringComparison.OrdinalIgnoreCase))
                .Click();

            return new CustomPageObjectFactory()
                .PreparePage(new SearchPage(WrappedDriver, pageSettings));
        }

        /// <inheritdoc/>
        public override bool IsStale()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
