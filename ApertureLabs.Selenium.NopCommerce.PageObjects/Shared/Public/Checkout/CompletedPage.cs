using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// CompletedPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout.ICompletedPage" />
    public class CompletedPage : PageObject, ICompletedPage
    {
        #region Fields

        #region Selectors

        private readonly By orderDetailsSelector = By.CssSelector(".details-link");
        private readonly By orderNumberSelector = By.CssSelector(".order-number");
        private readonly By continueSelector = By.CssSelector(".order-completed-continue-button");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CompletedPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public CompletedPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver)
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement OrderNumberElement => WrappedDriver.FindElement(orderNumberSelector);
        private IWebElement OrderDetailsElement => WrappedDriver.FindElement(orderDetailsSelector);
        private IWebElement ContinueElement => WrappedDriver.FindElement(continueSelector);

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

            return this;
        }

        /// <summary>
        /// Continues to the home page.
        /// </summary>
        /// <returns></returns>
        public IHomePage Continue()
        {
            ContinueElement.Click();

            return pageObjectFactory.PreparePage<IHomePage>();
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Gets the order number.
        /// </summary>
        /// <returns></returns>
        public int GetOrderNumber()
        {
            return OrderNumberElement.TextHelper().ExtractInteger();
        }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        public ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
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
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn()
        {
            return basePage.IsLoggedIn();
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IHomePage Login(string email, string password)
        {
            return basePage.Login(email, password);
        }

        /// <summary>
        /// Logs a user out if logged in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Logout<T>() where T : IPageObject
        {
            return basePage.Logout<T>();
        }

        /// <summary>
        /// Used to search for a product.
        /// </summary>
        /// <param name="searchFor">Partial or full name of product.</param>
        /// <returns></returns>
        public ISearchPage Search(string searchFor)
        {
            return basePage.Search(searchFor);
        }

        /// <summary>
        /// Similar to <c>Search</c> but waits for the ajax results to resolve
        /// and returns those items.
        /// </summary>
        /// <param name="searchFor">The search for.</param>
        /// <returns></returns>
        public IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            return basePage.SearchAjax(searchFor);
        }

        /// <summary>
        /// Views the order details.
        /// </summary>
        /// <returns></returns>
        public IOrderDetailsPage ViewOrderDetails()
        {
            OrderDetailsElement.Click();

            return pageObjectFactory.PreparePage<IOrderDetailsPage>();
        }

        #endregion
    }
}
