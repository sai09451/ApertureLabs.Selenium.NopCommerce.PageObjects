using System;
using System.Collections.Generic;
using System.Text;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout.ICheckoutPage" />
    public class CheckoutPage : PageObject, ICheckoutPage, IOnePageCheckoutPage
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructors

        public CheckoutPage(By selector,
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
