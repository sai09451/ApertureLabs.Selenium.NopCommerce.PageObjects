using System;
using System.Globalization;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The order details info component.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.FluidPageComponent{T}" />
    public class OrderDetailsInfoComponent : FluidPageComponent<IEditPage>
    {
        #region Fields

        #region Selectors

        private readonly By orderStatusSelector = By.CssSelector(".panel-group .panel:nth-child(1) .form-group:nth-child(1) .form-text-row");
        private readonly By orderNumberSelector = By.CssSelector(".panel-group .panel:nth-child(1) .form-group:nth-child(2) .form-text-row");
        private readonly By orderGuidSelector = By.CssSelector(".panel-group .panel:nth-child(1) .form-group:nth-child(3) .form-text-row");
        private readonly By storeSelector = By.CssSelector(".panel-group .panel:nth-child(1) .form-group:nth-child(4) .form-text-row");
        private readonly By customerEmailAddressSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(1) .form-text-row");
        private readonly By customerIpAddressSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(2) .form-text-row");
        private readonly By orderSubTotalSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(3) .form-text-row");
        private readonly By orderShippingSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(4) .form-text-row");
        private readonly By orderTaxSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(5) .form-text-row");
        private readonly By orderTotalSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(6) .form-text-row");
        private readonly By profitSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(7) .form-text-row");
        private readonly By paymentMethodSelector = By.CssSelector(".panel-group .panel:nth-child(2) .form-group:nth-child(8) .form-text-row");
        private readonly By paymentStatusSelector = By.CssSelector(".panel-group .panel:nth-child(3) .form-group:nth-child(1) .form-text-row");
        private readonly By createdOnSelector = By.CssSelector(".panel-group .panel:nth-child(3) .form-group:nth-child(2) .form-text-row");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsInfoComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="parent">The parent. Can be null.</param>
        public OrderDetailsInfoComponent(By selector,
            IWebDriver driver,
            IEditPage parent)
            : base(selector,
                  driver,
                  parent)
        { }

        #endregion

        #region Properties

        #region Elements

        private IWebElement OrderStatusElement => WrappedDriver
            .FindElement(orderStatusSelector);

        private IWebElement OrderNumberElement => WrappedDriver
            .FindElement(orderNumberSelector);

        private IWebElement OrderGuidElement => WrappedDriver
            .FindElement(orderGuidSelector);

        private IWebElement StoreElement => WrappedDriver
            .FindElement(storeSelector);

        private IWebElement CustomerEmailAddressElement => WrappedDriver
            .FindElement(customerEmailAddressSelector);

        private IWebElement CustomerIpAddressElement => WrappedDriver
            .FindElement(customerIpAddressSelector);

        private IWebElement OrderSubTotalElement => WrappedDriver
            .FindElement(orderSubTotalSelector);

        private IWebElement OrderShippingElement => WrappedDriver
            .FindElement(orderShippingSelector);

        private IWebElement OrderTaxElement => WrappedDriver
            .FindElement(orderTaxSelector);

        private IWebElement OrderTotalElement => WrappedDriver
            .FindElement(orderTotalSelector);

        private IWebElement ProfitElement => WrappedDriver
            .FindElement(profitSelector);

        private IWebElement PaymentMethodElement => WrappedDriver
            .FindElement(paymentMethodSelector);

        private IWebElement PaymentStatusElement => WrappedDriver
            .FindElement(paymentStatusSelector);

        private IWebElement CreatedOnElement => WrappedDriver
            .FindElement(createdOnSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the order status.
        /// </summary>
        /// <returns></returns>
        public string GetOrderStatus()
        {
            return OrderStatusElement.TextHelper().InnerText;
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
        /// Gets the order unique identifier.
        /// </summary>
        /// <returns></returns>
        public string GetOrderGuid()
        {
            return OrderGuidElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the store name.
        /// </summary>
        /// <returns></returns>
        public string GetStore()
        {
            return StoreElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the customer email address.
        /// </summary>
        /// <returns></returns>
        public string GetCustomerEmailAddress()
        {
            return CustomerEmailAddressElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the customer ip address.
        /// </summary>
        /// <returns></returns>
        public string GetCustomerIpAddress()
        {
            return CustomerIpAddressElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the order sub total.
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderSubTotal()
        {
            return OrderSubTotalElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the order shipping cost.
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderShipping()
        {
            return OrderShippingElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the order tax.
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderTax()
        {
            return OrderTaxElement.TextHelper().ExtractPrice();
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
        /// Gets the profit.
        /// </summary>
        /// <returns></returns>
        public decimal GetProfit()
        {
            return ProfitElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the payment method.
        /// </summary>
        /// <returns></returns>
        public string GetPaymentMethod()
        {
            return PaymentMethodElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the paid status.
        /// </summary>
        /// <returns></returns>
        public string GetPaidStatus()
        {
            return PaymentStatusElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the date the order was created on.
        /// </summary>
        /// <returns></returns>
        public DateTime GetCreatedOn()
        {
            var dateTimeStr = CreatedOnElement.TextHelper().InnerText;
            var dateTime = DateTime.ParseExact(
                dateTimeStr,
                "M/d/yyyy h:m:ss tt",
                new CultureInfo("en-US"));

            return dateTime;
        }

        #endregion

    }
}
