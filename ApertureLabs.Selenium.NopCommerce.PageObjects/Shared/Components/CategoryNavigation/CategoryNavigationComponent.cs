using System;
using System.Collections.Generic;
using System.Text;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CategoryNavigation
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryNavigationComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By CategoriesSelector = By.CssSelector("");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="selector"></param>
        public CategoryNavigationComponent(IWebDriver driver, By selector)
            : base(driver, selector)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        public virtual string GetActiveCategory()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<string> GetCategories()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
