using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CatalogPagingFilter
{
    /// <summary>
    /// The element containg all of the search
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class CatalogPagingFilterComponent : PageComponent, IViewModel<SearchModel>
    {
        #region Fields

        #region Selectors

        private readonly By SearchKeywordSelector = By.CssSelector("#q");
        private readonly By AdvancedSearchCheckboxSelector = By.CssSelector("#adv");
        private readonly By CategoryDropDownSelector = By.CssSelector("#cid");
        private readonly By AutomaticallySearchSubCategoriesSelector = By.CssSelector("#isc");
        private readonly By ManufacturerDropDownSelector = By.CssSelector("#mid");
        private readonly By PriceFromSelector = By.CssSelector("#pf");
        private readonly By PriceToSelector = By.CssSelector("#pt");
        private readonly By SearchInProductDescriptionsSelector = By.CssSelector("#sid");
        private readonly By SearchSelector = By.CssSelector("input.search-button[type='submit]");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogPagingFilterComponent"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        public CatalogPagingFilterComponent(IWebDriver driver, By selector)
            : base(driver, selector)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the 'Advanced search'
        /// option is checked.
        public virtual bool AdvancedSearch
        {
            get => AdvancedSearchCheckboxElement.IsChecked;
            set => AdvancedSearchCheckboxElement.Check(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the option 'Automatically
        /// search sub categories' is checked..
        /// </summary>
        public virtual bool AutomaticallySearchSubCategories
        {
            get => AutomaticallySearchSubCategoriesElement.IsChecked;
            set => AutomaticallySearchSubCategoriesElement.Check(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the option 'Search in
        /// product descriptions' is checked.
        /// </summary>
        public virtual bool SearchInProductDescriptions
        {
            get => SearchInProductDescriptionsElement.IsChecked;
            set => SearchInProductDescriptionsElement.Check(value);
        }

        #region Elements

        private InputElement SearchKeywordElement => new InputElement(WrappedElement.FindElement(SearchKeywordSelector));
        private CheckboxElement AdvancedSearchCheckboxElement => new CheckboxElement(WrappedElement.FindElement(AdvancedSearchCheckboxSelector));
        private SelectElement CategoryDropDownElement => new SelectElement(WrappedElement.FindElement(CategoryDropDownSelector));
        private CheckboxElement AutomaticallySearchSubCategoriesElement => new CheckboxElement(WrappedElement.FindElement(AutomaticallySearchSubCategoriesSelector));
        private SelectElement ManufacturerDropDownElement => new SelectElement(WrappedElement.FindElement(ManufacturerDropDownSelector));
        private InputElement PriceFromElement => new InputElement(WrappedElement.FindElement(PriceFromSelector));
        private InputElement PriceToElement => new InputElement(WrappedElement.FindElement(PriceToSelector));
        private CheckboxElement SearchInProductDescriptionsElement => new CheckboxElement(WrappedElement.FindElement(AutomaticallySearchSubCategoriesSelector));
        private IWebElement SearchElement => WrappedElement.FindElement(SearchSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// ViewModel.
        /// </summary>
        public virtual SearchModel ViewModel()
        {
            if (IsStale())
                throw new InvalidElementStateException();

            var model = new SearchModel
            {
                AdvancedSearch = AdvancedSearch,
                SearchTerm = GetSearchKeyword()
            };

            if (model.AdvancedSearch)
            {
                model.Category = GetCategory();
                model.IncludeSubCategories = AutomaticallySearchSubCategories;
                model.PriceFrom = GetPriceFrom();
                model.PriceTo = GetPriceTo();
                model.SearchInDescriptions = SearchInProductDescriptions;
            }

            return model;
        }

        /// <summary>
        /// Searches the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void Search(SearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (String.IsNullOrEmpty(model.SearchTerm))
                SearchKeywordElement.Clear();
            else
                SearchKeywordElement.SetValue(model.SearchTerm);

            AdvancedSearch = model.AdvancedSearch;

            if (model.AdvancedSearch)
            {
                SetCategory(model.Category ?? "All");
                SetAutomaticallySearchSubCategories(model.IncludeSubCategories);
                SetManufacturer(model.Manufacturer ?? "All");
                SetPriceRange(model.PriceFrom, model.PriceTo);
                SetSearchInProductDescriptions(model.SearchInDescriptions);
            }

            Search();
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        public virtual void Search()
        {
            SearchElement.Click();
        }

        /// <summary>
        /// Sets the advanced search.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetAdvancedSearch(bool value)
        {
            AdvancedSearch = value;

            return this;
        }

        /// <summary>
        /// Sets the category by matching the text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="partialMatch">if set to <c>true</c> [partial match].</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetCategory(string value,
            bool partialMatch = false)
        {
            CategoryDropDownElement.SelectByText(value, partialMatch);

            return this;
        }

        /// <summary>
        /// Sets the automatically search sub categories.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetAutomaticallySearchSubCategories(bool value)
        {
            AutomaticallySearchSubCategoriesElement.Check(value);

            return this;
        }

        /// <summary>
        /// Sets the manufacturer by matching the text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="partialMatch">if set to <c>true</c> [partial match].</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetManufacturer(string value,
            bool partialMatch = false)
        {
            ManufacturerDropDownElement.SelectByText(value, partialMatch);

            return this;
        }

        /// <summary>
        /// Sets the price from.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetPriceFrom(decimal? value)
        {
            if (value.HasValue)
                PriceFromElement.SetValue(value.Value);
            else
                PriceFromElement.Clear();

            return this;
        }

        /// <summary>
        /// Sets the price to.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetPriceTo(decimal? value)
        {
            if (value.HasValue)
                PriceToElement.SetValue(value.Value);
            else
                PriceToElement.Clear();

            return this;
        }

        /// <summary>
        /// Sets the price range. Shorthand for calling both <c>SetPriceFrom</c>
        /// and <c>SetPriceTo</c>.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetPriceRange(decimal? from, decimal? to)
        {
            return SetPriceFrom(from).SetPriceTo(to);
        }

        /// <summary>
        /// Sets the search in product descriptions.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public virtual CatalogPagingFilterComponent SetSearchInProductDescriptions(bool value)
        {
            SearchInProductDescriptionsElement.Check(value);

            return this;
        }

        /// <summary>
        /// Gets the search keyword.
        /// </summary>
        /// <returns></returns>
        public virtual string GetSearchKeyword()
        {
            return SearchKeywordElement.GetValue<string>();
        }

        /// <summary>
        /// Gets all listed categories.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetCategories()
        {
            return CategoryDropDownElement.Options
                .Select(e => e.TextHelper().InnerText);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <returns></returns>
        public virtual string GetCategory()
        {
            return CategoryDropDownElement.SelectedOption
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets all listed manufacturers.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetManufacturers()
        {
            return ManufacturerDropDownElement.Options
                .Select(e => e.TextHelper().InnerText);
        }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        /// <returns></returns>
        public virtual string GetManufacturer()
        {
            return ManufacturerDropDownElement.SelectedOption
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets the price from.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetPriceFrom()
        {
            return PriceFromElement.GetValue<decimal>();
        }

        /// <summary>
        /// Gets the price to.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetPriceTo()
        {
            return PriceToElement.GetValue<decimal>();
        }

        /// <summary>
        /// Gets the price range where the first item is 'Price from' and the
        /// second item is the 'Price to'.
        /// </summary>
        /// <returns></returns>
        public virtual Tuple<decimal, decimal> GetPriceRange()
        {
            return Tuple.Create(GetPriceFrom(), GetPriceTo());
        }

        #endregion
    }
}
