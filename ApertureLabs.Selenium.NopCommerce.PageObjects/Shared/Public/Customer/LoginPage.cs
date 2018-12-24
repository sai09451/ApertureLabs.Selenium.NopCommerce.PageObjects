using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// LoginPage.
    /// </summary>
    public class LoginPage : BasePage
    {
        #region Fields

        private readonly By emailSelector;
        private readonly By emailValidatorSelector;
        private readonly By passwordSelector;
        private readonly By loginBtnSelector;
        private readonly By messageErrorSelector;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public LoginPage(IWebDriver driver, PageSettings pageSettings)
            : base(driver, pageSettings)
        {
            Uri = new Uri(Uri.ToString() + "login", UriKind.Absolute);
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
        protected IWebElement EmailInputElement => WrappedDriver.FindElement(emailSelector);

        /// <summary>
        /// Email validation element.
        /// </summary>
        protected IWebElement EmailValidationElement => WrappedDriver.FindElement(emailValidatorSelector);

        /// <summary>
        /// Password input element.
        /// </summary>
        protected IWebElement PasswordInputElement => WrappedDriver.FindElement(passwordSelector);

        /// <summary>
        /// Login button element.
        /// </summary>
        protected IWebElement LoginButtonElement => WrappedDriver.FindElement(loginBtnSelector);

        /// <summary>
        /// Message error summary element.
        /// </summary>
        protected IWebElement MessageErrorSummaryElement => WrappedDriver.FindElement(messageErrorSelector);

        /// <summary>
        /// A list of errors on the page.
        /// </summary>
        public IEnumerable<string> Errors
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

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the email is invalid.
        /// </summary>
        /// <returns></returns>
        public bool HasInvalidEmail()
        {
            return String.IsNullOrWhiteSpace(EmailValidationElement.Text);
        }

        /// <summary>
        /// Checks for any errors.
        /// </summary>
        /// <returns></returns>
        public bool HasMessageErrorSummary()
        {
            return WrappedDriver.Select(messageErrorSelector).Any();
        }

        /// <summary>
        /// Will first set the IWebDrivers Url to the login page before calling
        /// base.Load().
        /// </summary>
        /// <param name="navigateToUrl"></param>
        /// <returns></returns>
        public ILoadableComponent Load(bool navigateToUrl)
        {
            if (navigateToUrl)
            {
                WrappedDriver.Navigate().GoToUrl(Uri);
            }

            return base.Load();
        }

        /// <summary>
        /// Enters the users email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public LoginPage EnterEmail(string email)
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
        public LoginPage EnterPassword(string password)
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
        public void ClickLogin(Action<HomePage> success, Action<LoginPage> error)
        {
            LoginButtonElement.Click();

            if (WrappedDriver.Url == Uri.ToString())
            {
                error(this);
            }
            else
            {
                var homePage = new HomePage(WrappedDriver, null);
                homePage.Load();
                success(homePage);
            }
        }

        #endregion
    }
}
