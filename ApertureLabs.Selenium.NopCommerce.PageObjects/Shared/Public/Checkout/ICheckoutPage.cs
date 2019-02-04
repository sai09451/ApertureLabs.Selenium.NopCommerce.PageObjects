using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;

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
        void EnterBillingAddress(AddressModel address);

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
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        AddressModel GetShippingAddress();

        /// <summary>
        /// Selects the shipping method.
        /// </summary>
        void SelectShippingMethod();

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <returns></returns>
        string GetShippingMethod();

        /// <summary>
        /// Selects the payment method.
        /// </summary>
        void SelectPaymentMethod();

        /// <summary>
        /// Gets the payment method.
        /// </summary>
        /// <returns></returns>
        string GetPaymentMethod();

        /// <summary>
        /// Enters the payment information.
        /// </summary>
        void EnterPaymentInformation();

        /// <summary>
        /// Gets the payment information.
        /// </summary>
        /// <returns></returns>
        object GetPaymentInformation();

        /// <summary>
        /// Finalizes and confirms the order.
        /// </summary>
        void Confirm();
    }
}
