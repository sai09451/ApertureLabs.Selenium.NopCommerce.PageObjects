using System;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.PaymentHandlers
{
    /// <summary>
    /// NopCreditCardPaymentHandler.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout.IPaymentMethodHandler" />
    public class NopCreditCardPaymentHandler : IPaymentMethodHandler
    {
        #region Fields

        #region Selectors

        private readonly By cardTypeSelector = By.CssSelector("#CreditCardType");
        private readonly By cardholderNameSelector = By.CssSelector("#CardholderName");
        private readonly By cardNumberSelector = By.CssSelector("#CardNumber");
        private readonly By expirationDateMonthSelector = By.CssSelector("#ExpireMonth");
        private readonly By expirationDateYearSelector = By.CssSelector("#ExpireYear");
        private readonly By cardCodeSelector = By.CssSelector("#CardCode");

        #endregion

        // TODO: Finish LockableService and use that to get random cc numbers
        // and other info.

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NopCreditCardPaymentHandler"/> class.
        /// </summary>
        public NopCreditCardPaymentHandler()
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
                "Credit Card",
                StringComparison.Ordinal);
        }

        /// <summary>
        /// Enters the information into the container element.
        /// </summary>
        /// <param name="containerElement">The container element.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void EnterInformation(IWebElement containerElement)
        {
            if (containerElement == null)
                throw new ArgumentNullException(nameof(containerElement));

            // Leave card type as default (visa).

            // Card holder name.
            var cardHolderNameEl = new InputElement(
                containerElement.FindElement(
                    cardholderNameSelector));

            cardHolderNameEl.SetValue("John Smith");

            // Card number.
            var cardNumberEl = new InputElement(
                containerElement.FindElement(
                    cardNumberSelector));

            cardNumberEl.SetValue("4111111111111111");

            // Expiration date.
            var yearDropDown = new SelectElement(
                containerElement.FindElement(
                    expirationDateYearSelector));
        }

        #endregion
    }
}
