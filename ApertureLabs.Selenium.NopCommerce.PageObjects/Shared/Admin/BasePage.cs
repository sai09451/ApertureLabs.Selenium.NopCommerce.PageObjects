using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin
{
    /// <summary>
    /// Base page for all admin pages.
    /// </summary>
    public class BasePage : PageObject
    {
        #region Fields

        #region Selectors

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

        #region Elements

        #endregion

        #endregion

        #region Methods

        #endregion
    }
}