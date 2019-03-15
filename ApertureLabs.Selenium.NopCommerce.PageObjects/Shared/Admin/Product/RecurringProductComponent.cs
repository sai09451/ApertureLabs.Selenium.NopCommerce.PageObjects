using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The 'Recurring product' component on the 'info' tab of the admin
    /// product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class RecurringProductComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By recurringProductSelector = By.CssSelector("#IsRecurring");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RecurringProductComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public RecurringProductComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement RecurringProductElement => new CheckboxElement(
            WrappedDriver.FindElement(
                recurringProductSelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the is recurring product.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsRecurringProduct()
        {
            return RecurringProductElement.IsChecked;
        }

        /// <summary>
        /// Sets the is recurringproduct.
        /// </summary>
        /// <param name="isRecurring">if set to <c>true</c> [is recurring].</param>
        /// <returns></returns>
        public virtual RecurringProductComponent SetIsRecurringproduct(
            bool isRecurring)
        {
            RecurringProductElement.Check(isRecurring);

            return this;
        }

        #endregion
    }
}
