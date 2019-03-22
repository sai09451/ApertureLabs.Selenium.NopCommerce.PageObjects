using System;
using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The 'Billing and shipping' tab of the order edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.FluidPageComponent{T}" />
    public class OrderDetailsBillingShippingComponent : FluidPageComponent<IEditPage>
    {
        #region Fields

        #region Selectors

        private readonly By billingAddressListSelector = By.CssSelector("div:nth-child(1) > div:nth-child(1) > table");
        private readonly By shippingAddressListSelector = By.CssSelector("div:nth-child(2) > div:nth-child(1) > table");
        private readonly By shippingMethodSelector = By.CssSelector("#lblShippingMethod");
        private readonly By shippingStatusSelector = By.CssSelector("div > div:nth-child(2) > div > div:nth-child(2) > div.col-md-9 > div");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsBillingShippingComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OrderDetailsBillingShippingComponent(By selector,
            IEditPage parent,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector,
                  driver,
                  parent)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            Shipments = new KGridComponent<OrderDetailsBillingShippingComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#shipments-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);
        }


        #endregion

        #region Properties

        #region Elements

        private IWebElement BillingAddressListElement => WrappedElement
            .FindElement(billingAddressListSelector);

        private IWebElement ShippingAddressListElement => WrappedElement
            .FindElement(shippingAddressListSelector);

        private IWebElement ShippingMethodElement => WrappedElement
            .FindElement(shippingMethodSelector);

        private IWebElement ShippingStatusElement => WrappedElement
            .FindElement(shippingStatusSelector);

        #endregion

        /// <summary>
        /// Gets the shipments.
        /// </summary>
        /// <value>
        /// The shipments.
        /// </value>
        public virtual KGridComponent<OrderDetailsBillingShippingComponent> Shipments { get; }

        #endregion

        #region Methods

        /// <summary>
        /// If overloaded don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            Shipments.Load();

            return this;
        }

        /// <summary>
        /// Gets the billing address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual AddressModel GetBillingAddress()
        {
            var model = GetAddressFromListElement(BillingAddressListElement);

            return model;
        }

        /// <summary>
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual AddressModel GetShippingAddress()
        {
            var model = GetAddressFromListElement(ShippingAddressListElement);

            return model;
        }

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual string GetShippingMethod()
        {
            return ShippingMethodElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the shipping status.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual string GetShippingStatus()
        {
            return ShippingStatusElement.TextHelper().InnerText;
        }

        private AddressModel GetAddressFromListElement(IWebElement listElement)
        {
            var fullNameSelector = By.CssSelector("tr:nth-child(1) td:nth-child(2)");
            var emailSelector = By.CssSelector("tr:nth-child(2) td:nth-child(2)");
            var phoneSelector = By.CssSelector("tr:nth-child(3) td:nth-child(2)");
            var faxSelector = By.CssSelector("tr:nth-child(4) td:nth-child(2)");
            var companySelector = By.CssSelector("tr:nth-child(5) td:nth-child(2)");
            var address1Selector = By.CssSelector("tr:nth-child(6) td:nth-child(2)");
            var address2Selector = By.CssSelector("tr:nth-child(7) td:nth-child(2)");
            var citySelector = By.CssSelector("tr:nth-child(8) td:nth-child(2)");
            var stateProvinceSelector = By.CssSelector("tr:nth-child(9) td:nth-child(2)");
            var zipSelector = By.CssSelector("tr:nth-child(10) td:nth-child(2)");
            var countrySelector = By.CssSelector("tr:nth-child(11) td:nth-child(2)");

            string GetTextFor(By selector)
            {
                return WrappedElement.FindElement(selector)
                    .TextHelper()
                    .InnerText;
            }

            var fullName = GetTextFor(fullNameSelector);
            var splitName = fullName.Split(
                separator: new[] { ' ' },
                count: 2,
                options: StringSplitOptions.RemoveEmptyEntries);

            var fName = splitName[0];
            var lName = splitName.Length > 1 ? splitName[1] : null;

            var model = new AddressModel
            {
                Address1 = GetTextFor(address1Selector),
                Address2 = GetTextFor(address2Selector),
                City = GetTextFor(citySelector),
                Company = GetTextFor(companySelector),
                Country = GetTextFor(countrySelector),
                Email = GetTextFor(emailSelector),
                FaxNumber = GetTextFor(faxSelector),
                FirstName = fName,
                LastName = lName,
                PhoneNumber = GetTextFor(phoneSelector),
                StateProvince = GetTextFor(stateProvinceSelector),
                ZipPostalCode = GetTextFor(zipSelector)
            };

            return model;
        }

        #endregion
    }
}
