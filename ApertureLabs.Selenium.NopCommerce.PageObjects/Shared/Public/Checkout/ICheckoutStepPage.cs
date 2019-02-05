using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Checkout
{
    /// <summary>
    /// OnePageCheckout.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public interface ICheckoutStepPage : ICheckoutPage
    {
        /// <summary>
        /// Gets the current step (zero based).
        /// </summary>
        /// <returns></returns>
        int GetCurrentStep();

        /// <summary>
        /// Gets the total steps.
        /// </summary>
        /// <returns></returns>
        int GetTotalSteps();

        /// <summary>
        /// Gets the name of the current step.
        /// </summary>
        /// <returns></returns>
        string GetCurrentStepName();

        /// <summary>
        /// Gets all step names.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<string> GetAllStepNames();

        /// <summary>
        /// Tries the go to step.
        /// </summary>
        /// <param name="step">The step (zero based).</param>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <returns>The operation success status.</returns>
        bool TryGoToStep(int step,
            Action<ICheckoutStepPage> resolve = null,
            Action<ICheckoutStepPage> reject = null);

        /// <summary>
        /// Tries the go to step.
        /// </summary>
        /// <param name="stepName">Name of the step.</param>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns>The operation success status.</returns>
        bool TryGoToStep(string stepName,
            Action<ICheckoutStepPage> resolve = null,
            Action<ICheckoutStepPage> reject = null,
            StringComparison stringComparison = StringComparison.Ordinal);
    }
}
