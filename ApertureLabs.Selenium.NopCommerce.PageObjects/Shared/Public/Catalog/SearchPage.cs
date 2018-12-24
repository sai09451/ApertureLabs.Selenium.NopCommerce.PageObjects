using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Factories;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium.Components.Nop;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// SearchPage.
    /// </summary>
    public class SearchPage : BasePage, IViewModel<SearchModel>
    {
        #region Fields

        #region Selectors

        private readonly By searchTermInputSelector = By.CssSelector("#q");
        private readonly By advancedSearchSelector = By.CssSelector("#adv");
        private readonly By categorySelector = By.CssSelector("#cid");
        private readonly By searchSubCatSelector = By.CssSelector("isc");
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

        private InputElement SearchTermInputElement => new InputElement(WrappedDriver.FindElement(searchTermInputSelector));
        private CheckboxElement AdvancedSearchElement => new CheckboxElement(WrappedDriver.FindElement(advancedSearchSelector));
        private SelectElement CategoryElement => new SelectElement(WrappedDriver.FindElement(categorySelector));
        private CheckboxElement SearchSubCategoriesElement => new CheckboxElement(WrappedDriver.FindElement(searchSubCatSelector));
        private InputElement PriceFromElement => new InputElement(WrappedDriver.FindElement(priceFromSelector));
        private InputElement PriceToElement => new InputElement(WrappedDriver.FindElement(priceToSelector));
        private CheckboxElement SearchProductDescElement => new CheckboxElement(WrappedDriver.FindElement(searchInProductDescSelector));
        private PagerComponent PagerComponent => new PagerComponent(WrappedDriver);
        private CatalogPagingFilterComponent PagingFilterComponent => new CatalogPagingFilterComponent(WrappedDriver);
        private IWebElement SearchButtonElement => WrappedDriver.FindElement(searchButtonSelector);
        private IList<IWebElement> SearchResultItemElements => WrappedDriver.FindElements(searchResultsSelector);

        #endregion

        /// <summary>
        /// ViewModel.
        /// </summary>
        public SearchModel ViewModel
        {
            get
            {
                if (IsStale())
                    throw new InvalidElementStateException();

                var model = new SearchModel
                {
                    SearchTerm = SearchTermInputElement.GetValue<string>(),
                    AdvancedSearch = AdvancedSearchElement.IsChecked
                };

                if (model.AdvancedSearch)
                {
                    model.Category = int.Parse(CategoryElement.SelectedOption.GetAttribute("value"));
                    model.IncludeSubCategories = SearchSubCategoriesElement.IsChecked;
                    model.PriceFrom = PriceFromElement.GetValue<string>();
                    model.PriceTo = PriceToElement.GetValue<string>();
                    model.SearchInDescriptions = SearchProductDescElement.IsChecked;
                }

                return model;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the category.
        /// </summary>
        /// <param name="category">The text of the option</param>
        /// <returns></returns>
        public SearchPage SetCategory(string category)
        {
            AdvancedSearchElement.Check(true);
            CategoryElement.SelectByText(category);

            return this;
        }

        /// <summary>
        /// Sets the price range.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public SearchPage SetPriceRange(decimal? from = null, decimal? to = null)
        {
            PriceFromElement.Clear();
            PriceToElement.Clear();

            if (from.HasValue)
                PriceFromElement.SetValue(from.Value);

            if (to.HasValue)
                PriceToElement.SetValue(to.Value);

            return this;
        }

        /// <summary>
        /// Checks/unchecks the option to search sub categories.
        /// </summary>
        /// <param name="searchSubCat"></param>
        /// <returns></returns>
        public SearchPage SetSearchSubCategories(bool searchSubCat)
        {
            SearchSubCategoriesElement.Check(searchSubCat);

            return this;
        }

        /// <summary>
        /// Searchs for a product.
        /// </summary>
        /// <param name="searchFor"></param>
        /// <returns></returns>
        public override SearchPage Search(string searchFor)
        {
            SearchTermInputElement.SetValue(searchFor);
            SearchButtonElement.Click();

            return this;
        }

        /// <summary>
        /// Retrieves the items listed.
        /// </summary>
        /// <returns></returns>
        public IList<SearchResult> GetResults()
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
