using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// OnePageCheckoutPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout.ICheckoutPage" />
    public class OnePageCheckoutPage : PageObject, ICheckoutStepPage
    {
        #region Fields

        #region Selectors

        private readonly By tabNamesSelector = By.CssSelector(".tab-section .step-title .title");
        private readonly By activeTabNumberSelector = By.CssSelector(".tab-section.allow");
        private readonly By activeTabTitleSelector = By.CssSelector(".tab-section.allow .step-title .title");

        private readonly By billingAddressDropDownSelector = By.CssSelector("#billing-address-select");
        private readonly By shipToSameAddressSelector = By.CssSelector("#ShipToSameAddress");
        private readonly By billingAddressFirstNameSelector = By.CssSelector("#BillingNewAddress_FirstName");
        private readonly By billingAddressLastNameSelector = By.CssSelector("#BillingNewAddress_LastName");
        private readonly By billingAddressEmailSelector = By.CssSelector("#BillingNewAddress_Email");
        private readonly By billingAddressCompanySelector = By.CssSelector("#BillingNewAddress_Company");
        private readonly By billingAddressCountrySelector = By.CssSelector("#BillingNewAddress_CountryId");
        private readonly By billingAddressStateProvinceLoadingSelector = By.CssSelector("#billing-new-address-form #states-loading-progress");
        private readonly By billingAddressStateProvinceSelector = By.CssSelector("#BillingNewAddress_StateProvinceId");
        private readonly By billingAddressCitySelector = By.CssSelector("#BillingNewAddress_City");
        private readonly By billingAddressAddress1Selector = By.CssSelector("#BillingNewAddress_Address1");
        private readonly By billingAddressAddress2Selector = By.CssSelector("#BillingNewAddress_Address2");
        private readonly By billingAddressZipCodeSelector = By.CssSelector("#BillingNewAddress_ZipPostalCode");
        private readonly By billingAddressPhoneNumberSelector = By.CssSelector("#BillingNewAddress_PhoneNumber");
        private readonly By billingAddressFaxNumberSelector = By.CssSelector("#BillingNewAddress_FaxNumber");
        private readonly By billingAddressNextStepSelector = By.CssSelector("#billing-buttons-container .new-address-next-step-button");
        private readonly By billingAddressPleaseWaitSelector = By.CssSelector("#billing-please-wait");

        private readonly By shippingAddressPickupInStoreSelector = By.CssSelector("#PickUpInStore");
        private readonly By shippingAddressDropDownSelector = By.CssSelector("#shipping-address-select");
        private readonly By shippingAddressFirstNameSelector = By.CssSelector("#ShippingNewAddress_FirstName");
        private readonly By shippingAddressLastNameSelector = By.CssSelector("#ShippingNewAddress_LastName");
        private readonly By shippingAddressEmailSelector = By.CssSelector("#ShippingNewAddress_Email");
        private readonly By shippingAddressCompanySelector = By.CssSelector("#ShippingNewAddress_Company");
        private readonly By shippingAddressCountrySelector = By.CssSelector("#ShippingNewAddress_CountryId");
        private readonly By shippingAddressStateProvinceLoadingSelector = By.CssSelector("#shipping-new-address-form #states-loading-progress");
        private readonly By shippingAddressStateProvinceSelector = By.CssSelector("#ShippingNewAddress_StateProvinceId");
        private readonly By shippingAddressCitySelector = By.CssSelector("#ShippingNewAddress_City");
        private readonly By shippingAddressAddress1Selector = By.CssSelector("#ShippingNewAddress_Address1");
        private readonly By shippingAddressAddress2Selector = By.CssSelector("#ShippingNewAddress_Address2");
        private readonly By shippingAddressZipCodeSelector = By.CssSelector("#ShippingNewAddress_ZipPostalCode");
        private readonly By shippingAddressPhoneNumberSelector = By.CssSelector("#ShippingNewAddress_PhoneNumber");
        private readonly By shippingAddressFaxNumberSelector = By.CssSelector("#ShippingNewAddress_FaxNumber");
        private readonly By shippingAddressNextStepSelector = By.CssSelector("#shipping-buttons-container .new-address-next-step-button");
        private readonly By shippingAddressPleaseWaitSelector = By.CssSelector("#shipping-please-wait");

        private readonly By shippingMethodRadiosSelector = By.CssSelector(".shipping-method .method-list input[type='radio']");
        private readonly By shippingMethodNamesSelector = By.CssSelector(".shipping-method .method-list .method-name label");
        private readonly By shippingMethodSelectedSelector = By.CssSelector(".shipping-method .method-list .method-name input[type='radio']:checked + label");
        private readonly By shippingMethodNextStepSelector = By.CssSelector("#shipping-method-buttons-container .shipping-method-next-step-button");
        private readonly By shippingMethodPleaseWaitSelector = By.CssSelector("#shipping-method-please-wait");

        private readonly By paymentMethodRadioSelector = By.CssSelector("#payment-method-block .payment-details input[type='radio']");
        private readonly By paymentMethodNamesSelector = By.CssSelector("#payment-method-block .payment-details label");
        private readonly By paymentMethodSelectedSelector = By.CssSelector(".payment-method .method-list .method-name input[type='radio']:checked + label");
        private readonly By paymentMethodNextStepSelector = By.CssSelector("#payment-method-buttons-container .payment-method-next-step-button");
        private readonly By paymentMethodPleaseWaitSelector = By.CssSelector("#payment-method-please-wait");

        private readonly By paymentInformationContainerSelector = By.CssSelector("#checkout-payment-info-load");
        private readonly By paymentInformationNextStepSelector = By.CssSelector("#payment-info-buttons-container .payment-info-next-step-button");
        private readonly By paymentInformationPleaseWaitSelector = By.CssSelector("#payment-info-please-wait");

        private readonly By confirmSelector = By.CssSelector("#confirm-order-buttons-container > button");
        private readonly By confirmPleaseWaitSelector = By.CssSelector("#confirm-order-please-wait");
        private readonly By messageErrorSelector = By.CssSelector(".message-error");

        #endregion

        private readonly IBasePage basePage;
        private readonly IEnumerable<IPaymentMethodHandler> paymentMethodHandlers;
        private readonly IPageObjectFactory pageObjectFactory;

        private string paymentMethodName = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OnePageCheckoutPage"/>
        /// class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="paymentMethodHandlers">
        /// The payment method handlers.
        /// </param>
        /// <param name="driver">The driver.</param>
        public OnePageCheckoutPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IEnumerable<IPaymentMethodHandler> paymentMethodHandlers,
            IWebDriver driver)
            : base(driver)
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
            this.paymentMethodHandlers = paymentMethodHandlers;
        }

        #endregion

        #region Properties

        #region Elements

        private IReadOnlyCollection<IWebElement> TabNameElements => WrappedDriver.FindElements(tabNamesSelector);
        private IWebElement ActiveTabNumberElement => WrappedDriver.FindElements(activeTabNumberSelector).Last();
        private IWebElement ActiveTabTitleElement => WrappedDriver.FindElements(activeTabTitleSelector).Last();

        private CheckboxElement ShipToSameAddressElement => new CheckboxElement(WrappedDriver.FindElement(shipToSameAddressSelector));
        private SelectElement BillingAddressDropDownElement => new SelectElement(WrappedDriver.FindElement(billingAddressDropDownSelector));
        private InputElement BillingAddressFirstNameElement => new InputElement(WrappedDriver.FindElement(billingAddressFirstNameSelector));
        private InputElement BillingAddressLastNameElement => new InputElement(WrappedDriver.FindElement(billingAddressLastNameSelector));
        private InputElement BillingAddressEmailElement => new InputElement(WrappedDriver.FindElement(billingAddressEmailSelector));
        private InputElement BillingAddressCompanyElement => new InputElement(WrappedDriver.FindElement(billingAddressCompanySelector));
        private SelectElement BillingAddressCountryElement => new SelectElement(WrappedDriver.FindElement(billingAddressCountrySelector));
        private IWebElement BillingAddressStateProvinceLoadingElement => WrappedDriver.FindElement(billingAddressStateProvinceLoadingSelector);
        private SelectElement BillingAddressStateProvinceElement => new SelectElement(WrappedDriver.FindElement(billingAddressStateProvinceSelector));
        private InputElement BillingAddressCityElement => new InputElement(WrappedDriver.FindElement(billingAddressCitySelector));
        private InputElement BillingAddressAddress1Element => new InputElement(WrappedDriver.FindElement(billingAddressAddress1Selector));
        private InputElement BillingAddressAddress2Element => new InputElement(WrappedDriver.FindElement(billingAddressAddress2Selector));
        private InputElement BillingAddressZipCodeElement => new InputElement(WrappedDriver.FindElement(billingAddressZipCodeSelector));
        private InputElement BillingAddressPhoneNumberElement => new InputElement(WrappedDriver.FindElement(billingAddressPhoneNumberSelector));
        private InputElement BillingAddressFaxNumberElement => new InputElement(WrappedDriver.FindElement(billingAddressFaxNumberSelector));
        private IWebElement BillingAddressNextStepElement => WrappedDriver.FindElements(billingAddressNextStepSelector).FirstOrDefault();
        private IWebElement BillingAddressPleaseWaitElement => WrappedDriver.FindElement(billingAddressPleaseWaitSelector);

        private CheckboxElement PickupInStoreElement => new CheckboxElement(WrappedDriver.FindElement(shippingAddressPickupInStoreSelector));
        private SelectElement ShippingAddressDropDownElement => new SelectElement(WrappedDriver.FindElement(shippingAddressDropDownSelector));
        private InputElement ShippingAddressFirstNameElement => new InputElement(WrappedDriver.FindElement(shippingAddressFirstNameSelector));
        private InputElement ShippingAddressLastNameElement => new InputElement(WrappedDriver.FindElement(shippingAddressLastNameSelector));
        private InputElement ShippingAddressEmailElement => new InputElement(WrappedDriver.FindElement(shippingAddressEmailSelector));
        private InputElement ShippingAddressCompanyElement => new InputElement(WrappedDriver.FindElement(shippingAddressCompanySelector));
        private SelectElement ShippingAddressCountryElement => new SelectElement(WrappedDriver.FindElement(shippingAddressCountrySelector));
        private IWebElement ShippingAddressStateProvinceLoadingElement => WrappedDriver.FindElement(shippingAddressStateProvinceLoadingSelector);
        private SelectElement ShippingAddressStateProvinceElement => new SelectElement(WrappedDriver.FindElement(shippingAddressStateProvinceSelector));
        private InputElement ShippingAddressCityElement => new InputElement(WrappedDriver.FindElement(shippingAddressCitySelector));
        private InputElement ShippingAddressAddress1Element => new InputElement(WrappedDriver.FindElement(shippingAddressAddress1Selector));
        private InputElement ShippingAddressAddress2Element => new InputElement(WrappedDriver.FindElement(shippingAddressAddress2Selector));
        private InputElement ShippingAddressZipCodeElement => new InputElement(WrappedDriver.FindElement(shippingAddressZipCodeSelector));
        private InputElement ShippingAddressPhoneNumberElement => new InputElement(WrappedDriver.FindElement(shippingAddressPhoneNumberSelector));
        private InputElement ShippingAddressFaxNumberElement => new InputElement(WrappedDriver.FindElement(shippingAddressFaxNumberSelector));
        private IWebElement ShippingAddressNextStepElement => WrappedDriver.FindElements(shippingAddressNextStepSelector).FirstOrDefault();
        private IWebElement ShippingAddressPleaseWaitElement => WrappedDriver.FindElement(shippingAddressPleaseWaitSelector);

        private IReadOnlyCollection<IWebElement> ShippingMethodRadioElements => WrappedDriver.FindElements(shippingMethodRadiosSelector);
        private IReadOnlyCollection<IWebElement> ShippingMethodNameElements => WrappedDriver.FindElements(shippingMethodNamesSelector);
        private IWebElement ShippingMethodSelectedNameElement => WrappedDriver.FindElement(shippingMethodSelectedSelector);
        private IWebElement ShippingMethodNextStepElement => WrappedDriver.FindElements(shippingMethodNextStepSelector).FirstOrDefault();
        private IWebElement ShippingMethodPleaseWaitElement => WrappedDriver.FindElement(shippingMethodPleaseWaitSelector);

        private IReadOnlyCollection<IWebElement> PaymentMethodRadioElements => WrappedDriver.FindElements(paymentMethodRadioSelector);
        private IReadOnlyCollection<IWebElement> PaymentMethodNameElements => WrappedDriver.FindElements(paymentMethodNamesSelector);
        private IWebElement PaymentMethodSelectedNameElement => WrappedDriver.FindElement(paymentMethodSelectedSelector);
        private IWebElement PaymentMethodNextStepElement => WrappedDriver.FindElements(paymentMethodNextStepSelector).FirstOrDefault();
        private IWebElement PaymentMethodPleaseWaitElement => WrappedDriver.FindElement(paymentMethodPleaseWaitSelector);

        private IWebElement PaymentInformationContainerElement => WrappedDriver.FindElement(paymentInformationContainerSelector);
        private IWebElement PaymentInformationNextStepElement => WrappedDriver.FindElements(paymentInformationNextStepSelector).FirstOrDefault();
        private IWebElement PaymentInformationPleaseWaitElement => WrappedDriver.FindElement(paymentInformationPleaseWaitSelector);

        private IWebElement ConfirmElement => WrappedDriver.FindElement(confirmSelector);
        private IWebElement ConfirmPleaseWaitElement => WrappedDriver.FindElements(confirmPleaseWaitSelector).FirstOrDefault();
        private IReadOnlyCollection<IWebElement> MessageErrorElements => WrappedDriver.FindElements(messageErrorSelector);

        #endregion

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

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
        /// Finalizes and confirms the order.
        /// </summary>
        /// <returns></returns>
        public ICompletedPage Confirm()
        {
            var page = default(ICompletedPage);
            TryConfirm(
                completedPage => page = completedPage,
                checkoutPage => throw new Exception("Failed to checkout"));

            return page;
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Enters the billing address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="shipToSameAddress">Whether or not to ship to the same address.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void EnterBillingAddress(AddressModel address,
            bool shipToSameAddress = true)
        {
            if (GetCurrentStepName() != "Billing address")
                throw new Exception("Not on the correct step.");

            ShipToSameAddressElement.Check(shipToSameAddress);

            // Check if an existing address is selected.
            if (HasExistingBillingAddresses())
            {
                // Select the new address option.
                BillingAddressDropDownElement.SelectByText("New Address");
            }

            // First name.
            BillingAddressFirstNameElement.SetValue(address.FirstName);

            // Last name.
            BillingAddressLastNameElement.SetValue(address.LastName);

            // Email.
            BillingAddressEmailElement.SetValue(address.Email);

            // Company.
            BillingAddressCompanyElement.SetValue(address.Company);

            // Country.
            if (!String.Equals(
                BillingAddressCountryElement.SelectedOption.TextHelper().InnerText,
                address.Country,
                StringComparison.Ordinal))
            {
                BillingAddressCountryElement.SelectByText(address.Country);
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(30))
                    .TrySequentialWait(
                        out var exc,
                        d => BillingAddressStateProvinceLoadingElement.Displayed,
                        d => !BillingAddressStateProvinceLoadingElement.Displayed);
            }

            // State/province.
            BillingAddressStateProvinceElement
                .SelectByText(address.StateProvince);

            // City.
            BillingAddressCityElement.SetValue(address.City);

            // Address 1 & 2.
            BillingAddressAddress1Element.SetValue(address.Address1);
            BillingAddressAddress2Element.SetValue(address.Address2);

            // Zipcode.
            BillingAddressZipCodeElement.SetValue(address.ZipPostalCode);

            // Phone number.
            BillingAddressPhoneNumberElement.SetValue(address.PhoneNumber);

            // Fax.
            BillingAddressFaxNumberElement.SetValue(address.FaxNumber);
        }

        /// <summary>
        /// Enters the payment information.
        /// </summary>
        /// <param name="containerElement">The element containing the payment information details.</param>
        /// <exception cref="Exception">
        /// Couldn't determine payment method name.
        /// or
        /// No payment handler registered that can " +
        ///                     $"operate on {paymentMethodName}
        /// </exception>
        public void EnterPaymentInformation(IWebElement containerElement)
        {
            if (String.IsNullOrEmpty(paymentMethodName))
                throw new Exception("Couldn't determine payment method name.");

            var handler = paymentMethodHandlers
                .FirstOrDefault(p => p.CanOperateOn(paymentMethodName));

            if (handler == null)
            {
                throw new Exception("No payment handler registered that can " +
                    $"operate on {paymentMethodName}");
            }

            handler.EnterInformation(PaymentInformationContainerElement);
        }

        /// <summary>
        /// Enters the shipping address.
        /// </summary>
        /// <param name="address">The address.</param>
        public void EnterShippingAddress(AddressModel address)
        {
            if (GetCurrentStepName() != "Shipping address")
                throw new Exception("Not on correct step.");

            // Check if an existing address is selected.
            if (HasExistingShippingAddresses())
            {
                // Select the new address option.
                ShippingAddressDropDownElement.SelectByText("New Address");
            }
            
            // First name.
            ShippingAddressFirstNameElement.SetValue(address.FirstName);

            // Last name.
            ShippingAddressLastNameElement.SetValue(address.LastName);

            // Email.
            ShippingAddressEmailElement.SetValue(address.Email);

            // Company.
            ShippingAddressCompanyElement.SetValue(address.Company);

            // Country.
            if (!String.Equals(
                ShippingAddressDropDownElement.SelectedOption.TextHelper().InnerText,
                address.Country,
                StringComparison.Ordinal))
            {
                ShippingAddressDropDownElement.SelectByText(address.Country);
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(30))
                    .TrySequentialWait(
                        out var exc,
                        d => ShippingAddressPleaseWaitElement.Displayed,
                        d => !ShippingAddressPleaseWaitElement.Displayed);
            }

            // State/province.
            ShippingAddressStateProvinceElement
                .SelectByText(address.StateProvince);

            // City.
            ShippingAddressCityElement.SetValue(address.City);

            // Address 1 & 2.
            ShippingAddressAddress1Element.SetValue(address.Address1);
            ShippingAddressAddress2Element.SetValue(address.Address2);

            // Zipcode.
            ShippingAddressZipCodeElement.SetValue(address.ZipPostalCode);

            // Phone number.
            ShippingAddressPhoneNumberElement.SetValue(address.PhoneNumber);

            // Fax.
            ShippingAddressFaxNumberElement.SetValue(address.FaxNumber);
        }

        /// <summary>
        /// Gets the billing address.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public AddressModel GetBillingAddress()
        {
            if (GetCurrentStepName() != "Billing address")
                throw new Exception("Not on the correct step");

            var model = new AddressModel();

            // Check if the using a pre-existing address or a new address.
            // 1) Using a pre-existing option.
            // 2) Using a new address.

            if (HasExistingBillingAddresses()
                && !String.IsNullOrEmpty(
                    BillingAddressDropDownElement
                        .SelectedOption
                        .GetElementProperty("value")))
            {
                var str = BillingAddressDropDownElement
                    .SelectedOption
                    .TextHelper()
                    .InnerText;

                // Is an existing address.
                model = GetAddressFromExistingAddress(str);
            }
            else
            {
                // Is a new address.
                model = GetAddressFromNewAddress(
                    BillingAddressFirstNameElement,
                    BillingAddressLastNameElement,
                    BillingAddressEmailElement,
                    BillingAddressCompanyElement,
                    BillingAddressCountryElement,
                    BillingAddressStateProvinceElement,
                    BillingAddressCityElement,
                    BillingAddressAddress1Element,
                    BillingAddressAddress2Element,
                    BillingAddressZipCodeElement,
                    BillingAddressPhoneNumberElement,
                    BillingAddressFaxNumberElement);
            }

            return model;
        }

        /// <summary>
        /// Gets the current step.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentStep()
        {
            return ActiveTabNumberElement.GetIndexRelativeToSiblings();
        }

        /// <summary>
        /// Gets the name of the current step.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStepName()
        {
            return ActiveTabTitleElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the names of payment methods listed on the page.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedPaymentMethod()
        {
            if (GetCurrentStepName() != "Payment method")
                throw new Exception("Not on correct step.");

            return PaymentMethodSelectedNameElement
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        public AddressModel GetShippingAddress()
        {
            if (GetCurrentStepName() != "Shipping address")
                throw new Exception("Not on the correct step.");

            var model = new AddressModel();

            // Check if the current address is an existing address or a new
            // address.
            if (HasExistingShippingAddresses()
                && !String.IsNullOrEmpty(
                    ShippingAddressDropDownElement
                        .SelectedOption
                        .GetElementProperty("value")))
            {
                // Is existing address.
                var str = ShippingAddressDropDownElement
                    .SelectedOption
                    .TextHelper()
                    .InnerText;

                model = GetAddressFromExistingAddress(str);
            }
            else
            {
                // Is new address.
                model = GetAddressFromNewAddress(
                    ShippingAddressFirstNameElement,
                    ShippingAddressLastNameElement,
                    ShippingAddressEmailElement,
                    ShippingAddressCompanyElement,
                    ShippingAddressCountryElement,
                    ShippingAddressStateProvinceElement,
                    ShippingAddressCityElement,
                    ShippingAddressAddress1Element,
                    ShippingAddressAddress2Element,
                    ShippingAddressZipCodeElement,
                    ShippingAddressPhoneNumberElement,
                    ShippingAddressFaxNumberElement);
            }

            return model;
        }

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedShippingMethod()
        {
            var name = ShippingMethodSelectedNameElement
                .TextHelper()
                .InnerText;

            name = IgnoreLastParenthesis(name);

            return name;
        }

        /// <summary>
        /// Gets the shipping methods.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetShippingMethods()
        {
            return ShippingMethodNameElements
                .Select(e => e.TextHelper().InnerText);
        }

        /// <summary>
        /// Gets the total steps.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetTotalSteps()
        {
            return GetAllStepNames().Count;
        }

        /// <summary>
        /// Gets all step names.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<string> GetAllStepNames()
        {
            return TabNameElements
                .Select(e => e.TextHelper().InnerText)
                .ToList()
                .AsReadOnly();
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
        /// Selects the payment method.
        /// </summary>
        /// <param name="paymentMethodName">The payment method name.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <exception cref="NoSuchElementException"></exception>
        public void SelectPaymentMethod(string paymentMethodName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var radioEls = PaymentMethodRadioElements;
            var names = GetPaymentMethods().ToList();
            var foundPaymentMethod = false;

            for (var i = 0; i < names.Count; i++)
            {
                var name = names[i];
                var matches = String.Equals(
                    name,
                    paymentMethodName,
                    stringComparison);

                if (matches)
                {
                    foundPaymentMethod = true;
                    this.paymentMethodName = name;
                    radioEls.ElementAt(i).Click();
                    break;
                }
            }

            if (!foundPaymentMethod)
                throw new NoSuchElementException();
        }

        /// <summary>
        /// Selects the shipping method.
        /// </summary>
        public void SelectShippingMethod(string shippingMethod,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var els = ShippingMethodRadioElements;
            var shippingMethodNames = GetShippingMethods().ToList();
            var foundMethod = false;

            for (var i = 0; i < shippingMethodNames.Count; i++)
            {
                var name = shippingMethodNames[i];
                var isMatch = String.Equals(
                    name,
                    shippingMethod,
                    stringComparison);

                if (isMatch)
                {
                    ShippingMethodRadioElements.ElementAt(i).Click();
                    foundMethod = true;
                    break;
                }
            }

            if (!foundMethod)
                throw new Exception("Failed to locate the payment method.");
        }

        /// <summary>
        /// Finalizes and confirms the order.
        /// </summary>
        /// <param name="resolve">Called if</param>
        /// <param name="reject"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool TryConfirm(Action<ICompletedPage> resolve,
            Action<ICheckoutPage> reject)
        {
            if (GetCurrentStepName() != "Confirm order")
                throw new Exception("Not on correct step.");

            // Check if on right step.
            if (GetCurrentStep() != 6)
                throw new Exception("Not on the correct step.");

            ConfirmElement.Click();

            // Wait until the please wait message appears and then either
            // dissapears or becomes a stale element (would be null in that
            // case).
            var result = WrappedDriver
                .Wait(TimeSpan.FromMinutes(5))
                .TrySequentialWait(
                    out var exc,
                    d => ConfirmPleaseWaitElement.Displayed,
                    d => !ConfirmPleaseWaitElement?.Displayed ?? false);

            if (result)
            {
                var completedPage = pageObjectFactory
                    .PreparePage<ICompletedPage>();
                resolve(completedPage);
            }
            else
            {
                reject(this);
            }

            return result;
        }

        /// <summary>
        /// Tries the go to step.
        /// </summary>
        /// <param name="step">The step (zero based).</param>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <returns>
        /// The operation success status.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">step</exception>
        /// <exception cref="ArgumentNullException">
        /// resolve
        /// or
        /// reject
        /// </exception>
        public bool TryGoToStep(int step,
            Action<ICheckoutStepPage> resolve = null,
            Action<ICheckoutStepPage> reject = null)
        {
            if (step < 0 || step >= GetTotalSteps())
                throw new ArgumentOutOfRangeException(nameof(step));

            var result = false;
            var currentStep = GetCurrentStep();

            // Check if already on step.
            if (currentStep == step)
            {
                result = true;
            }
            else
            {

                if (step > currentStep)
                {
                    // Trying to go to the next step. Since steps might be
                    // skipped it can be any number greater then the current
                    // one.
                    IWebElement nextStepEl = null;
                    IWebElement pleaseWaitEl = null;

                    if (BillingAddressNextStepElement?.Displayed ?? false)
                    {
                        nextStepEl = BillingAddressNextStepElement;
                        pleaseWaitEl = BillingAddressPleaseWaitElement;
                    }
                    else if (ShippingAddressNextStepElement?.Displayed ?? false)
                    {
                        nextStepEl = ShippingAddressNextStepElement;
                        pleaseWaitEl = ShippingAddressPleaseWaitElement;
                    }
                    else if (ShippingMethodNextStepElement?.Displayed ?? false)
                    {
                        nextStepEl = ShippingMethodNextStepElement;
                        pleaseWaitEl = ShippingMethodPleaseWaitElement;
                    }
                    else if (PaymentMethodNextStepElement?.Displayed ?? false)
                    {
                        nextStepEl = PaymentMethodNextStepElement;
                        pleaseWaitEl = PaymentMethodPleaseWaitElement;
                    }
                    else if (PaymentInformationNextStepElement?.Displayed ?? false)
                    {
                        nextStepEl = PaymentInformationNextStepElement;
                        pleaseWaitEl = PaymentInformationPleaseWaitElement;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    // Click the next step button.
                    nextStepEl.Click();

                    WrappedDriver
                        .Wait(TimeSpan.FromSeconds(30))
                        .TrySequentialWait(
                            out var exc,
                            d => pleaseWaitEl.Displayed,
                            d => !pleaseWaitEl.Displayed);

                    // Verify the step changed to the correct one.
                    result = GetCurrentStep() == step;
                }
                else if (step < currentStep)
                {
                    // Trying to back to a previous step.
                    var activatableSteps = WrappedDriver
                        .FindElements(activeTabTitleSelector);

                    // Verify still in range.
                    if (activatableSteps.Count > step)
                        activatableSteps[step].Click();
                    else
                        result = false;
                }
                else
                {
                    result = false;
                }
            }

            if (result)
                resolve?.Invoke(this);
            else
                reject?.Invoke(this);

            return result;
        }

        /// <summary>
        /// Uses an existing billing address.
        /// </summary>
        /// <param name="shipToSameAddress">if set to <c>true</c> [ship to same address].</param>
        /// <exception cref="Exception">
        /// Not on correct step.
        /// or
        /// No existing addresses to choose from.
        /// </exception>
        public void UseExistingBillingAddress(bool shipToSameAddress = true)
        {
            if (GetCurrentStepName() != "Billing address")
                throw new Exception("Not on correct step.");
            else if (!HasExistingBillingAddresses())
                throw new Exception("No existing addresses to choose from.");

            ShipToSameAddressElement.Check(shipToSameAddress);

            // Select random address.
            var dropDown = BillingAddressDropDownElement;
            var opt = dropDown.Options
                .Where(e => !String.IsNullOrEmpty(e.GetElementProperty("value")))
                .SelectRandom();

            var index = dropDown.Options.IndexOf(opt);
            dropDown.SelectByIndex(index);
        }

        /// <summary>
        /// Uses an existing shipping address.
        /// </summary>
        /// <exception cref="Exception">
        /// Not on correct step.
        /// or
        /// No existing addresses to choose from.
        /// </exception>
        public void UseExistingShippingAddress()
        {
            if (GetCurrentStepName() != "Shipping address")
                throw new Exception("Not on correct step.");
            else if (!HasExistingShippingAddresses())
                throw new Exception("No existing addresses to choose from.");

            // Select random address.
            var dropDown = ShippingAddressDropDownElement;
            var opt = dropDown.Options
                .Where(e => !String.IsNullOrEmpty(e.GetElementProperty("value")))
                .SelectRandom();

            var index = dropDown.Options.IndexOf(opt);
            dropDown.SelectByIndex(index);
        }

        /// <summary>
        /// Gets the payment methods.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetPaymentMethods()
        {
            if (GetCurrentStepName() != "Payment method")
                throw new Exception("Not on correct step.");

            return PaymentMethodNameElements
                .Select(e => e.TextHelper().InnerText);
        }

        /// <summary>
        /// Tries the go to step.
        /// </summary>
        /// <param name="stepName">Name of the step.</param>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns>
        /// The operation success status.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// resolve
        /// or
        /// reject
        /// </exception>
        /// <exception cref="NotImplementedException"></exception>
        public bool TryGoToStep(string stepName,
            Action<ICheckoutStepPage> resolve = null,
            Action<ICheckoutStepPage> reject = null,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (String.IsNullOrEmpty(stepName))
                throw new ArgumentNullException(stepName);

            var indexOfStep = GetAllStepNames().IndexOf(stepName);

            return TryGoToStep(indexOfStep, resolve, reject);
        }

        private IEnumerable<string> GetMessageErrors()
        {
            return MessageErrorElements.Select(e => e.TextHelper().InnerText);
        }

        private bool HasExistingBillingAddresses()
        {
            if (GetCurrentStepName() != "Billing address")
                throw new Exception("Not on correct step.");

            // If the element exists then there are existing addresses.
            return WrappedDriver
                .FindElements(billingAddressDropDownSelector)
                .Any();
        }

        private bool HasExistingShippingAddresses()
        {
            // If the element exists then there are existing addresses.
            return WrappedDriver
                .FindElements(shippingAddressDropDownSelector)
                .Any();
        }

        private AddressModel GetAddressFromExistingAddress(string addressStr)
        {
            var match = Regex.Match(
                addressStr,
                @"^(?<firstname>.*?)\s(?<lastname>.*?),\s(?<address1>.*?),\s(?<stateprovince>.*?),\s(?<city>.*?)\s(?<zipcode>\d.*?),\s(?<country>.*)$");

            var model = new AddressModel
            {
                Address1 = match.Groups["address1"].Value,
                City = match.Groups["city"].Value,
                Country = match.Groups["country"].Value,
                FirstName = match.Groups["firstname"].Value,
                LastName = match.Groups["lastname"].Value,
                StateProvince = match.Groups["stateprovince"].Value,
                ZipPostalCode = match.Groups["zipcode"].Value
            };

            return model;
        }

        private AddressModel GetAddressFromNewAddress(
            InputElement firstNameElement,
            InputElement lastNameElement,
            InputElement emailElement,
            InputElement companyElement,
            SelectElement countryElement,
            SelectElement stateProvinceElement,
            InputElement cityElement,
            InputElement address1Element,
            InputElement address2Element,
            InputElement zipPostalCodeElement,
            InputElement phoneElement,
            InputElement faxNumberElement)
        {
            var model = new AddressModel();

            model.Address1 = firstNameElement.GetValue<string>();
            model.Address2 = lastNameElement.GetValue<string>();
            model.City = cityElement.GetValue<string>();
            model.Company = companyElement.GetValue<string>();

            model.Country = countryElement
                .SelectedOption
                .TextHelper()
                .InnerText;

            model.Email = emailElement.GetValue<string>();
            model.FaxNumber = faxNumberElement.GetValue<string>();
            model.FirstName = firstNameElement.GetValue<string>();
            model.LastName = lastNameElement.GetValue<string>();
            model.PhoneNumber = phoneElement.GetValue<string>();

            model.StateProvince = stateProvinceElement
                .SelectedOption
                .TextHelper()
                .InnerText;

            model.ZipPostalCode = zipPostalCodeElement.GetValue<string>();

            return model;
        }

        private string IgnoreLastParenthesis(string str)
        {
            return Regex.Match(
                    str,
                    @"(.*)\s\(.*\)")
                .Groups[1]
                .Value;
        }

        #endregion
    }
}
