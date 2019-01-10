using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CategoryNavigation;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Abstract base class for the Catalog pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home.HomePage" />
    public abstract class CatalogTemplatePage : HomePage
    {
        #region Fields

        #region Selectors

        private readonly By CategoryNavigationSelector = By.CssSelector(".block.block-category-navigation");
        private readonly By ManufacturerSelector = By.CssSelector(".block.block-manufacturer-navigation");
        private readonly By RecentlyViewedProductsSelector = By.CssSelector(".block.block-recently-viewed-products");
        private readonly By PopularTagsSelector = By.CssSelector(".block.block-popular-tags");
        private readonly By LinkSelector = By.CssSelector("a:not(.product-picture)");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogTemplatePage"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        public CatalogTemplatePage(IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(pageObjectFactory, driver, pageSettings)
        { }

        #endregion

        #region Properties

        #region Elements

        private CatalogBlockComponent CategoriesComponent => PageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                CategoryNavigationSelector));

        private CatalogBlockComponent ManufacturersComponent => PageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                ManufacturerSelector));

        private CatalogBlockComponent RecentlyViewProductsComponent => PageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                RecentlyViewedProductsSelector));

        private CatalogBlockComponent PopularTagsComponent => PageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                PopularTagsSelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetCategories()
        {
            var categories = CategoriesComponent
                .GetItems()
                .Select(e => e.FindElement(LinkSelector)
                    .TextHelper()
                    .InnerText);

            return categories;
        }

        /// <summary>
        /// Gets the current category.
        /// </summary>
        /// <returns></returns>
        public virtual string GetCurrentCategory()
        {
            var selectedCategory = CategoriesComponent.GetItems()
                .FirstOrDefault(e => e.Classes().Contains("active"));

            return selectedCategory?.TextHelper().InnerText;
        }

        /// <summary>
        /// Selects the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public virtual void SelectCategory(string category,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            CategoriesComponent
                .GetItems()
                .First(e => String.Equals(
                    category,
                    e.FindElement(LinkSelector).TextHelper().InnerText,
                    stringComparison))
                .Click();
        }

        /// <summary>
        /// Gets the manufacturers.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetManufacturers()
        {
            var manufacturers = ManufacturersComponent
                .GetItems()
                .Select(e => e.FindElement(LinkSelector)
                    .TextHelper()
                    .InnerText);

            return manufacturers;
        }

        /// <summary>
        /// Selects the manufacturers.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public virtual void SelectManufacturers(string manufacturer,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            ManufacturersComponent.GetItems()
                .First(e => String.Equals(
                    manufacturer,
                    e.FindElement(LinkSelector).TextHelper().InnerText,
                    stringComparison))
                .Click();
        }

        /// <summary>
        /// Views all manufacturers.
        /// </summary>
        public virtual void ViewAllManufacturers()
        {
            ManufacturersComponent.ViewAll();
        }

        /// <summary>
        /// Gets the recently viewed products.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetRecentlyViewedProducts()
        {
            var recentlyViewedProducts = RecentlyViewProductsComponent
                .GetItems()
                .Select(e => e.FindElement(LinkSelector)
                    .TextHelper()
                    .InnerText);

            return recentlyViewedProducts;
        }

        /// <summary>
        /// Selects the recently viewed products.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public virtual void SelectRecentlyViewedProducts(string productName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            RecentlyViewProductsComponent.GetItems()
                .First(e => String.Equals(
                    productName,
                    e.FindElement(LinkSelector).TextHelper().InnerText,
                    stringComparison))
                .Click();
        }

        /// <summary>
        /// Gets the popular tags.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetPopularTags()
        {
            var popularTags = PopularTagsComponent
                .GetItems()
                .Select(e => e.FindElement(LinkSelector)
                    .TextHelper()
                    .InnerText);

            return popularTags;
        }

        /// <summary>
        /// Selects the popular tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public virtual void SelectPopularTag(string tag,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            PopularTagsComponent.GetItems()
                .First(e => String.Equals(
                    tag,
                    e.FindElement(LinkSelector).TextHelper().InnerText,
                    stringComparison))
                .Click();
        }

        /// <summary>
        /// Views all popular tags.
        /// </summary>
        /// <returns></returns>
        public virtual ProductTagsAllPage ViewAllPopularTags()
        {
            PopularTagsComponent.ViewAll();

            return PageObjectFactory.PreparePage< ProductTagsAllPage>();
        }

        #endregion
    }
}
