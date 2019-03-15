using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CustomerNavigation;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// AddressesPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer.IAddressesPage" />
    public class AddressesPage : StaticPageObject, IAddressesPage
    {
        #region Fields

        #region Selectors

        private readonly By addressRowsSelector = By.CssSelector(".address-list > .address-item");
        private readonly By addNewSelector = By.CssSelector(".add-address-button");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressesPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <exception cref="ArgumentNullException">
        /// pageSettings
        /// or
        /// basePage
        /// or
        /// pageObjectFactory
        /// </exception>
        public AddressesPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  new Uri(pageSettings.BaseUrl, "customer/addresses"))
        {
            if (pageSettings == null)
                throw new ArgumentNullException(nameof(pageSettings));

            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            AccountNavigation = new CustomerNavigationComponent<IAddressesPage>(
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddNewElement => WrappedDriver.FindElement(addNewSelector);
        private IReadOnlyCollection<IWebElement> AddressRowElements => WrappedDriver.FindElements(addressRowsSelector);

        #endregion

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public virtual IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

        /// <summary>
        /// Gets the account navigation.
        /// </summary>
        /// <value>
        /// The account navigation.
        /// </value>
        public virtual CustomerNavigationComponent<IAddressesPage> AccountNavigation { get; private set; }

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
            pageObjectFactory.PrepareComponent(AccountNavigation);

            return this;
        }

        /// <summary>
        /// Begins the process of adding a new address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IAddressesAddPage AddNew()
        {
            AddNewElement.Click();

            return pageObjectFactory.PreparePage<IAddressesAddPage>();
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<AddressesRowComponent<IAddressesPage>> GetAddresses()
        {
            foreach (var row in AddressRowElements)
            {
                yield return pageObjectFactory.PrepareComponent(
                    new AddressesRowComponent<IAddressesPage>(
                        ByElement.FromElement(row),
                        pageObjectFactory,
                        WrappedDriver,
                        this));
            }
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
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasNotifications()
        {
            return basePage.HasNotifications();
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
        /// <typeparam name="T"></typeparam>
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

        #endregion
    }
}
