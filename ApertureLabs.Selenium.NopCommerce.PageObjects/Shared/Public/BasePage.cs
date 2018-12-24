﻿using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.HeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public
{
    /// <summary>
    /// BasePage for all public pages.
    /// </summary>
    public abstract class BasePage : PageObject
    {
        #region Fields

        #region Selectors

        private readonly By searchBoxSelector = By.CssSelector("#small-searchterms");
        private readonly By searchButtonSelector = By.CssSelector("#small-search-box-form input[type=\"submit\"");

        #endregion

        private HeaderLinksComponent headerLinks;

        /// <summary>
        /// Settings for the page.
        /// </summary>
        public readonly PageSettings PageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public BasePage(IWebDriver driver, PageSettings pageSettings) : base(driver)
        {
            this.PageSettings = pageSettings;
            Uri = new Uri(pageSettings.BaseUrl, UriKind.Absolute);
            headerLinks = new HeaderLinksComponent(WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement SearchBoxElement => new InputElement(WrappedDriver.FindElement(searchBoxSelector));
        private IWebElement SearchBoxButtonElement => WrappedDriver.FindElement(searchButtonSelector);

        #endregion

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override ILoadableComponent Load()
        {
            headerLinks.Load();
            return base.Load();
        }

        /// <summary>
        /// Logs a user out if logged in.
        /// </summary>
        /// <returns></returns>
        public HomePage Logout()
        {
            if (IsLoggedIn())
            {
                var logoutUrl = new Uri(Uri, "/logout");
                WrappedDriver.Navigate().GoToUrl(logoutUrl);
            }

            var homePage = new HomePage(WrappedDriver, null);
            homePage.Load(true);

            return homePage;
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public HomePage Login(string email, string password)
        {
            return headerLinks.Login(email, password);
        }

        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn()
        {
            var hasNopCookie = WrappedDriver.Manage().Cookies
                .GetCookieNamed("NOPCOMMERCE.AUTH");

            return hasNopCookie != null;
        }

        /// <summary>
        /// Used to search for a product.
        /// </summary>
        /// <param name="searchFor">Partial or full name of product.</param>
        /// <returns></returns>
        public virtual SearchPage Search(string searchFor)
        {
            SearchBoxElement.SetValue(searchFor);
            SearchBoxButtonElement.Click();

            var searchPage = new SearchPage(WrappedDriver, PageSettings);
            searchPage.Load();
            return searchPage;
        }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        public BasePage GoToShoppingCart()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
