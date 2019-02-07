using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// AddressRowComponent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.FluidPageComponent{T}" />
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IViewModel{T}" />
    public class AddressesRowComponent<T> : FluidPageComponent<T>,
        IViewModel<AddressModel>
    {
        #region Fields

        #region Selectors

        private readonly By deleteSelector = By.CssSelector(".delete-address-button");
        private readonly By editSelector = By.CssSelector(".edit-address-button");
        private readonly By nameSelector = By.CssSelector(".name");
        private readonly By emailSelector = By.CssSelector("");
        private readonly By phoneSelector = By.CssSelector("");
        private readonly By faxSelector = By.CssSelector("");
        private readonly By companySelector = By.CssSelector("");
        private readonly By address1Selector = By.CssSelector("");
        private readonly By cityStateZipSelector = By.CssSelector("");
        private readonly By countrySelector = By.CssSelector("");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressesRowComponent{T}"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="parent">The parent.</param>
        /// <exception cref="ArgumentNullException">pageObjectFactory</exception>
        public AddressesRowComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            T parent)
            : base(selector, driver, parent)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement EditElement => WrappedElement.FindElement(editSelector);
        private IWebElement DeleteElement => WrappedElement.FindElement(deleteSelector);
        private IWebElement NameElement => WrappedElement.FindElement(nameSelector);
        private IWebElement EmailElement => WrappedElement.FindElement(emailSelector);
        private IWebElement PhoneElement => WrappedElement.FindElement(phoneSelector);
        private IWebElement FaxElement => WrappedElement.FindElement(faxSelector);
        private IWebElement CompnayElement => WrappedElement.FindElement(companySelector);
        private IWebElement Address1Element => WrappedElement.FindElement(address1Selector);
        private IWebElement CityStateZipElement => WrappedElement.FindElement(cityStateZipSelector);
        private IWebElement CountryElement => WrappedElement.FindElement(countrySelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns></returns>
        public virtual T Delete()
        {
            DeleteElement.Click();
            WrappedDriver
                .SwitchTo()
                .Alert()
                .Accept();

            return Parent();
        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        /// <returns></returns>
        public virtual IAddressesEditPage Edit()
        {
            EditElement.Click();

            return pageObjectFactory.PreparePage<IAddressesEditPage>();
        }

        /// <summary>
        /// Retrieves the viewmodel.
        /// </summary>
        /// <returns></returns>
        public virtual AddressModel ViewModel()
        {
            var match = Regex.Match(
                CityStateZipElement.TextHelper().InnerText,
                @"(.*?),\s(.*?),\s(.*)");

            var city = match.Groups[1].Value;
            var state = match.Groups[2].Value;
            var zip = match.Groups[3].Value;

            return new AddressModel
            {
                Email = RightOfSemiColon(EmailElement.TextHelper().InnerText),
                PhoneNumber = RightOfSemiColon(PhoneElement.TextHelper().InnerText),
                FaxNumber = RightOfSemiColon(FaxElement.TextHelper().InnerText),
                Company = CompnayElement.TextHelper().InnerText,
                Address1 = Address1Element.TextHelper().InnerText,

                Country = CountryElement.TextHelper().InnerText
            };
        }

        private string RightOfSemiColon(string text)
        {
            return Regex.Replace(text, @".*:\s?", "");
        }

        #endregion
    }
}
