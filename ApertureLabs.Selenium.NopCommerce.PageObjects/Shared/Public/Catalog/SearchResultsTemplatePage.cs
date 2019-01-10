using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Abstract class for any catalog search results page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public abstract class SearchResultsTemplatePage : CatalogTemplatePage
    {
        #region Fields

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
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public SearchResultsTemplatePage(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(pageObjectFactory, driver, pageSettings)
        { }

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
        public IEnumerable<string> GetSortOptions()
        {
            var sortOptions = SortByElement.Options
                .Select(e => e.TextHelper().InnerText);

            return sortOptions;
        }

        /// <summary>
        /// Gets the current sort option.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentSortOption()
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
        public void SetSortOptions(string sortOption,
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
        /// Gets the display options.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetDisplayOptions()
        {
            var displayOptions = DisplayElement.Options
                .Select(e => e.TextHelper().ExtractInteger());

            return displayOptions;
        }

        /// <summary>
        /// Gets the current display option.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentDisplayOption()
        {
            return DisplayElement.SelectedOption
                .TextHelper()
                .ExtractInteger();
        }

        /// <summary>
        /// Selects the display option.
        /// </summary>
        /// <param name="display">The display.</param>
        public void SetDisplayOption(int display)
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
        public IEnumerable<string> GetViewModes()
        {
            var viewModes = ViewModeElements.Select(e => e.GetAttribute("title"));

            return viewModes;
        }

        /// <summary>
        /// Gets the active view mode.
        /// </summary>
        /// <returns></returns>
        public string GetActiveViewMode()
        {
            return ActiveViewModeElement.GetAttribute("title");
        }

        /// <summary>
        /// Sets the active view mode.
        /// </summary>
        /// <param name="viewMode">The view mode.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public void SetActiveViewMode(string viewMode,
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
                .Select(element => PageObjectFactory
                    .PrepareComponent(new SearchResult(
                        SearchResultsSelector,
                        PageObjectFactory,
                        element,
                        PageSettings)))
                .ToList();
        }

        #endregion
    }
}
