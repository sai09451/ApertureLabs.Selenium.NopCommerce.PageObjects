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
        #region Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public HomePage(IWebDriver driver, PageSettings pageSettings)
            : base(driver, pageSettings)
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

        /// <inheritdoc/>
        public override ILoadableComponent Load()
        {
            return base.Load();
        }

        #endregion
    }
}
