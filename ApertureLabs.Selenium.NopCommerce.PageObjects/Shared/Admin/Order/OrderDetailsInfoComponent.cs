using System;
using System.Globalization;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
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

        public string GetOrderStatus()
        {
            return OrderStatusElement.TextHelper().InnerText;
        }

        public int GetOrderNumber()
        {
            return OrderNumberElement.TextHelper().ExtractInteger();
        }

        public string GetOrderGuid()
        {
            return OrderGuidElement.TextHelper().InnerText;
        }

        public string GetStore()
        {
            return StoreElement.TextHelper().InnerText;
        }

        public string GetCustomerEmailAddress()
        {
            return CustomerEmailAddressElement.TextHelper().InnerText;
        }

        public string GetCustomerIpAddress()
        {
            return CustomerIpAddressElement.TextHelper().InnerText;
        }

        public decimal GetOrderSubTotal()
        {
            return OrderSubTotalElement.TextHelper().ExtractPrice();
        }

        public decimal GetOrderShipping()
        {
            return OrderShippingElement.TextHelper().ExtractPrice();
        }

        public decimal GetOrderTax()
        {
            return OrderTaxElement.TextHelper().ExtractPrice();
        }

        public decimal GetOrderTotal()
        {
            return OrderTotalElement.TextHelper().ExtractPrice();
        }

        public decimal GetProfit()
        {
            return ProfitElement.TextHelper().ExtractPrice();
        }

        public string GetPaymentMethod()
        {
            return PaymentMethodElement.TextHelper().InnerText;
        }

        public string GetPaidStatus()
        {
            return PaymentStatusElement.TextHelper().InnerText;
        }

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
