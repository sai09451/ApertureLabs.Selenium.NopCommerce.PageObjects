using System;
using System.Linq;
using System.Threading;
using ApertureLabs.Selenium.Extensions;
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

        private readonly By sideBarToggleSelector = By.CssSelector(".sidebar-toggle");
        private readonly By logoSelector = By.CssSelector(".logo");
        private readonly By accountInfoSelector = By.CssSelector(".account-info");
        private readonly By dropDownToggleSelector = By.CssSelector(".dropdown");
        private readonly By restartApplicationSelector = By.CssSelector("*[action='/Admin/Common/RestartApplication']");
        private readonly By clearCacheSelector = By.CssSelector("*[action='/Admin/Common/ClearCache']");
        private readonly By publicStoreSelector = By.LinkText("Public store");

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

        private IWebElement SideBarToggleElement => WrappedElement
            .FindElement(sideBarToggleSelector);

        private IWebElement LogoElement => WrappedElement
            .FindElement(logoSelector);

        private IWebElement AccountInfoElement => WrappedElement
            .FindElement(accountInfoSelector);

        private IWebElement DropDownToggleElement => WrappedElement
            .FindElement(dropDownToggleSelector);

        private IWebElement ClearCacheElement => WrappedElement
            .FindElement(clearCacheSelector);

        private IWebElement RestartApplicationElement => WrappedElement
            .FindElement(restartApplicationSelector);

        private IWebElement PublicStoreElement => WrappedElement
            .FindElement(publicStoreSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public virtual void ClearCache()
        {
            ExpandDropDown();
            ClearCacheElement.Click();

            // Wait until stale.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => IsStale());

            Load();
        }

        /// <summary>
        /// Collapses or expandes the sidebar depending on <c>collapse</c>.
        /// </summary>
        /// <param name="collapse">if set to <c>true</c> [collapse].</param>
        /// <returns></returns>
        public virtual IAdminMainHeaderComponent CollapseSidebar(bool collapse)
        {
            if (collapse != IsSidebarCollapsed())
            {
                SideBarToggleElement.Click();

                // Unsure how to wait on this one. Classes added are added
                // before the transition finishes.
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return this;
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns></returns>
        public virtual string GetCurrentUserName()
        {
            return AccountInfoElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Goes to the admin home page.
        /// </summary>
        /// <returns></returns>
        public virtual Admin.Home.IHomePage GoHome()
        {
            LogoElement.Click();

            return pageObjectFactory.PreparePage<Admin.Home.IHomePage>();
        }

        /// <summary>
        /// Goes to the public home page.
        /// </summary>
        /// <returns></returns>
        public virtual Public.Home.IHomePage PublicStore()
        {
            PublicStoreElement.Click();

            return pageObjectFactory.PreparePage<Public.Home.IHomePage>();
        }

        /// <summary>
        /// Restarts the application.
        /// </summary>
        public virtual void RestartApplication()
        {
            ExpandDropDown();
            RestartApplicationElement.Click();

            // Wait until stale.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => IsStale());

            Load();
        }

        private void ExpandDropDown()
        {
            if (!DropDownToggleElement.Classes().Contains("open"))
                DropDownToggleElement.Click();
        }

        private bool IsSidebarCollapsed()
        {
            return !WrappedDriver
                .FindElements(By.CssSelector(".sidebar-collapse"))
                .Any();
        }

        #endregion

    }
}
