using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.GiftCardBox
{
    /// <summary>
    /// GiftCardBoxComponent.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class GiftCardBoxComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By giftCardInputSelector = By.CssSelector("#giftcardcouponcode");
        private readonly By giftCardSubmitSelector = By.CssSelector("#applygiftcardcouponcode");
        private readonly By messageSuccessSelector = By.CssSelector(".message-success");
        private readonly By messageFailureSelector = By.CssSelector(".message-failure");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GiftCardBoxComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public GiftCardBoxComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private InputElement GiftCardInputElement => new InputElement(WrappedElement.FindElement(giftCardInputSelector));
        private IWebElement GiftCardSubmitElement => WrappedElement.FindElement(giftCardSubmitSelector);
        private IWebElement MessageSuccessElement => WrappedElement.FindElements(messageSuccessSelector).FirstOrDefault();
        private IWebElement MessageFailureElement => WrappedElement.FindElements(messageFailureSelector).FirstOrDefault();

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Applies the gift card.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <exception cref="Exception">Failed to determine result of operation.</exception>
        public virtual void ApplyGiftCard(string code,
            Action<GiftCardBoxComponent> resolve,
            Action<GiftCardBoxComponent> reject)
        {
            GiftCardInputElement.SetValue(code);
            GiftCardSubmitElement.Click();

            // Page should reload.
            Load();

            // Check if any errors occurred.
            if (MessageFailureElement != null)
                reject(this);
            else if (MessageSuccessElement != null)
                resolve(this);
            else
                throw new Exception("Failed to determine result of operation.");
        }

        #endregion
    }
}
