using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks
{
    /// <summary>
    /// Represents the '.admin-header-links' element on the public pages. This
    /// should only appear if the current customer is admin accessible.
    /// </summary>
    public class AdminHeaderLinksComponent : PageComponent, IAdminHeaderLinksComponent
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
        /// Initializes a new instance of the <see cref="AdminHeaderLinksComponent"/> class.
        /// </summary>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public AdminHeaderLinksComponent(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(By.CssSelector(".admin-header-links"), driver)
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
        public virtual IHomePage GoToAdmin()
        {
            LinkToAdministrationElement.Click();

            return pageObjectFactory.PreparePage<HomePage>();
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
        public virtual IEditPage ManagePage()
        {
            LinkToManagePageElement.Click();

            return pageObjectFactory.PreparePage<EditPage>();
        }

        #endregion
    }
}
