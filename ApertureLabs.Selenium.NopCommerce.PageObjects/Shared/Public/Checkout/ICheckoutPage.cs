using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// CheckoutPage.
    /// </summary>
    public interface ICheckoutPage : IBasePage
    {
        /// <summary>
        /// Enters the billing address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="shipToSameAddress">
        /// Whether or not to ship to the same address.
        /// </param>
        void EnterBillingAddress(AddressModel address,
            bool shipToSameAddress = true);

        /// <summary>
        /// Uses an existing billing address.
        /// </summary>
        /// <param name="shipToSameAddress">
        /// if set to <c>true</c> [ship to same address].
        /// </param>
        void UseExistingBillingAddress(bool shipToSameAddress = true);

        /// <summary>
        /// Gets the billing address.
        /// </summary>
        /// <returns></returns>
        AddressModel GetBillingAddress();

        /// <summary>
        /// Enters the shipping address.
        /// </summary>
        /// <param name="address">The address.</param>
        void EnterShippingAddress(AddressModel address);

        /// <summary>
        /// Uses an existing shipping address.
        /// </summary>
        void UseExistingShippingAddress();

        /// <summary>
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        AddressModel GetShippingAddress();

        /// <summary>
        /// Selects the shipping method.
        /// </summary>
        /// <param name="shippingMethod">The name of the shipping method.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SelectShippingMethod(string shippingMethod,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <returns></returns>
        string GetSelectedShippingMethod();

        /// <summary>
        /// Gets the shipping methods.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetShippingMethods();

        /// <summary>
        /// Selects the payment method.
        /// </summary>
        /// <param name="paymentMethodName">The payment method name.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SelectPaymentMethod(string paymentMethodName,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Gets the names of payment methods listed on the page.
        /// </summary>
        /// <returns></returns>
        string GetSelectedPaymentMethod();

        /// <summary>
        /// Gets the payment methods.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetPaymentMethods();

        /// <summary>
        /// Enters the payment information.
        /// </summary>
        void EnterPaymentInformation();

        /// <summary>
        /// Finalizes and confirms the order.
        /// </summary>
        /// <param name="resolve">Called if </param>
        /// <param name="reject"></param>
        bool TryConfirm(Action<ICompletedPage> resolve,
            Action<ICheckoutPage> reject);

        /// <summary>
        /// Finalizes and confirms the order.
        /// </summary>
        /// <returns></returns>
        ICompletedPage Confirm();
    }
}
