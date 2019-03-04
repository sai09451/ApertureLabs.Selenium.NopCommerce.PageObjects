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
    public class BasePage : BasePageObject, IBasePage
    {
        #region Fields

        #region Selectors

        private readonly By searchBoxSelector = By.CssSelector("#small-searchterms");
        private readonly By searchButtonSelector = By.CssSelector("#small-search-box-form .search-box-button");
        private readonly By searchAjaxLoadingSelector = By.CssSelector(".ui-autocomplete-loading");
        private readonly By ajaxSearchResultsSelector = By.CssSelector("#ui-id-1 a");
        private readonly By shoppingCartLinkSelector = By.CssSelector("#topcartlink a");
        private readonly By barNotificationSelector = By.CssSelector("#bar-notification");
        private readonly By closeBarNotificationsSelector = By.CssSelector("#bar-notification .close");
        private readonly By dialogNotificationsSuccessSelector = By.CssSelector("#dialog-notifications-success");
        private readonly By dialogNotificationsErrorSelector = By.CssSelector("#dialog-notifications-error");
        private readonly By dialogNotificationsWarningSelector = By.CssSelector("#dialog-notifications-warning");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        private AdminHeaderLinksComponent adminHeaderLinks;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public BasePage(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver)
        {
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;

            HeaderLinks = new HeaderLinksComponent(
                pageObjectFactory,
                WrappedDriver);

            adminHeaderLinks = new AdminHeaderLinksComponent(
                pageObjectFactory,
                WrappedDriver,
                pageSettings);
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
        private IWebElement BarNotificationElement => WrappedDriver.FindElement(barNotificationSelector);
        private IWebElement CloseNotificationBarElement => WrappedDriver.FindElement(closeBarNotificationsSelector);
        private IWebElement DialogNotificationsSuccessElement => WrappedDriver.FindElement(dialogNotificationsSuccessSelector);
        private IWebElement DialogNotificationsErrorElement => WrappedDriver.FindElement(dialogNotificationsErrorSelector);
        private IWebElement DialogNotificationsWarningElement => WrappedDriver.FindElement(dialogNotificationsWarningSelector);

        #endregion

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
        /// By default will to see if the pages original window handle still
        /// exists and that windows url matches the Uri.
        /// </summary>
        /// <returns></returns>
        public override bool IsStale()
        {
            if (!WrappedDriver.Url.StartsWith(Uri.ToString()))
                return true;
            else if (WindowHandle != WrappedDriver.CurrentWindowHandle)
                return true;
            else
                return false;
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

            return pageObjectFactory.PreparePage<ISearchPage>();
        }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        public virtual ICartPage GoToShoppingCart()
        {
            ShoppingCartLinkElement.Click();

            return pageObjectFactory.PreparePage<ICartPage>();
        }

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        public bool HasNotifications()
        {
            return DialogNotificationsErrorElement.Displayed
                || DialogNotificationsSuccessElement.Displayed
                || DialogNotificationsWarningElement.Displayed
                || BarNotificationElement.Displayed;
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="dialogHandler">The element.</param>
        public void HandleNotification(Action<IWebElement> dialogHandler)
        {
            if (DialogNotificationsErrorElement.Displayed)
                dialogHandler(DialogNotificationsErrorElement);
            else if (DialogNotificationsSuccessElement.Displayed)
                dialogHandler(DialogNotificationsSuccessElement);
            else if (DialogNotificationsWarningElement.Displayed)
                dialogHandler(DialogNotificationsWarningElement);
            else if (BarNotificationElement.Displayed)
                dialogHandler(BarNotificationElement);
            else
                throw new NoSuchElementException();
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DismissNotifications()
        {
            if (BarNotificationElement.Displayed)
            {
                CloseNotificationBarElement.Click();

                // Wait until the bar notification is no longer displayed.
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(5))
                    .Until(d => !BarNotificationElement.Displayed);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
