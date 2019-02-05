using System;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.PaymentHandlers
{
    /// <summary>
    /// NopCheckPaymentHandler.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout.IPaymentMethodHandler" />
    public class NopCheckPaymentHandler : IPaymentMethodHandler
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NopCheckPaymentHandler"/> class.
        /// </summary>
        public NopCheckPaymentHandler()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance can operate on the specified
        /// payment method.
        /// </summary>
        /// <param name="paymentMethodName">Name of the payment method.</param>
        /// <returns>
        /// <c>true</c> if this instance can operate on the specified payment
        /// method; otherwise, <c>false</c>.
        /// </returns>
        public bool CanOperateOn(string paymentMethodName)
        {
            return String.Equals(
                paymentMethodName,
                "Check / Money Order",
                StringComparison.Ordinal);
        }

        /// <summary>
        /// Enters the information into the container element.
        /// </summary>
        /// <param name="containerElement">The container element.</param>
        public void EnterInformation(IWebElement containerElement)
        { }

        #endregion
    }
}
