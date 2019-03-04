using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CustomerNavigation;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// ChangePasswordPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer.IChangePasswordPage" />
    public class ChangePasswordPage : StaticPageObject, IChangePasswordPage
    {
        #region Fields

        #region Selectors

        private readonly By oldPasswordSelector = By.CssSelector("#OldPassword");
        private readonly By newPasswordSelector = By.CssSelector("#NewPassword");
        private readonly By confirmPasswordSelector = By.CssSelector("#ConfirmNewPassword");
        private readonly By changePasswordSelector = By.CssSelector(".change-password-button");
        private readonly By errorMessagesSelector = By.CssSelector(".field-validation-error");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <exception cref="ArgumentNullException">
        /// basePage
        /// or
        /// pageSettings
        /// </exception>
        public ChangePasswordPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  new Uri(pageSettings.BaseUrl, "customer/changepassword"))
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));

            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            if (pageSettings == null)
                throw new ArgumentNullException(nameof(pageSettings));

            AccountNavigation = new CustomerNavigationComponent<IChangePasswordPage>(
                pageObjectFactory,
                driver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement OldPasswordElement => new InputElement(
            WrappedDriver.FindElement(
                oldPasswordSelector));

        private InputElement NewPasswordElement => new InputElement(
            WrappedDriver.FindElement(
                newPasswordSelector));

        private InputElement ConfirmPasswordElement => new InputElement(
            WrappedDriver.FindElement(
                confirmPasswordSelector));

        private IWebElement ChangePasswordElement => WrappedDriver
            .FindElement(changePasswordSelector);

        private IReadOnlyCollection<IWebElement> ErrorMessageElements => WrappedDriver
            .FindElements(errorMessagesSelector);

        #endregion

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

        /// <summary>
        /// Gets the account navigation.
        /// </summary>
        /// <value>
        /// The account navigation.
        /// </value>
        public CustomerNavigationComponent<IChangePasswordPage> AccountNavigation { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the component. Checks to see if the current url matches
        /// the Route and if not an exception is thrown. If the WrappedDriver
        /// is an <see cref="T:OpenQA.Selenium.Support.Events.EventFiringWebDriver" /> event listeners will be
        /// added to the <see cref="E:OpenQA.Selenium.Support.Events.EventFiringWebDriver.Navigated" /> event
        /// which will call <see cref="M:ApertureLabs.Selenium.PageObjects.PageObject.Dispose" /> on this instance.
        /// NOTE:
        /// If overriding don't forget to either call base.Load() or make sure
        /// the <see cref="P:ApertureLabs.Selenium.PageObjects.PageObject.Uri" /> and the <see cref="P:ApertureLabs.Selenium.PageObjects.PageObject.WindowHandle" /> are
        /// assigned to.
        /// </summary>
        /// <returns>
        /// A reference to this
        /// <see cref="T:OpenQA.Selenium.Support.UI.ILoadableComponent" />.
        /// </returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            pageObjectFactory.PrepareComponent(AccountNavigation);
            pageObjectFactory.PrepareComponent(basePage);

            return this;
        }

        /// <summary>
        /// Changes the password. Throws an exception if the password fails to
        /// update.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public IChangePasswordPage ChangePassword(string oldPassword,
            string newPassword)
        {
            EnterOldPassword(oldPassword);
            EnterNewPassword(newPassword);
            Save(reject: page => throw new Exception());

            return this;
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Enters the new password.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IChangePasswordPage EnterNewPassword(string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enters the old password.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <returns></returns>
        public IChangePasswordPage EnterOldPassword(string oldPassword)
        {
            OldPasswordElement.SetValue(oldPassword);

            return this;
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
        /// Tries to save the changes. Resolve will be called upon success and
        /// reject will be called upon failure to update the changes.
        /// </summary>
        /// <param name="resolve">Can be null.</param>
        /// <param name="reject">Can be null.</param>
        /// <returns></returns>
        public IChangePasswordPage Save(
            Action<IChangePasswordPage> resolve = null,
            Action<IChangePasswordPage> reject = null)
        {
            ChangePasswordElement.Click();

            // Page should reload.
            Load();

            // Check for errors.
            var hasErrors = ErrorMessageElements.Any(
                e => !String.IsNullOrEmpty(
                    e.TextHelper().InnerText));

            if (hasErrors)
                reject?.Invoke(this);
            else
                resolve?.Invoke(this);

            return this;
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

        #endregion
    }
}
