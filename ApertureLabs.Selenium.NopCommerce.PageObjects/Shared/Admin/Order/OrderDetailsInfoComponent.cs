using System;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    public class OrderDetailsInfoComponent : PageComponent
    {
        #region Fields

        #region Selectors

        #endregion

        #endregion

        #region Constructor

        public OrderDetailsInfoComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        public string GetOrderStatus()
        {
            throw new NotImplementedException();
        }

        public int GetOrderNumber()
        {
            throw new NotImplementedException();
        }

        public string GetOrderGuid()
        {
            throw new NotImplementedException();
        }

        public string GetCustomerEmailAddress()
        {
            throw new NotImplementedException();
        }

        public string GetCustomerIpAddress()
        {
            throw new NotImplementedException();
        }

        public decimal GetOrderSubTotal()
        {
            throw new NotImplementedException();
        }

        public decimal GetOrderShipping()
        {
            throw new NotImplementedException();
        }

        public decimal GetOrderTax()
        {
            throw new NotImplementedException();
        }

        public decimal GetOrderTotal()
        {
            throw new NotImplementedException();
        }

        public decimal GetProfit()
        {
            throw new NotImplementedException();
        }

        public string GetPaymentMethod()
        {
            throw new NotImplementedException();
        }

        public string GetPaidStatus()
        {
            throw new NotImplementedException();
        }

        public DateTime GetCreatedOn()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
