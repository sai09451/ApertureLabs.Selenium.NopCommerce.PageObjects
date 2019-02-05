using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CategoryNavigation;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Abstract base class for the Catalog pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home.HomePage" />
    public abstract class CatalogTemplatePage : PageObject, ICatalogTemplatePage
    {
        #region Fields

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

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
        /// <param name="basePage">The base page.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        public CatalogTemplatePage(
            IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver)
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private CatalogBlockComponent CategoriesComponent => pageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                CategoryNavigationSelector));

        private CatalogBlockComponent ManufacturersComponent => pageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                ManufacturerSelector));

        private CatalogBlockComponent RecentlyViewProductsComponent => pageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                RecentlyViewedProductsSelector));

        private CatalogBlockComponent PopularTagsComponent => pageObjectFactory.PrepareComponent(
            new CatalogBlockComponent(
                WrappedDriver,
                PopularTagsSelector));

        #endregion

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

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

            return pageObjectFactory.PreparePage< ProductTagsAllPage>();
        }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        public virtual ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
        }

        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLoggedIn()
        {
            return basePage.IsLoggedIn();
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual IHomePage Login(string email, string password)
        {
            return basePage.Login(email, password);
        }

        /// <summary>
        /// Logs a user out if logged in.
        /// </summary>
        /// <returns></returns>
        public virtual T Logout<T>() where T : IPageObject
        {
            return basePage.Logout<T>();
        }

        /// <summary>
        /// Used to search for a product.
        /// </summary>
        /// <param name="searchFor">Partial or full name of product.</param>
        /// <returns></returns>
        public virtual ISearchPage Search(string searchFor)
        {
            return basePage.Search(searchFor);
        }

        /// <summary>
        /// Similar to <c>Search</c> but waits for the ajax results to resolve
        /// and returns those items.
        /// </summary>
        /// <param name="searchFor">The search for.</param>
        /// <returns></returns>
        public virtual IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            return basePage.SearchAjax(searchFor);
        }

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        public bool HasNotifications()
        {
            return basePage.HasNotifications();
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Selects the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public IProductsByCategoryPage SelectCategory(string category,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            SelectCategoryHelper(category, stringComparison);

            return pageObjectFactory.PreparePage<IProductsByCategoryPage>();
        }

        /// <summary>
        /// Selects a parent category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public IParentCategoryPage SelectParentCategory(string category,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            SelectCategoryHelper(category, stringComparison);

            return pageObjectFactory.PreparePage<IParentCategoryPage>();
        }

        private void SelectCategoryHelper(string category,
            StringComparison stringComparison)
        {
            var foundItem = false;
            var links = CategoriesComponent.GetItems()
                .Select(e => e.FindElement(By.CssSelector("a")));

            foreach (var item in links)
            {
                var text = item.TextHelper().InnerText;
                var matches = String.Equals(
                    category,
                    text,
                    stringComparison);

                if (matches)
                {
                    foundItem = true;
                    item.Click();
                    break;
                }
            }

            if (!foundItem)
                throw new NoSuchElementException();
        }

        #endregion
    }
}
