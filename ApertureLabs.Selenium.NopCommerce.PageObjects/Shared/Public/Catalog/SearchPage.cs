using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CatalogPagingFilter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.Pager;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Factories;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// SearchPage.
    /// </summary>
    public class SearchPage : HomePage
    {
        #region Fields

        #region Selectors

        private readonly By searchTermInputSelector = By.CssSelector("#q");
        private readonly By advancedSearchSelector = By.CssSelector("#adv");
        private readonly By categorySelector = By.CssSelector("#cid");
        private readonly By searchSubCatSelector = By.CssSelector("#isc");
        private readonly By priceFromSelector = By.CssSelector("#pf");
        private readonly By priceToSelector = By.CssSelector("#pt");
        private readonly By searchInProductDescSelector = By.CssSelector("#sid");
        private readonly By searchButtonSelector = By.CssSelector(".buttons input[type=\"submit\"]");
        private readonly By searchResultsSelector = By.CssSelector(".item-box");

        #endregion

        private readonly CustomPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="settings"></param>
        public SearchPage(IWebDriver driver, PageSettings settings)
            : base(driver, settings)
        {
            pageObjectFactory = new CustomPageObjectFactory();
        }

        #endregion

        #region Properties

        #region Elements

        private PagerComponent PagerComponent => new PagerComponent(WrappedDriver, By.CssSelector(".pager"));
        private CatalogPagingFilterComponent PagingFilterComponent => new CatalogPagingFilterComponent(WrappedDriver, By.CssSelector(".search-input"));
        private IWebElement SearchButtonElement => WrappedDriver.FindElement(searchButtonSelector);
        private IList<IWebElement> SearchResultItemElements => WrappedDriver.FindElements(searchResultsSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Searchs for a product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual SearchPage Search(SearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            PagingFilterComponent.Search(model);

            // Page is reloaded call Load() to refresh any cached content even
            // though there shouldn't be any.
            Load();

            return this;
        }

        /// <summary>
        /// Retrieves the items listed.
        /// </summary>
        /// <returns></returns>
        public virtual IList<SearchResult> GetResults()
        {
            return SearchResultItemElements
                .Select(element => pageObjectFactory
                    .PrepareComponent(new SearchResult(
                        searchResultsSelector,
                        element,
                        PageSettings)))
                .ToList();
        }

        #endregion
    }
}
