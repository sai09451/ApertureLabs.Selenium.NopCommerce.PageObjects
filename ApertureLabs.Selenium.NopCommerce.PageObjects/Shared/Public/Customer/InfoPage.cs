using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// InfoPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer.IInfoPage" />
    public class InfoPage : StaticPageObject, IInfoPage
    {
        #region Fields

        #region Selectors

        private readonly By companyNameSelector = By.CssSelector("#Company");
        private readonly By dateOfBirthDaySelector = By.CssSelector(".date-of-birth *[name='DateOfBirthDay']");
        private readonly By dateOfBirthMonthSelector = By.CssSelector(".date-of-birth *[name='DateOfBirthMonth']");
        private readonly By dateOfBirthYearSelector = By.CssSelector(".date-of-birth *[name='DateOfBirthYear']");
        private readonly By emailSelector = By.CssSelector("#Email");
        private readonly By firstNameSelector = By.CssSelector("#FirstName");
        private readonly By allGendersSelector = By.CssSelector(".gender input");
        private readonly By registeredForNewsLetterSelector = By.CssSelector("#Newsletter");
        private readonly By lastNameSelector = By.CssSelector("#LastName");
        private readonly By saveSelector = By.CssSelector("#save-info-button");

        #endregion

        private readonly IBasePage basePage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public InfoPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  new Uri(pageSettings.BaseUrl, "customer/info"))
        {
            if (pageSettings == null)
                throw new ArgumentNullException(nameof(pageSettings));

            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));

            AccountNavigation = new CustomerNavigationComponent<IInfoPage>(
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement CompanyNameElement => new InputElement(WrappedDriver.FindElement(companyNameSelector));
        private SelectElement DateOfBirthDayElement => new SelectElement(WrappedDriver.FindElement(dateOfBirthDaySelector));
        private SelectElement DateOfBirthMonthElement => new SelectElement(WrappedDriver.FindElement(dateOfBirthMonthSelector));
        private SelectElement DateOfBirthYearElement => new SelectElement(WrappedDriver.FindElement(dateOfBirthYearSelector));
        private InputElement EmailElement => new InputElement(WrappedDriver.FindElement(emailSelector));
        private InputElement FirstNameElement => new InputElement(WrappedDriver.FindElement(firstNameSelector));
        private RadioElementSet GenderRadioElement => new RadioElementSet(WrappedDriver.FindElements(allGendersSelector));
        private CheckboxElement RegisteredForNewsLetterElement => new CheckboxElement(WrappedDriver.FindElement(registeredForNewsLetterSelector));
        private InputElement LastNameElement => new InputElement(WrappedDriver.FindElement(lastNameSelector));
        private IWebElement SaveElement => WrappedDriver.FindElement(saveSelector);

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
        public CustomerNavigationComponent<IInfoPage> AccountNavigation { get; private set; }

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
            AccountNavigation.Load();

            return this;
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <returns></returns>
        public virtual string GetCompanyName()
        {
            return CompanyNameElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the date of birth.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime? GetDateOfBirth()
        {
            var result = default(DateTime?);

            var day = int.Parse(DateOfBirthDayElement
                .SelectedOption
                .GetElementProperty("value"));

            var month = int.Parse(DateOfBirthDayElement
                .SelectedOption
                .GetElementProperty("value"));

            var year = int.Parse(DateOfBirthYearElement
                .SelectedOption
                .GetElementProperty("value"));

            if (day != 0 && month != 0 && year != 0)
                result = new DateTime(year, month, day);

            return result;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        public virtual string GetEmail()
        {
            return EmailElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns></returns>
        public virtual string GetFirstName()
        {
            return FirstNameElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        /// <returns></returns>
        public virtual string GetGender()
        {
            return GenderRadioElement.SelectedOption
                ?.WrappedElement
                .GetParentElement()
                .Classes()
                .First();
        }

        /// <summary>
        /// Gets the is registered for news letter.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsRegisteredForNewsLetter()
        {
            return RegisteredForNewsLetterElement.IsChecked;
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <returns></returns>
        public virtual string GetLastName()
        {
            return LastNameElement.GetValue<string>();
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
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public virtual IInfoPage Save()
        {
            SaveElement.Click();

            // Page should reload.
            Load();

            return this;
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
        /// Sets the name of the company.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        public virtual IInfoPage SetCompanyName(string companyName)
        {
            CompanyNameElement.SetValue(companyName);

            return this;
        }

        /// <summary>
        /// Sets the date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns></returns>
        public virtual IInfoPage SetDateOfBirth(DateTime? dateOfBirth)
        {
            var day = (dateOfBirth?.Day ?? 0).ToString();
            var month = (dateOfBirth?.Month ?? 0).ToString();
            var year = (dateOfBirth?.Year ?? 0).ToString();

            DateOfBirthDayElement.SelectByValue(day);
            DateOfBirthMonthElement.SelectByValue(month);
            DateOfBirthYearElement.SelectByValue(year);

            return this;
        }

        /// <summary>
        /// Sets the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public virtual IInfoPage SetEmail(string email)
        {
            EmailElement.SetValue(email);

            return this;
        }

        /// <summary>
        /// Sets the first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <returns></returns>
        public virtual IInfoPage SetFirstName(string firstName)
        {
            FirstNameElement.SetValue(firstName);

            return this;
        }

        /// <summary>
        /// Sets the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual IInfoPage SetGender(string gender,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            // This cannot be null unlike the others as there is no option to
            // deselect the gender.
            if (String.IsNullOrEmpty(gender))
                throw new ArgumentNullException(nameof(gender));

            var opt = GenderRadioElement
                .Options
                .FirstOrDefault(
                    e => e.GetParentElement()
                        .Classes()
                        .First()
                        .Equals(gender, stringComparison));

            if (opt == null)
                throw new NoSuchElementException();

            opt.Click();

            return this;
        }

        /// <summary>
        /// Sets the is registered for news letter.
        /// </summary>
        /// <param name="isRegistered">if set to <c>true</c> [is registered].</param>
        /// <returns></returns>
        public virtual IInfoPage SetIsRegisteredForNewsLetter(bool isRegistered)
        {
            RegisteredForNewsLetterElement.Check(isRegistered);

            return this;
        }

        /// <summary>
        /// Sets the last name.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        public virtual IInfoPage SetLastName(string lastName)
        {
            LastNameElement.SetValue(lastName);

            return this;
        }

        #endregion
    }
}
