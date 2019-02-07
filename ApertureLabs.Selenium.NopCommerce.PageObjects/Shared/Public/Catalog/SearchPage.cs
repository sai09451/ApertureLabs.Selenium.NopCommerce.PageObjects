using System;
using System.Linq;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CatalogPagingFilter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.Pager;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// SearchPage.
    /// </summary>
    public class SearchPage : SearchResultsTemplatePage, ISearchPage
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
        private readonly By pagerSelector = By.CssSelector(".pager");
        private readonly By pagingFilterSelcetor = By.CssSelector(".search-input");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="settings">The settings.</param>
        public SearchPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings settings)
            : base(basePage,
                  pageObjectFactory,
                  driver,
                  settings)
        {
            this.pageObjectFactory = pageObjectFactory;

            Uri = new Uri(
                new Uri(settings.BaseUrl),
                "search");
        }

        #endregion

        #region Properties

        #region Elements

        private PagerComponent PagerComponent { get; set; }
        private CatalogPagingFilterComponent PagingFilterComponent { get; set; }
        private IWebElement SearchButtonElement => WrappedDriver.FindElement(searchButtonSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// If overridding this don't forget to call base.Load().
        /// NOTE: Will navigate to the pages url if the current drivers url
        /// is empty.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// If the driver is an EventFiringWebDriver an event listener will
        /// be added to the 'Navigated' event and uses the url to determine
        /// if the page is 'stale'.
        /// </remarks>
        public override ILoadableComponent Load()
        {
            base.Load();

            if (WrappedDriver.FindElements(pagingFilterSelcetor).Any())
            {
                PagingFilterComponent = new CatalogPagingFilterComponent(
                    By.CssSelector(".search-input"),
                    WrappedDriver);

                pageObjectFactory.PrepareComponent(PagingFilterComponent);
            }
            else
            {
                PagingFilterComponent = null;
            }

            if (WrappedDriver.FindElements(pagerSelector).Any())
            {
                PagerComponent = new PagerComponent(
                    By.CssSelector(".pager"),
                    WrappedDriver);

                pageObjectFactory.PrepareComponent(PagerComponent);
            }
            else
            {
                PagerComponent = null;
            }

            return this;
        }

        /// <summary>
        /// Searchs for a product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual ISearchPage Search(SearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            PagingFilterComponent.Search(model);

            // Page is reloaded call Load() to refresh any cached content even
            // though there shouldn't be any.
            Load();

            return this;
        }

        #endregion
    }
}
