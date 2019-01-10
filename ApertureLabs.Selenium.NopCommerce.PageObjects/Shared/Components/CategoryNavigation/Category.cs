using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CategoryNavigation
{
    /// <summary>
    /// Represents a category.
    /// </summary>
    public class Category : IWrapsElement
    {
        #region Fields

        #region Selectors

        private readonly By SubCategoriesSelector = By.CssSelector(".sublist > li");

        #endregion

        #endregion

        #region Constructor

        public Category(IWebElement element)
        {
            WrappedElement = element;
        }

        #endregion

        #region Properties

        #region Elements

        public IWebElement WrappedElement { get; private set; }

        private IWebElement SubCategoryElement => WrappedElement.Children().FirstOrDefault(e => e.);

        #endregion

        #endregion

        #region Methods

        public virtual IEnumerable<Category> GetSubCategories()
        {
            throw new NotImplementedException();
        }

        public virtual string GetCategoryName()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
