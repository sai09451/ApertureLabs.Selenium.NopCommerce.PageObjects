using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks
{
    /// <summary>
    /// Represents the '.admin-header-links' element.
    /// </summary>
    public class AdminHeaderLinksComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By LinkToAdministrationSelector = By.CssSelector(".administration");
        private readonly By LinkToManagePageSelector = By.CssSelector(".manage-page");

        #endregion

        #endregion

        #region Contructor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="driver"></param>
        public AdminHeaderLinksComponent(IWebDriver driver)
            : base(driver, By.CssSelector(".admin-header-links"))
        { }

        #endregion

        #region Properties

        #region Elements

        private IWebElement LinkToAdministrationElement => WrappedElement.FindElement(LinkToAdministrationSelector);
        private IWebElement LinkToManagePageElement => WrappedElement.FindElement(LinkToManagePageSelector);

        #endregion

        #endregion

        #region Methods

        public PageObject GoToAdmin()
        {
            LinkToAdministrationElement.Click();
        }

        /// <summary>
        /// Checks if the 'Manage Page' link is available.
        /// </summary>
        /// <returns></returns>
        public bool CanManagePage()
        {
            return WrappedElement.FindElements(LinkToManagePageSelector).Any();
        }

        /// <inheritdoc/>
        public override bool IsStale()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
