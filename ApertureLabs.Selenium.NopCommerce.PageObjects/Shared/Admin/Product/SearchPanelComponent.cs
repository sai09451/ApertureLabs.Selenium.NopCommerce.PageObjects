using System;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// TODO: Finish component.
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
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public SearchPanelComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Enters the name of the product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual SearchPanelComponent EnterProductName(string productName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual SearchPanelComponent SelectCategory(string categoryName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the search sub categories.
        /// </summary>
        /// <param name="searchSubCats">if set to <c>true</c> [search sub cats].</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual SearchPanelComponent SetSearchSubCategories(bool searchSubCats)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the store.
        /// </summary>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual SearchPanelComponent SelectStore(string storeName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the type of the product.
        /// </summary>
        /// <param name="productType">Type of the product.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual SearchPanelComponent SelectProductType(
            string productType,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects the published status.
        /// </summary>
        /// <param name="publishedStatus">The published status.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual SearchPanelComponent SelectPublished(
            string publishedStatus,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IListPage Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Goes the directly to sku.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IEditPage GoDirectlyToSku(string sku)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
