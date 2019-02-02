using System;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin
{
    /// <summary>
    /// Base page for all admin pages.
    /// </summary>
    public class BasePage : PageObject, IBasePage
    {
        #region Fields

        #region Selectors

        private readonly By backToTopSelector = By.CssSelector("#backTop");

        #endregion

        /// <summary>
        /// Settings for the page.
        /// </summary>
        public readonly PageSettings PageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public BasePage(IWebDriver driver, PageSettings pageSettings) : base(driver)
        {
            PageSettings = pageSettings;
            Uri = new Uri(pageSettings.BaseUrl + "/Admin", UriKind.Absolute);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the main side bar which is used to navigate bewtween the
        /// admin.
        /// components.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public virtual IAdminMainSideBarComponent MainSideBar
        {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the navigation bar.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public virtual IAdminMainHeaderComponent NavigationBar
        {
            get => throw new NotImplementedException();
            private set => throw new NotImplementedException();
        }

        #region Elements

        private IWebElement BackToTopElement => WrappedDriver.FindElement(backToTopSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Backs to top.
        /// </summary>
        public virtual void BackToTop()
        {
            if (BackToTopElement.Displayed)
            {
                BackToTopElement.Click();

                // Wait until the element is no longer displayed.
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(5))
                    .Until(d => !BackToTopElement.Displayed);
            }
        }

        #endregion
    }
}