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
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Abstract base class for the Catalog pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home.HomePage" />
    public abstract class CatalogTemplatePage : ParameterPageObject,
        ICatalogTemplatePage
    {
        #region Fields

        #region Selectors

        private readonly By categoryNavigationSelector = By.CssSelector(".block.block-category-navigation");
        private readonly By manufacturerSelector = By.CssSelector(".block.block-manufacturer-navigation");
        private readonly By recentlyViewedProductsSelector = By.CssSelector(".block.block-recently-viewed-products");
        private readonly By popularTagsSelector = By.CssSelector(".block.block-popular-tags");
        private readonly By linkSelector = By.CssSelector("a:not(.product-picture)");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogTemplatePage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <param name="template">The template.</param>
        public CatalogTemplatePage(
            IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings,
            UriTemplate template)
            : base(driver,
                  pageSettings.BaseUrl,
                  template)
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;

            CategoriesComponent = new CatalogBlockComponent(
                categoryNavigationSelector,
                WrappedDriver);

            ManufacturersComponent = new CatalogBlockComponent(
                manufacturerSelector,
                WrappedDriver);

            PopularTagsComponent = new CatalogBlockComponent(
                popularTagsSelector,
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        private CatalogBlockComponent CategoriesComponent { get; set; }

        private CatalogBlockComponent ManufacturersComponent { get; set; }

        private CatalogBlockComponent RecentlyViewProductsComponent { get; set; }

        private CatalogBlockComponent PopularTagsComponent { get; set; }

        #endregion

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

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
            basePage.Load();

            pageObjectFactory.PrepareComponent(CategoriesComponent);
            pageObjectFactory.PrepareComponent(ManufacturersComponent);
            pageObjectFactory.PrepareComponent(PopularTagsComponent);

            // Verify this is displayed before loading it.
            if (WrappedDriver.FindElements(recentlyViewedProductsSelector).Any())
            {
                RecentlyViewProductsComponent = new CatalogBlockComponent(
                    recentlyViewedProductsSelector,
                    WrappedDriver);

                pageObjectFactory.PrepareComponent(RecentlyViewProductsComponent);
            }
            else
            {
                // Assign to null if not loaded.
                RecentlyViewProductsComponent = null;
            }

            return this;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetCategories()
        {
            var categories = CategoriesComponent
                .GetItems()
                .Select(e => e.FindElement(linkSelector)
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
                .Select(e => e.FindElement(linkSelector)
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
                    e.FindElement(linkSelector).TextHelper().InnerText,
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
                .Select(e => e.FindElement(linkSelector)
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
                    e.FindElement(linkSelector).TextHelper().InnerText,
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
                .Select(e => e.FindElement(linkSelector)
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
                    e.FindElement(linkSelector).TextHelper().InnerText,
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
