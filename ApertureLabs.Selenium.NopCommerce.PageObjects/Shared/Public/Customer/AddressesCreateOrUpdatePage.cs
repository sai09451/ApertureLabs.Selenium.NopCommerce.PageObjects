using System;
using System.Collections.Generic;
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
    /// Base class for the AddressesAdd and AddressesEdit PageObjects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer.IAddressesCreateOrUpdatePage{T}" />
    public abstract class AddressesCreateOrUpdatePage<T> : PageObject,
        IAddressesCreateOrUpdatePage<T>
        where T : IPageObject
    {
        #region Fields

        #region Selectors

        private readonly By firstNameSelector = By.CssSelector("#Address_FirstName");
        private readonly By lastNameSelector = By.CssSelector("#Address_LastName");
        private readonly By emailSelector = By.CssSelector("#Address_Email");
        private readonly By companySelector = By.CssSelector("#Address_Company");
        private readonly By countrySelector = By.CssSelector("#Address_CountryId");
        private readonly By stateProvinceSelector = By.CssSelector("#Address_StateProvinceId");
        private readonly By stateProvinceLoadingSelector = By.CssSelector("#states-loading-progress");
        private readonly By citySelector = By.CssSelector("#Address_City");
        private readonly By address1Selector = By.CssSelector("#Address_Address1");
        private readonly By address2Selector = By.CssSelector("#Address_Address2");
        private readonly By zipCodeSelector = By.CssSelector("#Address_ZipPostalCode");
        private readonly By phoneNumberSelector = By.CssSelector("#Address_PhoneNumber");
        private readonly By faxNumberSelector = By.CssSelector("#Address_FaxNumber");
        private readonly By saveSelector = By.CssSelector("#save-address-button");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressesAddPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <param name="template">The template.</param>
        /// <exception cref="ArgumentNullException">
        /// basePage
        /// or
        /// pageObjectFactory
        /// or
        /// pageSettings
        /// </exception>
        public AddressesCreateOrUpdatePage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings,
            UriTemplate template)
            : base(driver,
                  pageSettings.BaseUrl,
                  template)
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            if (pageSettings == null)
                throw new ArgumentNullException(nameof(pageSettings));
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement FirstNameElement => new InputElement(
            WrappedDriver.FindElement(
                firstNameSelector));

        private InputElement LastNameElement => new InputElement(
            WrappedDriver.FindElement(
                lastNameSelector));

        private InputElement EmailElement => new InputElement(
            WrappedDriver.FindElement(
                emailSelector));

        private InputElement CompanyElement => new InputElement(
            WrappedDriver.FindElement(
                companySelector));

        private SelectElement CountryElement => new SelectElement(
            WrappedDriver.FindElement(
                countrySelector));

        private SelectElement StateProvinceElement => new SelectElement(
            WrappedDriver.FindElement(
                stateProvinceSelector));

        private IWebElement StateProvinceLoadingElement => WrappedDriver
            .FindElement(stateProvinceLoadingSelector);

        private InputElement CityElement => new InputElement(
            WrappedDriver.FindElement(
                citySelector));

        private InputElement Address1Element => new InputElement(
            WrappedDriver.FindElement(
                address1Selector));

        private InputElement Address2Element => new InputElement(
            WrappedDriver.FindElement(
                address2Selector));

        private InputElement ZipCodeElement => new InputElement(
            WrappedDriver.FindElement(
                zipCodeSelector));

        private InputElement PhoneNumberElement => new InputElement(
            WrappedDriver.FindElement(
                phoneNumberSelector));

        private InputElement FaxNumberElement => new InputElement(
            WrappedDriver.FindElement(
                faxNumberSelector));

        private IWebElement SaveElement => WrappedDriver
            .FindElement(saveSelector);

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
        public CustomerNavigationComponent<T> AccountNavigation { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Gets the address1.
        /// </summary>
        /// <returns></returns>
        public string GetAddress1()
        {
            return Address1Element.GetValue<string>();
        }

        /// <summary>
        /// Gets the address2.
        /// </summary>
        /// <returns></returns>
        public string GetAddress2()
        {
            return Address2Element.GetValue<string>();
        }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <returns></returns>
        public string GetCity()
        {
            return CityElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the company.
        /// </summary>
        /// <returns></returns>
        public string GetCompany()
        {
            return CompanyElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <returns></returns>
        public string GetCountry()
        {
            return CountryElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        public string GetEmail()
        {
            return EmailElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the fax number.
        /// </summary>
        /// <returns></returns>
        public string GetFaxNumber()
        {
            return FaxNumberElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns></returns>
        public string GetFirstName()
        {
            return FirstNameElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <returns></returns>
        public string GetLastName()
        {
            return LastNameElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <returns></returns>
        public string GetPhoneNumber()
        {
            return PhoneNumberElement.GetValue<string>();
        }

        /// <summary>
        /// Gets the state province.
        /// </summary>
        /// <returns></returns>
        public string GetStateProvince()
        {
            return StateProvinceElement.SelectedOption
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets the zip code.
        /// </summary>
        /// <returns></returns>
        public string GetZipCode()
        {
            return ZipCodeElement.GetValue<string>();
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
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        public U Logout<U>() where U : IPageObject
        {
            return basePage.Logout<U>();
        }

        /// <summary>
        /// Saves the address.
        /// </summary>
        /// <returns></returns>
        public IAddressesPage Save()
        {
            SaveElement.Click();

            // TODO: Handle error scenario.
            return pageObjectFactory.PreparePage<IAddressesPage>();
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

        /// <summary>
        /// Sets the address1.
        /// </summary>
        /// <param name="address1">The address1.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetAddress1(string address1)
        {
            Address1Element.SetValue(address1);

            return this;
        }

        /// <summary>
        /// Sets the address2.
        /// </summary>
        /// <param name="address2">The address2.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetAddress2(string address2)
        {
            Address2Element.SetValue(address2);

            return this;
        }

        /// <summary>
        /// Sets the city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetCity(string city)
        {
            CityElement.SetValue(city);

            return this;
        }

        /// <summary>
        /// Sets the company.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetCompany(string company)
        {
            CompanyElement.SetValue(company);

            return this;
        }

        /// <summary>
        /// Sets the country.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetCountry(string country)
        {
            CountryElement.SelectByText(country);

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(30))
                .TrySequentialWait(
                    out var exc,
                    d => StateProvinceLoadingElement.Displayed,
                    d => !StateProvinceLoadingElement.Displayed);

            return this;
        }

        /// <summary>
        /// Sets the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetEmail(string email)
        {
            EmailElement.SetValue(email);

            return this;
        }

        /// <summary>
        /// Sets the fax number.
        /// </summary>
        /// <param name="faxNumber">The fax number.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetFaxNumber(string faxNumber)
        {
            FaxNumberElement.SetValue(faxNumber);

            return this;
        }

        /// <summary>
        /// Sets the first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetFirstName(string firstName)
        {
            FirstNameElement.SetValue(firstName);

            return this;
        }

        /// <summary>
        /// Sets from model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">model</exception>
        public IAddressesCreateOrUpdatePage<T> SetFromModel(AddressModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return SetFirstName(model.FirstName)
                .SetLastName(model.LastName)
                .SetEmail(model.Email)
                .SetCompany(model.Company)
                .SetCountry(model.Country)
                .SetStateProvince(model.StateProvince)
                .SetCity(model.City)
                .SetAddress1(model.Address1)
                .SetAddress2(model.Address2)
                .SetZipCode(model.ZipPostalCode)
                .SetPhoneNumber(model.PhoneNumber)
                .SetFaxNumber(model.FaxNumber);
        }

        /// <summary>
        /// Sets the last name.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetLastName(string lastName)
        {
            LastNameElement.SetValue(lastName);

            return this;
        }

        /// <summary>
        /// Sets the phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetPhoneNumber(string phoneNumber)
        {
            PhoneNumberElement.SetValue(phoneNumber);

            return this;
        }

        /// <summary>
        /// Sets the state province.
        /// </summary>
        /// <param name="stateProvince">The state province.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetStateProvince(string stateProvince)
        {
            StateProvinceElement.SelectByText(stateProvince);

            return this;
        }

        /// <summary>
        /// Sets the zip code.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns></returns>
        public IAddressesCreateOrUpdatePage<T> SetZipCode(string zipCode)
        {
            ZipCodeElement.SetValue(zipCode);

            return this;
        }

        /// <summary>
        /// Retrieves the viewmodel.
        /// </summary>
        /// <returns></returns>
        public AddressModel ViewModel()
        {
            var model = new AddressModel
            {
                FirstName = GetFirstName(),
                LastName = GetLastName(),
                Email = GetEmail(),
                Company = GetCompany(),
                Country = GetCountry(),
                StateProvince = GetStateProvince(),
                City = GetCity(),
                Address1 = GetAddress1(),
                Address2 = GetAddress2(),
                ZipPostalCode = GetZipCode(),
                PhoneNumber = GetPhoneNumber(),
                FaxNumber = GetFaxNumber()
            };

            return model;
        }

        #endregion
    }
}
