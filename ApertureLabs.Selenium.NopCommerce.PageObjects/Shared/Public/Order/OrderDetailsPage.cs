using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order
{
    /// <summary>
    /// OrderDetailsPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order.IOrderDetailsPage" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public class OrderDetailsPage : PageObject, IOrderDetailsPage, IBasePage
    {
        #region Fields

        #region Selectors

        private readonly By billingAddressSelector = By.CssSelector(".billing-info .info-list");
        private readonly By orderDateSelector = By.CssSelector(".order-date");
        private readonly By orderNumberSelector = By.CssSelector(".order-number");
        private readonly By orderStatusSelector = By.CssSelector(".order-status");
        private readonly By orderTotalSelector = By.CssSelector(".order-total");
        private readonly By productRowsSelector = By.CssSelector(".products tbody tr");
        private readonly By shippingAddressSelector = By.CssSelector(".shipping-info .info-list");
        private readonly By shippingMethodSelector = By.CssSelector(".shipping-method .value");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OrderDetailsPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver)
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement BillingAddressElement => WrappedDriver.FindElement(billingAddressSelector);
        private IWebElement OrderDateElement => WrappedDriver.FindElement(orderDateSelector);
        private IWebElement OrderNumberElement => WrappedDriver.FindElement(orderNumberSelector);
        private IWebElement OrderStatusElement => WrappedDriver.FindElement(orderStatusSelector);
        private IWebElement OrderTotalElement => WrappedDriver.FindElement(orderTotalSelector);
        private IWebElement ShippingAddressElement => WrappedDriver.FindElement(shippingAddressSelector);
        private IWebElement ShippingMethodElement => WrappedDriver.FindElement(shippingMethodSelector);

        #endregion

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

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
        /// Gets the billing address.
        /// </summary>
        /// <returns></returns>
        public AddressModel GetBillingAddress()
        {
            return ExtractAddressFromContainer(BillingAddressElement);
        }

        /// <summary>
        /// Gets the order date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetOrderDate()
        {
            var text = Regex.Replace(
                OrderDateElement.TextHelper().InnerText,
                @".*?:",
                "");

            return DateTime.ParseExact(text,
                "D",
                CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Gets the order number.
        /// </summary>
        /// <returns></returns>
        public int GetOrderNumber()
        {
            return OrderNumberElement.TextHelper().ExtractInteger();
        }

        /// <summary>
        /// Gets the order status.
        /// </summary>
        /// <returns></returns>
        public string GetOrderStatus()
        {
            var match = Regex.Match(
                OrderStatusElement.TextHelper().InnerText,
                @".*:\s(.*)");

            return match.Groups[1].Value;
        }

        /// <summary>
        /// Gets the order total.
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderTotal()
        {
            return OrderTotalElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderSummaryReadOnlyRowComponent> GetProducts()
        {
            var rowEls = WrappedDriver.FindElements(productRowsSelector);

            foreach (var rowEl in rowEls)
            {
                yield return pageObjectFactory.PrepareComponent(
                    new OrderSummaryReadOnlyRowComponent(
                        ByElement.FromElement(rowEl),
                        WrappedDriver));
            }
        }

        /// <summary>
        /// Gets the shipping.
        /// </summary>
        /// <returns></returns>
        public decimal GetShipping()
        {
            return ShippingMethodElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        public AddressModel GetShippingAddress()
        {
            return ExtractAddressFromContainer(ShippingAddressElement);
        }

        public string GetShippingMethod()
        {
            throw new NotImplementedException();
        }

        public string GetShippingStatus()
        {
            throw new NotImplementedException();
        }

        public decimal GetSubTotal()
        {
            throw new NotImplementedException();
        }

        public decimal GetTax()
        {
            throw new NotImplementedException();
        }

        public decimal GiftCard()
        {
            throw new NotImplementedException();
        }

        public ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
        }

        public void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        public bool HasGiftWrapping()
        {
            throw new NotImplementedException();
        }

        public bool HasNotifications()
        {
            return basePage.HasNotifications();
        }

        public bool IsLoggedIn()
        {
            return basePage.IsLoggedIn();
        }

        public IHomePage Login(string email, string password)
        {
            return basePage.Login(email, password);
        }

        public T Logout<T>() where T : IPageObject
        {
            return basePage.Logout<T>();
        }

        public void PdfInvoice()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public ICartPage ReOrder()
        {
            throw new NotImplementedException();
        }

        public ISearchPage Search(string searchFor)
        {
            return basePage.Search(searchFor);
        }

        public IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            return basePage.SearchAjax(searchFor);
        }

        private AddressModel ExtractAddressFromContainer(IWebElement container)
        {
            var nameEl = container.FindElement(By.CssSelector(".name"));
            var emailEl = container.FindElement(By.CssSelector(".email"));
            var phoneEl = container.FindElement(By.CssSelector(".phone"));
            var faxEl = container.FindElement(By.CssSelector(".fax"));
            var companyEl = container.FindElement(By.CssSelector(".company"));
            var address1El = container.FindElement(By.CssSelector(".address1"));
            var cityStateZipEl = container.FindElement(By.CssSelector(".city-state-zip"));
            var countryEl = container.FindElement(By.CssSelector(".country"));

            var model = new AddressModel();

            // First & last name.
            var namesMatch = Regex.Match(
                nameEl.TextHelper().InnerText,
                @"(.*) (.*)");

            model.FirstName = namesMatch.Groups[1].Value;
            model.LastName = namesMatch.Groups[2].Value;

            // Email.
            model.Email = emailEl.TextHelper().InnerText;

            // Phone.
            model.PhoneNumber = phoneEl.TextHelper().InnerText;

            // Fax.
            model.FaxNumber = faxEl.TextHelper().InnerText;

            // Company.
            model.Company = companyEl.TextHelper().InnerText;

            // Address1.
            model.Address1 = address1El.TextHelper().InnerText;

            // City/state/zip.
            var cityStateZipMatch = Regex.Match(
                cityStateZipEl.TextHelper().InnerText,
                @"(.*?),\s(.*?),\s(.*)");

            model.City = cityStateZipMatch.Groups[1].Value;
            model.StateProvince = cityStateZipMatch.Groups[2].Value;
            model.ZipPostalCode = cityStateZipMatch.Groups[3].Value;

            // Country.
            model.Country = countryEl.TextHelper().InnerText;

            return model;
        }

        #endregion
    }
}
