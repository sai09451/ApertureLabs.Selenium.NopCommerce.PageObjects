using System;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order
{
    /// <summary>
    /// CustomerOrderRowComponent.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.FluidPageComponent{T}" />
    public class CustomerOrderRowComponent : FluidPageComponent<ICustomerOrdersPage>
    {
        #region Fields

        #region Selectors

        private readonly By detailsSelector = By.CssSelector(".buttons .order-details-button");
        private readonly By orderNumberSelector = By.CssSelector(".title");
        private readonly By orderStatusSelector = By.CssSelector(".order-status");
        private readonly By orderDateSelector = By.CssSelector(".order-date");
        private readonly By orderTotalSelector = By.CssSelector(".order-total");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderRowComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">pageObjectFactory</exception>
        public CustomerOrderRowComponent(By selector,
            ICustomerOrdersPage parent,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver, parent)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement DetailsElement => WrappedDriver
            .FindElement(detailsSelector);

        private IWebElement OrderNumberElement => WrappedDriver
            .FindElement(orderNumberSelector);

        private IWebElement OrderStatusElement => WrappedDriver
            .FindElement(orderStatusSelector);

        private IWebElement OrderDateElement => WrappedDriver
            .FindElement(orderDateSelector);

        private IWebElement OrderTotalElement => WrappedDriver
            .FindElement(orderTotalSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Views the details.
        /// </summary>
        /// <returns></returns>
        public virtual IOrderDetailsPage ViewDetails()
        {
            DetailsElement.Click();

            return pageObjectFactory.PreparePage<IOrderDetailsPage>();
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
            return OrderStatusElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the order date.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetOrderDate()
        {
            return OrderDateElement.TextHelper().ExtractDateTime();
        }

        /// <summary>
        /// Gets the order total.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetOrderTotal()
        {
            return OrderTotalElement.TextHelper().ExtractPrice();
        }

        #endregion
    }
}
