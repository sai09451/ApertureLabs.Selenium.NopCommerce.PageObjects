using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// Used to define payment method handlers. These will be auto-registered
    /// in the <c>Resources.AutofacStartup</c> class. Make sure any arguments
    /// need to construct these handlers are registered with the dependency
    /// injector. Note: The class should also be public.
    /// </summary>
    public interface IPaymentMethodHandler
    {
        /// <summary>
        /// Determines whether this instance can operate on the specified
        /// payment method.
        /// </summary>
        /// <param name="paymentMethodName">Name of the payment method.</param>
        /// <returns>
        ///   <c>true</c> if this instance can operate on the specified payment
        ///   method; otherwise, <c>false</c>.
        /// </returns>
        bool CanOperateOn(string paymentMethodName);

        /// <summary>
        /// Enters the information into the container element.
        /// </summary>
        /// <param name="containerElement">The container element.</param>
        void EnterInformation(IWebElement containerElement);
    }
}
