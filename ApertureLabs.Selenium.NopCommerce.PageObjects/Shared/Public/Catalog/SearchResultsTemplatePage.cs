using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Abstract class for any catalog search results page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public abstract class SearchResultsTemplatePage : CatalogTemplatePage, ISearchResultsTemplatePage
    {
        #region Fields

        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        #region Selectors

        private readonly By SortBySelector = By.CssSelector("#products-orderby");
        private readonly By DisplaySelector = By.CssSelector("#products-pagesize");
        private readonly By ViewModesSelector = By.CssSelector(".product-viewmode > a");
        private readonly By ActiveViewModeSelector = By.CssSelector(".product-viewmode > a.selected");
        private readonly By SearchResultsSelector = By.CssSelector(".item-box");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsTemplatePage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <param name="template">The template.</param>
        public SearchResultsTemplatePage(
            IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings,
            UriTemplate template)
            : base(basePage,
                  pageObjectFactory,
                  driver,
                  pageSettings,
                  template)
        {
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;
        }

        #endregion

        #region Properties

        #region Elements

        private SelectElement SortByElement => new SelectElement(WrappedDriver.FindElement(SortBySelector));
        private SelectElement DisplayElement => new SelectElement(WrappedDriver.FindElement(DisplaySelector));
        private IReadOnlyCollection<IWebElement> ViewModeElements => WrappedDriver.FindElements(ViewModesSelector);
        private IWebElement ActiveViewModeElement => WrappedDriver.FindElement(ActiveViewModeSelector);
        private IList<IWebElement> SearchResultItemElements => WrappedDriver.FindElements(SearchResultsSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the sort options.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetSortOptions()
        {
            var sortOptions = SortByElement.Options
                .Select(e => e.TextHelper().InnerText);

            return sortOptions;
        }

        /// <summary>
        /// Gets the current sort option.
        /// </summary>
        /// <returns></returns>
        public virtual string GetCurrentSortOption()
        {
            var currentSortOption = SortByElement.SelectedOption
                .TextHelper()
                .InnerText;

            return currentSortOption;
        }

        /// <summary>
        /// Sets the sort options.
        /// </summary>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public virtual void SetSortOptions(string sortOption,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var isAlreadySelected = !String.Equals(
                sortOption,
                GetCurrentSortOption(),
                stringComparison);

            if (!isAlreadySelected)
            {
                var option = SortByElement.Options
                    .Select((element, index) => new { element, index })
                    .First(opt => String.Equals(
                        sortOption,
                        opt.element.TextHelper().InnerText,
                        stringComparison));

                SortByElement.SelectByIndex(option.index);

                // Reload page references.
                Load();
            }
        }

        /// <summary>
        /// Gets all 'Display per page' options.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<int> GetDisplayOptions()
        {
            var displayOptions = DisplayElement.Options
                .Select(e => e.TextHelper().ExtractInteger());

            return displayOptions;
        }

        /// <summary>
        /// Gets the current 'Display per page' value.
        /// </summary>
        /// <returns></returns>
        public virtual int GetCurrentDisplayOption()
        {
            return DisplayElement.SelectedOption
                .TextHelper()
                .ExtractInteger();
        }

        /// <summary>
        /// Selects the 'Display per page' option.
        /// </summary>
        /// <param name="display">The display.</param>
        public virtual void SetDisplayOption(int display)
        {
            if (GetCurrentDisplayOption() != display)
            {
                DisplayElement.SelectByText(display.ToString());

                // Reload page references.
                Load();
            }
        }

        /// <summary>
        /// Gets the view modes.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetViewModes()
        {
            var viewModes = ViewModeElements.Select(e => e.GetAttribute("title"));

            return viewModes;
        }

        /// <summary>
        /// Gets the active view mode.
        /// </summary>
        /// <returns></returns>
        public virtual string GetActiveViewMode()
        {
            return ActiveViewModeElement.GetAttribute("title");
        }

        /// <summary>
        /// Sets the active view mode.
        /// </summary>
        /// <param name="viewMode">The view mode.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public virtual void SetActiveViewMode(string viewMode,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var alreadySelected = String.Equals(
                viewMode,
                GetActiveViewMode(),
                stringComparison);

            if (!alreadySelected)
            {
                ViewModeElements.First(e => String.Equals(
                        viewMode,
                        e.GetAttribute("title"),
                        stringComparison))
                    .Click();

                // Reload page references.
                Load();
            }
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
                        SearchResultsSelector,
                        pageObjectFactory,
                        element,
                        pageSettings)))
                .ToList();
        }

        #endregion
    }
}
