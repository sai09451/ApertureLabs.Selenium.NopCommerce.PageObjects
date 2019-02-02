﻿using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

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
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public CartPage(IBasePage basePage,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver)
        {
            this.basePage = basePage;

            Uri = new Uri(
                new Uri(pageSettings.BaseUrl),
                "cart");
        }

        #endregion

        #region Properties

        #region Elements

        /// <summary>
        /// Gets the order summary.
        /// </summary>
        /// <value>
        /// The order summary.
        /// </value>
        public virtual OrderSummaryComponent OrderSummary { get; private set; }

        #endregion

        public virtual IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

        #endregion

        #region Methods

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

        #endregion
    }
}
