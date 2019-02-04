using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// OnePageCheckout.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public interface IOnePageCheckoutPage : ICheckoutPage
    {
        /// <summary>
        /// Gets the current step.
        /// </summary>
        /// <returns></returns>
        int GetCurrentStep();

        /// <summary>
        /// Tries the go to step.
        /// </summary>
        /// <param name="step">The step.</param>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        void TryGoToStep(int step,
            Action<IOnePageCheckoutPage> resolve,
            Action<IOnePageCheckoutPage> reject);
    }
}
