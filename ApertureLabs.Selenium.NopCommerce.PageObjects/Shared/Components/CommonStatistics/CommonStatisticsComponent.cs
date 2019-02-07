using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CommonStatistics
{
    /// <summary>
    /// CommonStatisticsComponent.
    /// </summary>
    public class CommonStatisticsComponent : PageComponent
    {
        #region Fields

        #region Selectors

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        public CommonStatisticsComponent(IWebDriver driver)
            : base(By.CssSelector(""), driver)
        { }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override bool IsStale()
        {
            return WrappedElement.IsStale();
        }

        #endregion
    }
}
