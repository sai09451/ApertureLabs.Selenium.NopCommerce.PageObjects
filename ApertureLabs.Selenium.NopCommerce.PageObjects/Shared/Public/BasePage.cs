using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.HeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public
{
    /// <summary>
    /// BasePage for all public pages.
    /// </summary>
    public class BasePage : PageObject, IBasePage
    {
        #region Fields

        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        private AdminHeaderLinksComponent adminHeaderLinks;

        #region Selectors

        private readonly By searchBoxSelector = By.CssSelector("#small-searchterms");
        private readonly By searchButtonSelector = By.CssSelector("#small-search-box-form .search-box-button");
        private readonly By searchAjaxLoadingSelector = By.CssSelector(".ui-autocomplete-loading");
        private readonly By ajaxSearchResultsSelector = By.CssSelector("#ui-id-1 a");
        private readonly By shoppingCartLinkSelector = By.CssSelector("#topcartlink a");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="pageObjectFactory"></param>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public BasePage(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver)
        {
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;
            Uri = new Uri(pageSettings.BaseUrl, UriKind.Absolute);

            HeaderLinks = new HeaderLinksComponent(
                pageObjectFactory,
                WrappedDriver);

            adminHeaderLinks = new AdminHeaderLinksComponent(
                WrappedDriver,
                pageSettings,
                pageObjectFactory);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public IAdminHeaderLinksComponent AdminHeaderLinks { get; private set; }

        private HeaderLinksComponent HeaderLinks { get; set; }

        #region Elements

        private InputElement SearchBoxElement => new InputElement(WrappedDriver.FindElement(searchBoxSelector));
        private IWebElement SearchBoxButtonElement => WrappedDriver.FindElement(searchButtonSelector);
        private IWebElement ShoppingCartLinkElement => WrappedDriver.FindElement(shoppingCartLinkSelector);

        #endregion

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override ILoadableComponent Load()
        {
            base.Load();

            // Load the headerlinks.
            pageObjectFactory.PrepareComponent(HeaderLinks);

            // Check if the admin header links exist, if they don't set to
            // null.
            if (WrappedDriver.FindElements(adminHeaderLinks.By).Any())
            {
                AdminHeaderLinks = pageObjectFactory
                    .PrepareComponent(adminHeaderLinks);
            }
            else
            {
                AdminHeaderLinks = null;
            }

            return this;
        }

        /// <summary>
        /// Logs a user out if logged in.
        /// </summary>
        /// <returns></returns>
        public virtual T Logout<T>() where T : IPageObject
        {
            if (IsLoggedIn())
            {
                var logoutUrl = new Uri(Uri, "/logout");
                WrappedDriver.Navigate().GoToUrl(logoutUrl);
            }

            var afterLogoutPage = pageObjectFactory.PreparePage<T>();

            return afterLogoutPage;
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual IHomePage Login(string email, string password)
        {
            return HeaderLinks.Login(email, password);
        }

        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLoggedIn()
        {
            var hasNopCookie = WrappedDriver.Manage().Cookies
                .GetCookieNamed("NOPCOMMERCE.AUTH");

            return hasNopCookie != null;
        }

        /// <summary>
        /// Similar to <c>Search</c> but waits for the ajax results to resolve
        /// and returns those items.
        /// </summary>
        /// <param name="searchFor">The search for.</param>
        public virtual IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            // Cache the searchbox element.
            var searchBoxEl = SearchBoxElement;
            searchBoxEl.SetValue(searchFor);

            // Wait for the loading class to toggle on the input el.
            WrappedDriver.Wait(TimeSpan.FromSeconds(30))
                .TrySequentialWait(
                    out var exc,
                    d => searchBoxEl.Is(searchAjaxLoadingSelector),
                    d => !searchBoxEl.Is(searchAjaxLoadingSelector));

            var els = WrappedDriver.FindElements(ajaxSearchResultsSelector);

            return els;
        }

        /// <summary>
        /// Used to search for a product.
        /// </summary>
        /// <param name="searchFor">Partial or full name of product.</param>
        /// <returns></returns>
        public virtual ISearchPage Search(string searchFor)
        {
            SearchBoxElement.SetValue(searchFor);
            SearchBoxButtonElement.Click();

            return pageObjectFactory.PreparePage<SearchPage>();
        }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        public virtual ICartPage GoToShoppingCart()
        {
            ShoppingCartLinkElement.Click();

            return pageObjectFactory.PreparePage<CartPage>();
        }

        #endregion
    }
}
