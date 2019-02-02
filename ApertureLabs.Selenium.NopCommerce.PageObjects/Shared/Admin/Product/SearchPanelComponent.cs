using System;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The search component of the list page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class SearchPanelComponent : PageComponent, ISearchPanelComponent
    {
        #region Fields

        #region Selectors

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPanelComponent"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        public SearchPanelComponent(IWebDriver driver, By selector)
            : base(driver, selector)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        public virtual SearchPanelComponent EnterProductName(string productName)
        {
            throw new NotImplementedException();
        }

        public virtual SearchPanelComponent SelectCategory(string categoryName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        public virtual SearchPanelComponent SetSearchSubCategories(bool searchSubCats)
        {
            throw new NotImplementedException();
        }

        public virtual SearchPanelComponent SelectStore(string storeName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        public virtual SearchPanelComponent SelectProductType(
            string productType,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        public virtual SearchPanelComponent SelectPublished(
            string publishedStatus,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        public virtual IListPage Search()
        {
            throw new NotImplementedException();
        }

        public virtual IEditPage GoDirectlyToSku(string sku)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
