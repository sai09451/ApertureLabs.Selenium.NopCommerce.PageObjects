using System;
using System.Globalization;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter
{
    /// <summary>
    /// Represents the footer component of an admin page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class AdminFooterComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By displayedDateTimeSelector = By.CssSelector(".text-center");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminFooterComponent"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector. Defaults to .main-footer.</param>
        public AdminFooterComponent(IWebDriver driver, By selector = null)
            : base(selector ?? By.CssSelector(".main-footer"), driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private IWebElement DisplayedDateTimeElement => WrappedElement.FindElement(displayedDateTimeSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the displayed date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetDisplayedDate()
        {
            var innerText = DisplayedDateTimeElement.TextHelper().InnerText;

            return DateTime.ParseExact(
                innerText,
                "dddd, MMMM dd, yyyy hh:mm:ss tt",
                CultureInfo.CreateSpecificCulture("en-US"));
        }

        #endregion
    }
}
