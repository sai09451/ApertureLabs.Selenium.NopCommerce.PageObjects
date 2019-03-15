using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// LoginPage.
    /// </summary>
    public class LoginPage : StaticPageObject, ILoginPage
    {
        #region Fields

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        #region Selectors

        private readonly By emailSelector;
        private readonly By emailValidatorSelector;
        private readonly By passwordSelector;
        private readonly By loginBtnSelector;
        private readonly By messageErrorSelector;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public LoginPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  new Uri(pageSettings.BaseUrl, "login"))
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;

            emailSelector = By.CssSelector(".email");
            emailValidatorSelector = By.CssSelector(".email + .field-validation-error");
            passwordSelector = By.CssSelector(".password");
            loginBtnSelector = By.CssSelector(".login-button");
            messageErrorSelector = By.CssSelector(".validation-summary-errors");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Email input element.
        /// </summary>
        protected virtual IWebElement EmailInputElement => WrappedDriver.FindElement(emailSelector);

        /// <summary>
        /// Email validation element.
        /// </summary>
        protected virtual IWebElement EmailValidationElement => WrappedDriver.FindElement(emailValidatorSelector);

        /// <summary>
        /// Password input element.
        /// </summary>
        protected virtual IWebElement PasswordInputElement => WrappedDriver.FindElement(passwordSelector);

        /// <summary>
        /// Login button element.
        /// </summary>
        protected virtual IWebElement LoginButtonElement => WrappedDriver.FindElement(loginBtnSelector);

        /// <summary>
        /// Message error summary element.
        /// </summary>
        protected virtual IWebElement MessageErrorSummaryElement => WrappedDriver.FindElement(messageErrorSelector);

        /// <summary>
        /// A list of errors on the page.
        /// </summary>
        public virtual IEnumerable<string> Errors
        {
            get
            {
                var errors = new List<string>();

                if (HasInvalidEmail())
                    errors.Add(EmailValidationElement.Text);

                if (HasMessageErrorSummary())
                    errors.Add(MessageErrorSummaryElement.Text);

                return errors;
            }
        }

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public virtual IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

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
        /// Checks if the email is invalid.
        /// </summary>
        /// <returns></returns>
        public virtual bool HasInvalidEmail()
        {
            if (WrappedDriver.FindElements(emailValidatorSelector).Any())
                return String.IsNullOrWhiteSpace(EmailValidationElement.Text);

            return false;
        }

        /// <summary>
        /// Checks for any errors.
        /// </summary>
        /// <returns></returns>
        public virtual bool HasMessageErrorSummary()
        {
            return WrappedDriver.FindElements(messageErrorSelector).Any();
        }

        /// <summary>
        /// Enters the users email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual ILoginPage EnterEmail(string email)
        {
            EmailInputElement.Clear();
            EmailInputElement.SendKeys(email);

            return this;
        }

        /// <summary>
        /// Enters the users password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual ILoginPage EnterPassword(string password)
        {
            PasswordInputElement.Clear();
            PasswordInputElement.SendKeys(password);
            return this;
        }

        /// <summary>
        /// Clicks the login button.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        public virtual void ClickLogin(Action<HomePage> success, Action<LoginPage> error)
        {
            LoginButtonElement.Click();

            if (WrappedDriver.Url == Uri.ToString())
            {
                error(this);
            }
            else
            {
                var homePage = pageObjectFactory.PreparePage<HomePage>();
                success(homePage);
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
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        #endregion
    }
}
