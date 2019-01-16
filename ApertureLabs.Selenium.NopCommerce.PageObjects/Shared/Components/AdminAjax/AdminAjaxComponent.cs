using System;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminAjax
{
    /// <summary>
    /// Extends PageComponent by adding a WaitForAjaxBusy(...) method.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public abstract class AdminAjaxComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By ajaxBusySelector = By.CssSelector("#ajaxBusy");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminAjaxComponent"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        public AdminAjaxComponent(IWebDriver driver, By selector)
            : base(driver, selector)
        { }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AjaxBusyElement => WrappedDriver.FindElement(ajaxBusySelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Waits for ajax busy to start and finish.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        protected void WaitForAjaxBusy(TimeSpan timeSpan)
        {
            WrappedDriver.Wait(timeSpan)
                .TrySequentialWait(
                    out var exc,
                    d => AjaxBusyElement.Displayed,
                    d => !AjaxBusyElement.Displayed);

            if (exc != null)
                throw exc;
        }

        #endregion
    }
}
