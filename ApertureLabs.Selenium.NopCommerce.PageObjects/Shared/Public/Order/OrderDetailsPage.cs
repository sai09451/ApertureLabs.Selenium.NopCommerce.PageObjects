using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order
{
    /// <summary>
    /// OrderDetailsPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order.IOrderDetailsPage" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public class OrderDetailsPage : PageObject, IOrderDetailsPage
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
        private readonly By shippingStatusSelector = By.CssSelector(".shipping-status .value");
        private readonly By cartTotalColsSelector = By.CssSelector(".cart-total tbody tr td");
        private readonly By selectedCheckoutAttributesSelector = By.CssSelector(".selected-checkout-attributes");
        private readonly By reorderButtonSelector = By.CssSelector(".action .re-order-button");
        private readonly By pdfInvoiceButtonSelector = By.CssSelector(".pdf-invoice-button");

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
        private IWebElement ShippingStatusElement => WrappedDriver.FindElement(shippingStatusSelector);
        private IReadOnlyCollection<IWebElement> CartTotalColElements => WrappedDriver.FindElements(cartTotalColsSelector);
        private IWebElement SelectedCheckoutAttributeElement => WrappedDriver.FindElement(selectedCheckoutAttributesSelector);
        private IWebElement ReorderButtonElement => WrappedDriver.FindElement(reorderButtonSelector);
        private IWebElement PdfInvoiceButtonElement => WrappedDriver.FindElement(pdfInvoiceButtonSelector);

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
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Gets the billing address.
        /// </summary>
        /// <returns></returns>
        public virtual AddressModel GetBillingAddress()
        {
            return ExtractAddressFromContainer(BillingAddressElement);
        }

        /// <summary>
        /// Gets the order date.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetOrderDate()
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
        public virtual int GetOrderNumber()
        {
            return OrderNumberElement.TextHelper().ExtractInteger();
        }

        /// <summary>
        /// Gets the order status.
        /// </summary>
        /// <returns></returns>
        public virtual string GetOrderStatus()
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
        public virtual decimal GetOrderTotal()
        {
            return OrderTotalElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<OrderSummaryReadOnlyRowComponent> GetProducts()
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
        public virtual decimal GetShipping()
        {
            return ShippingMethodElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        public virtual AddressModel GetShippingAddress()
        {
            return ExtractAddressFromContainer(ShippingAddressElement);
        }

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <returns></returns>
        public virtual string GetShippingMethod()
        {
            return ShippingMethodElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the shipping status.
        /// </summary>
        /// <returns></returns>
        public virtual string GetShippingStatus()
        {
            return ShippingStatusElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the sub total.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetSubTotal()
        {
            return GetCartTotalRowValue("Sub-Total")
                .TextHelper()
                .ExtractPrice();
        }

        /// <summary>
        /// Gets the tax.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetTax()
        {
            return GetCartTotalRowValue("Tax")
                .TextHelper()
                .ExtractPrice();
        }

        /// <summary>
        /// Gifts the card.
        /// </summary>
        /// <returns></returns>
        public virtual decimal? GiftCard()
        {
            return GetCartTotalRowValue("Gift")
                ?.TextHelper()
                .ExtractPrice();
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
        /// Determines whether [has gift wrapping].
        /// </summary>
        /// <returns>
        /// <c>true</c> if [has gift wrapping]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasGiftWrapping()
        {
            return SelectedCheckoutAttribute("Gift wrapping") == "Yes";
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
        /// Downloads a pdf of the order details.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void PdfInvoice()
        {
            PdfInvoiceButtonElement.Click();

            // TODO: Get downloads folder location.
            var downLoadPath = Path.Combine(
                "",
                $"order_{GetOrderNumber()}.pdf");

            WrappedDriver
                .Wait(TimeSpan.FromMinutes(1))
                .Until(d => File.Exists(downLoadPath));
        }

        /// <summary>
        /// Prints the order details.
        /// </summary>
        public virtual void Print()
        {
            var tabHelper = WrappedDriver.TabHelper();
            var initialTabs = tabHelper.GetNumberOfTabs();
            var initialTab = WrappedDriver.CurrentWindowHandle;
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(30))
                .Until(
                    d => tabHelper.GetNumberOfTabs().Count > initialTabs.Count);

            WrappedDriver
                .SwitchTo()
                .Window(tabHelper
                    .GetNumberOfTabs()
                    .Except(initialTabs)
                    .First());

            var printWindowHandle = WrappedDriver.CurrentWindowHandle;
            WrappedDriver.WaitForUserSignal(TimeSpan.FromMinutes(5));

            // Close the tab if it's still open.
            if (WrappedDriver.CurrentWindowHandle == printWindowHandle)
                WrappedDriver.Close();

            // Switch back to the initial window handle.
            WrappedDriver.SwitchTo().Window(initialTab);
        }

        /// <summary>
        /// Re-orders the order.
        /// </summary>
        /// <returns></returns>
        public virtual ICartPage ReOrder()
        {
            ReorderButtonElement.Click();

            return pageObjectFactory.PreparePage<ICartPage>();
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

        private IWebElement GetCartTotalRowValue(string label)
        {
            var rows = CartTotalColElements.Chunk(2);
            var element = default(IWebElement);

            foreach (var row in rows)
            {
                if (row[0].TextHelper().InnerText.StartsWith(label))
                {
                    element = row[1];
                    break;
                }
            }

            return element;
        }

        private string SelectedCheckoutAttribute(string label)
        {
            var result = default(string);
            var matches = Regex.Matches(
                SelectedCheckoutAttributeElement.TextHelper().InnerText,
                @"^(.*?):\s?(.*)$");

            foreach (Match match in matches)
            {
                var m_label = match.Groups[1].Value;
                var m_value = match.Groups[2].Value;

                if (m_label.StartsWith(label))
                {
                    result = m_value;
                    break;
                }
            }

            return result;
        }

        #endregion
    }
}
