using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.DiscountBox
{
    /// <summary>
    /// DiscountBoxComponent.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class DiscountBoxComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By inputCouponCodeSelector = By.CssSelector("#discountcouponcode");
        private readonly By submitCouponCodeSelector = By.CssSelector("#applydiscountcouponcode");
        private readonly By messageFailureSelector = By.CssSelector(".message-failure");
        private readonly By messageSuccessSelector = By.CssSelector(".message-success");
        private readonly By currentCodeContainerSelector = By.CssSelector(".current-code");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscountBoxComponent"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        public DiscountBoxComponent(IWebDriver driver, By selector)
            : base(driver, selector)
        { }

        #endregion

        #region Properties

        #region Elements

        private InputElement InputCouponCodeElement => new InputElement(WrappedElement.FindElement(inputCouponCodeSelector));
        private IWebElement SubmitCouponCodeElement => WrappedElement.FindElement(submitCouponCodeSelector);
        private IWebElement MessageFailureElement => WrappedElement.FindElements(messageFailureSelector).FirstOrDefault();
        private IWebElement MessageSuccessElement => WrappedElement.FindElements(messageSuccessSelector).FirstOrDefault();
        private IReadOnlyCollection<IWebElement> CurrentCodeContainers => WrappedElement.FindElements(currentCodeContainerSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the applied coupon codes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAppliedDiscountCodes()
        {
            return CurrentCodeContainers
                .Select(e => GetCouponCodeOfCurrentCode(e))
                .Where(str => !String.IsNullOrEmpty(str));
        }

        /// <summary>
        /// Removes the applied discount.
        /// </summary>
        /// <param name="discount">The discount.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <exception cref="NoSuchElementException"></exception>
        public void RemoveAppliedDiscount(string discount,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            foreach (var currentCodeEl in CurrentCodeContainers)
            {
                var code = GetCouponCodeOfCurrentCode(currentCodeEl);

                if (String.Equals(discount, code, stringComparison))
                {
                    var removeEl = currentCodeEl.FindElement(
                        By.CssSelector(".remove-discount-button"));
                    removeEl.Click();

                    // Page should reload.
                    Load();
                    break;
                }
            }

            throw new NoSuchElementException();
        }

        /// <summary>
        /// Applies the discount.
        /// </summary>
        /// <param name="couponCode">The coupon code.</param>
        /// <param name="resolve">Run on success.</param>
        /// <param name="reject">Run on failure.</param>
        /// <exception cref="ArgumentNullException">
        /// couponCode or resolve or reject
        /// </exception>
        /// <exception cref="Exception">
        /// Failed to identify if the operation succeeded or failed.
        /// </exception>
        public void ApplyDiscount(string couponCode,
            Action<DiscountBoxComponent> resolve,
            Action<DiscountBoxComponent> reject)
        {
            if (String.IsNullOrEmpty(couponCode))
                throw new ArgumentNullException(nameof(couponCode));
            else if (resolve == null)
                throw new ArgumentNullException(nameof(resolve));
            else if (reject == null)
                throw new ArgumentNullException(nameof(reject));

            InputCouponCodeElement.SetValue(couponCode);
            SubmitCouponCodeElement.Click();

            // Page should have been reloaded.
            Load();

            // Check if errors occurred.
            if (MessageFailureElement != null)
            {
                reject(this);
            }
            else if (MessageSuccessElement != null)
            {
                resolve(this);
            }
            else
            {
                throw new Exception("Failed to identify if the operation " +
                    "succeeded or failed.");
            }
        }

        private string GetCouponCodeOfCurrentCode(IWebElement element)
        {
            var currentCodeEl = element.FindElements(
                    By.CssSelector(".applied-discount-code"))
                .FirstOrDefault();

            if (currentCodeEl == null)
                return null;

            return Regex.Match(
                    element.TextHelper().InnerText,
                    @"Entered coupon code - (.*)")
                .Groups?[1].Value;
        }

        #endregion
    }
}
