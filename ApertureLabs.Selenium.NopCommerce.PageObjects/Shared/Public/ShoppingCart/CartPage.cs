using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart
{
    /// <summary>
    /// CartPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.BasePage" />
    public class CartPage : PageObject, ICartPage
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IBasePage basePage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CartPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public CartPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver)
        {
            this.basePage = basePage;

            Uri = new Uri(
                new Uri(pageSettings.BaseUrl),
                "cart");

            OrderSummary = new OrderSummaryComponent(
                pageObjectFactory,
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        /// <summary>
        /// Gets the order summary.
        /// </summary>
        /// <value>
        /// The order summary.
        /// </value>
        public virtual OrderSummaryComponent OrderSummary { get; private set; }

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public virtual IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

        #endregion

        #region Methods

        public override ILoadableComponent Load()
        {
            base.Load();
            basePage.Load();
            OrderSummary.Load();

            return this;
        }

        public virtual ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
        }

        public virtual bool IsLoggedIn()
        {
            return basePage.IsLoggedIn();
        }

        public virtual IHomePage Login(string email, string password)
        {
            return basePage.Login(email, password);
        }

        public virtual T Logout<T>() where T : IPageObject
        {
            return basePage.Logout<T>();
        }

        public virtual ISearchPage Search(string searchFor)
        {
            return basePage.Search(searchFor);
        }

        public virtual IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            return basePage.SearchAjax(searchFor);
        }

        public bool HasNotifications()
        {
            return basePage.HasNotifications();
        }

        public void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        #endregion
    }
}
