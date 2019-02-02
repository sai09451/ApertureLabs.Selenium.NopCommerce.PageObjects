using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home
{
    /// <summary>
    /// HomePage.
    /// </summary>
    public class HomePage : PageObject, IHomePage
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public HomePage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver)
        {
            this.Uri = new Uri(pageSettings.BaseUrl);
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;
        }

        #endregion

        #region Properties

        public IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

        #endregion

        #region Methods

        public IReadOnlyCollection<IWebElement> GetFeaturedProducts()
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<IWebElement> GetNews()
        {
            throw new System.NotImplementedException();
        }

        public ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
        }

        public bool IsLoggedIn()
        {
            return basePage.IsLoggedIn();
        }

        public IHomePage Login(string email, string password)
        {
            return basePage.Login(email, password);
        }

        public T Logout<T>() where T : IPageObject
        {
            return basePage.Logout<T>();
        }

        public ISearchPage Search(string searchFor)
        {
            return basePage.Search(searchFor);
        }

        public IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            return basePage.SearchAjax(searchFor);
        }

        #endregion
    }
}
