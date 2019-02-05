using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// OnePageCheckoutPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout.ICheckoutPage" />
    public class OnePageCheckoutPage : PageObject, IOnePageCheckoutPage
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OnePageCheckoutPage"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OnePageCheckoutPage(By selector,
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

        #endregion

        public IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

        #endregion

        #region Methods

        public void Confirm()
        {
            throw new NotImplementedException();
        }

        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        public void EnterBillingAddress(AddressModel address)
        {
            throw new NotImplementedException();
        }

        public void EnterPaymentInformation()
        {
            throw new NotImplementedException();
        }

        public void EnterShippingAddress(AddressModel address)
        {
            throw new NotImplementedException();
        }

        public AddressModel GetBillingAddress()
        {
            throw new NotImplementedException();
        }

        public int GetCurrentStep()
        {
            throw new NotImplementedException();
        }

        public object GetPaymentInformation()
        {
            throw new NotImplementedException();
        }

        public string GetPaymentMethod()
        {
            throw new NotImplementedException();
        }

        public AddressModel GetShippingAddress()
        {
            throw new NotImplementedException();
        }

        public string GetShippingMethod()
        {
            throw new NotImplementedException();
        }

        public ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
        }

        public void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        public bool HasNotifications()
        {
            return basePage.HasNotifications();
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

        public void SelectPaymentMethod()
        {
            throw new NotImplementedException();
        }

        public void SelectShippingMethod()
        {
            throw new NotImplementedException();
        }

        public void TryGoToStep(int step, Action<IOnePageCheckoutPage> resolve, Action<IOnePageCheckoutPage> reject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
