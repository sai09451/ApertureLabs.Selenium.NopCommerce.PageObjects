﻿using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product
{
    /// <summary>
    /// ChallengeCodeComponent (might also be referred to as the discount).
    /// </summary>
    public class ChallengeCodeComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By challengeCodeInputSelector = By.CssSelector("#pdp_applyCouponText");
        private readonly By challengeCodeSubmitSelector = By.CssSelector("#pdp_applyCouponButton");
        private readonly By appliedDiscountCodeSelector = By.CssSelector(".applied-discount-code");
        private readonly By removeAppliedDiscountSelector = By.CssSelector(".remove-discount-button");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="driver"></param>
        internal ChallengeCodeComponent(IWebDriver driver, By selector)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private InputElement ChallengeCodeInputElement => new InputElement(WrappedElement.FindElement(challengeCodeInputSelector));
        private IWebElement ChallengeCodeSubmitElement => WrappedElement.FindElement(challengeCodeSubmitSelector);
        private IWebElement AppliedDiscountCodeElement => WrappedElement.FindElement(appliedDiscountCodeSelector);
        private IWebElement RemoveAppliedDisountElement => WrappedElement.FindElement(removeAppliedDiscountSelector);

        #endregion

        /// <summary>
        /// Checks if the product page has a discount applied to it.
        /// </summary>
        public virtual bool HasAppliedCode
        {
            get
            {
                return WrappedElement
                    .FindElements(removeAppliedDiscountSelector)
                    .Any();
            }
        }

        /// <summary>
        /// Make sure 'HasAppliedCode' returns true before calling this.
        /// Returns the currently applied discount code.
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// Thrown if there is no applied discount.
        /// </exception>
        public virtual string AppliedCode
        {
            get
            {
                return AppliedDiscountCodeElement.Text;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Enters a discount.
        /// </summary>
        /// <param name="code"></param>
        public virtual void EnterDiscount(string code)
        {
            ChallengeCodeInputElement.SetValue(code);

            var refEl = ChallengeCodeSubmitElement;
            refEl.Click();

            // Wait for the page to reload.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(30))
                .UntilStale(refEl);
        }

        /// <summary>
        /// Alias for EnterDiscount.
        /// </summary>
        /// <param name="code"></param>
        public virtual void EnterCode(string code)
        {
            EnterDiscount(code);
        }

        /// <summary>
        /// Alias for EnterDiscount.
        /// </summary>
        /// <param name="code"></param>
        public virtual void EnterCouponCode(string code)
        {
            EnterDiscount(code);
        }

        /// <summary>
        /// Removes the discount.
        /// </summary>
        public virtual void RemoveDiscount()
        {
            RemoveAppliedDisountElement.Click();
        }

        /// <summary>
        /// Alias for RemoveDiscount.
        /// </summary>
        public virtual void RemoveCode()
        {
            RemoveDiscount();
        }

        /// <summary>
        /// Alias for EnterDiscount.
        /// </summary>
        public virtual void RemoveCouponCode()
        {
            RemoveDiscount();
        }

        #endregion
    }
}
