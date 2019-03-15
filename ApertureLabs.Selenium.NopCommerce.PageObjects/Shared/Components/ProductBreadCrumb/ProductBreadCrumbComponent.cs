using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

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

        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="pageSettings"></param>
        /// <param name="driver"></param>
        /// <param name="pageObjectFactory"></param>
        /// <param name="selector"></param>
        public ProductBreadCrumbComponent(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings,
            By selector = null)
            : base(selector ?? By.CssSelector(".breadcrumb"), driver)
        {
            if (pageSettings == null)
                throw new ArgumentNullException(nameof(pageSettings));
            else if (pageObjectFactory == null)
                throw new ArgumentNullException(nameof(pageObjectFactory));

            this.pageObjectFactory = pageObjectFactory;
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
        public virtual IList<string> GetCategories()
        {
            return CategoryElements.Select(el => el.Text).ToList();
        }

        /// <summary>
        /// Clicks on the category link
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public virtual SearchPage ViewCategory(string categoryName)
        {
            CategoryElements
                .First(el => string.Equals(
                    el.Text,
                    categoryName,
                    StringComparison.OrdinalIgnoreCase))
                .Click();

            return pageObjectFactory.PreparePage<SearchPage>();
        }

        #endregion
    }
}
