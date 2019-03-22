using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

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

        private readonly By productNameSelector = By.CssSelector("#SearchProductName");
        private readonly By categorySelector = By.CssSelector("#SearchCategoryId");
        private readonly By searchSubCategoriesSelector = By.CssSelector("#SearchIncludeSubCategories");
        private readonly By manufacturerSelector = By.CssSelector("#SearchManufacturerId");
        private readonly By vendorSelector = By.CssSelector("#SearchVendorId");
        private readonly By storeSelector = By.CssSelector("#SearchStoreId");
        private readonly By warehouseSelector = By.CssSelector("#SearchWarehouseId");
        private readonly By productTypeSelector = By.CssSelector("#SearchProductTypeId");
        private readonly By publishedSelector = By.CssSelector("#SearchPublishedId");
        private readonly By goDirectlyToProductSkuSelector = By.CssSelector("#GoDirectlyToSku");
        private readonly By goDirectlyToProductSkuSubmitSelector = By.CssSelector("#go-to-product-by-sku");
        private readonly By searchSelector = By.CssSelector("#search-products");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPanelComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public SearchPanelComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement ProductNameElement => new InputElement(
            WrappedElement.FindElement(
                productNameSelector));

        private SelectElement CategoryElement => new SelectElement(
            WrappedElement.FindElement(
                categorySelector));

        private CheckboxElement SearchSubCategoriesElement => new CheckboxElement(
            WrappedElement.FindElement(
                searchSubCategoriesSelector));

        private SelectElement ManufacturerElement => new SelectElement(
            WrappedElement.FindElement(
                manufacturerSelector));

        private SelectElement VendorElement => new SelectElement(
            WrappedElement.FindElement(
                vendorSelector));

        private SelectElement StoreElement => new SelectElement(
            WrappedElement.FindElement(
                storeSelector));

        private SelectElement WarehouseElement => new SelectElement(
            WrappedElement.FindElement(
                warehouseSelector));

        private SelectElement ProductTypeElement => new SelectElement(
            WrappedElement.FindElement(
                productTypeSelector));

        private SelectElement PublishedElement => new SelectElement(
            WrappedElement.FindElement(
                publishedSelector));

        private InputElement GoDirectlyToSkuElement => new InputElement(
            WrappedElement.FindElement(
                goDirectlyToProductSkuSelector));

        private IWebElement GoDirectlyToSkuSubmitElement => WrappedElement
            .FindElement(goDirectlyToProductSkuSubmitSelector);

        private IWebElement SearchElement => WrappedElement
            .FindElement(searchSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Enters the name of the product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns></returns>
        public virtual ISearchPanelComponent EnterProductName(string productName)
        {
            ProductNameElement.SetValue(productName);

            return this;
        }

        /// <summary>
        /// Selects the category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual ISearchPanelComponent SelectCategory(string categoryName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var indexOfCategory = CategoryElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    categoryName,
                    stringComparison));

            if (indexOfCategory == -1)
                throw new NoSuchElementException();

            CategoryElement.SelectByIndex(indexOfCategory);

            return this;
        }

        /// <summary>
        /// Sets the search sub categories.
        /// </summary>
        /// <param name="searchSubCats">if set to <c>true</c> [search sub cats].</param>
        /// <returns></returns>
        public virtual ISearchPanelComponent SetSearchSubCategories(bool searchSubCats)
        {
            SearchSubCategoriesElement.Check(searchSubCats);

            return this;
        }

        /// <summary>
        /// Selects the store.
        /// </summary>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual ISearchPanelComponent SelectStore(string storeName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var indexOfStore = StoreElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    storeName,
                    stringComparison));

            if (indexOfStore == -1)
                throw new NoSuchElementException();

            StoreElement.SelectByIndex(indexOfStore);

            return this;
        }

        /// <summary>
        /// Selects the type of the product.
        /// </summary>
        /// <param name="productType">Type of the product.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual ISearchPanelComponent SelectProductType(
            string productType,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var indexOfProduct = ProductTypeElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    productType,
                    stringComparison));

            if (indexOfProduct == -1)
                throw new NoSuchElementException();

            ProductTypeElement.SelectByIndex(indexOfProduct);

            return this;
        }

        /// <summary>
        /// Selects the published status.
        /// </summary>
        /// <param name="publishedStatus">The published status.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual ISearchPanelComponent SelectPublished(
            string publishedStatus,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = PublishedElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    publishedStatus,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            PublishedElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns></returns>
        public virtual IListPage Search()
        {
            var searchEl = SearchElement;
            searchEl.Click();

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => searchEl.IsStale());

            return pageObjectFactory.PreparePage<IListPage>();
        }

        /// <summary>
        /// Goes the directly to sku.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <returns></returns>
        public virtual IEditPage GoDirectlyToSku(string sku)
        {
            if (String.IsNullOrEmpty(sku))
                throw new ArgumentNullException(nameof(sku));

            GoDirectlyToSkuElement.SetValue(sku);
            GoDirectlyToSkuSubmitElement.Click();

            return pageObjectFactory.PreparePage<IEditPage>();
        }

        #endregion
    }
}
