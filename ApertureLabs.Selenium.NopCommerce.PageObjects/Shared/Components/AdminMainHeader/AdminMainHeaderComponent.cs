using System;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader
{
    /// <summary>
    /// Represents the navigational header on all admin pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader.IAdminMainHeaderComponent" />
    public class AdminMainHeaderComponent : PageComponent, IAdminMainHeaderComponent
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminMainHeaderComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public AdminMainHeaderComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        #endregion

        #region Methods

        public virtual void ClearCache()
        {
            throw new NotImplementedException();
        }

        public virtual IAdminMainHeaderComponent CollapseSidebar(bool collapse)
        {
            throw new NotImplementedException();
        }

        public virtual string GetCurrentUserName()
        {
            throw new NotImplementedException();
        }

        public virtual Admin.Home.IHomePage GoHome()
        {
            throw new NotImplementedException();
        }

        public virtual Public.Home.IHomePage PublicStore()
        {
            throw new NotImplementedException();
        }

        public virtual void RestartApplication()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
