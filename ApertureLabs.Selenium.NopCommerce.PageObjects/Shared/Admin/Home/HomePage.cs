using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Home
{
    /// <summary>
    /// The admin home page.
    /// </summary>
    public class HomePage : BasePage
    {
        #region Fields

        private readonly IPageObjectFactory pageObjectFactory;

        #region Selectors

        private By NewsComponentSelector => By.CssSelector("");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        /// <param name="pageObjectFactory"></param>
        public HomePage(IWebDriver driver,
            PageSettings pageSettings,
            PageObjectFactory pageObjectFactory = null)
            : base(driver, pageSettings)
        {
            pageObjectFactory = new PageObjectFactory();
        }

        #endregion

        #region Properties

        /// <summary>
        /// News section of the home page.
        /// </summary>
        public NewsComponent News => pageObjectFactory
            .PrepareComponent(new NewsComponent(WrappedDriver));

        #region Elements

        #endregion

        #endregion

        #region Methods

        #endregion
    }
}
