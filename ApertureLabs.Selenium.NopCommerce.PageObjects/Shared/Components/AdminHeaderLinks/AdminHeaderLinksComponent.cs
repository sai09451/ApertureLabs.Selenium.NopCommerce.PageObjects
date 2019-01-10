using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
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

        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        #region Selectors

        private readonly By LinkToAdministrationSelector = By.CssSelector(".administration");
        private readonly By LinkToManagePageSelector = By.CssSelector(".manage-page");

        #endregion

        #endregion

        #region Contructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        /// <param name="pageObjectFactory"></param>
        public AdminHeaderLinksComponent(IWebDriver driver,
            PageSettings pageSettings,
            IPageObjectFactory pageObjectFactory)
            : base(driver, By.CssSelector(".admin-header-links"))
        {
            this.pageSettings = pageSettings;
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement LinkToAdministrationElement => WrappedElement.FindElement(LinkToAdministrationSelector);
        private IWebElement LinkToManagePageElement => WrappedElement.FindElement(LinkToManagePageSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Goes to the admin page.
        /// </summary>
        /// <returns></returns>
        public virtual HomePage GoToAdmin()
        {
            LinkToAdministrationElement.Click();

            return pageObjectFactory.PreparePage(
                new HomePage(
                    WrappedDriver,
                    pageSettings,
                    pageObjectFactory));
        }

        /// <summary>
        /// Checks if the 'Manage Page' link is available.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanManagePage()
        {
            return WrappedElement.FindElements(LinkToManagePageSelector).Any();
        }

        /// <summary>
        /// Navigates to the edit product page on the admin.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual PageObject ManagePage()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
