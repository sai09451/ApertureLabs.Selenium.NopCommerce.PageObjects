using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home
{
    /// <summary>
    /// HomePage.
    /// </summary>
    public class HomePage : BasePage
    {
        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="pageObjectFactory"></param>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public HomePage(IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(pageObjectFactory, driver, pageSettings)
        { }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <inheritdoc/>
        public ILoadableComponent Load(bool navigateToUrl)
        {
            WrappedDriver.Navigate().GoToUrl(Uri);
            return Load();
        }

        #endregion
    }
}
