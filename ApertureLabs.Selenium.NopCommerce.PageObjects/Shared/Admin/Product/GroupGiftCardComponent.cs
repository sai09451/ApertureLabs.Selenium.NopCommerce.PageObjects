using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The 'Gift card' component on the product edit page info tab.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class GroupGiftCardComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By isGiftCardSelector = By.CssSelector("#IsGiftCard");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupGiftCardComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">pageObjectFactory</exception>
        public GroupGiftCardComponent(By selector,
            IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement IsGiftCardElement => new CheckboxElement(
            WrappedDriver.FindElement(
                isGiftCardSelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the product is a gift card.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the product is a gift card; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool GetIsGiftCard()
        {
            return IsGiftCardElement.IsChecked;
        }

        /// <summary>
        /// Sets the product is gift card value.
        /// </summary>
        /// <param name="isGiftCard">if set to <c>true</c> [is gift card].</param>
        /// <returns></returns>
        public virtual GroupGiftCardComponent SetIsGiftCard(bool isGiftCard)
        {
            IsGiftCardElement.Check(isGiftCard);

            return this;
        }

        #endregion
    }
}
