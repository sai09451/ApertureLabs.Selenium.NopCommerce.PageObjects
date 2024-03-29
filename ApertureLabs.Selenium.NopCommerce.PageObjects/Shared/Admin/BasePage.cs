using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin
{
    /// <summary>
    /// Base page for all admin pages.
    /// </summary>
    public class BasePage : BasePageObject, IBasePage
    {
        #region Fields

        #region Selectors

        private readonly By backToTopSelector = By.CssSelector("#backTop");
        private readonly By mainSideBarSelector = By.CssSelector(".main-sidebar");
        private readonly By navigationBarSelector = By.CssSelector(".main-header");
        private readonly By footerSelector = By.CssSelector(".main-footer");
        private readonly By ajaxBusySelector = By.CssSelector("#ajaxBusy");
        private readonly By alertSelector = By.CssSelector(".alert-dismissable");
        private readonly By alertDismissSelector = By.CssSelector(".alert-dismissable .close");

        #endregion

        private readonly PageSettings pageSettings;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public BasePage(IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver)
        {
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the main side bar which is used to navigate bewtween the
        /// admin.
        /// components.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        public virtual IAdminMainSideBarComponent MainSideBar { get; private set; }

        /// <summary>
        /// Gets the navigation bar.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        public virtual IAdminMainHeaderComponent NavigationBar { get; private set; }

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public virtual AdminFooterComponent Footer { get; private set; }

        #region Elements

        private IWebElement BackToTopElement => WrappedDriver
            .FindElement(backToTopSelector);

        private IWebElement AjaxBusyElement => WrappedDriver
            .FindElement(ajaxBusySelector);

        private IReadOnlyCollection<IWebElement> AlertElements => WrappedDriver
            .FindElements(alertSelector);

        private IReadOnlyCollection<IWebElement> AlertDismissElements => WrappedDriver
            .FindElements(alertDismissSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// If overridding this don't forget to call base.Load().
        /// NOTE: Will navigate to the pages url if the current drivers url
        /// is empty.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// If the driver is an EventFiringWebDriver an event listener will
        /// be added to the 'Navigated' event and uses the url to determine
        /// if the page is 'stale'.
        /// </remarks>
        public override ILoadableComponent Load()
        {
            base.Load();

            if (WrappedDriver.FindElements(mainSideBarSelector).Any())
            {
                MainSideBar = pageObjectFactory.PrepareComponent(
                    new AdminMainSideBarComponent(
                        mainSideBarSelector,
                        pageObjectFactory,
                        WrappedDriver));
            }
            else
            {
                MainSideBar = null;
            }

            if (WrappedDriver.FindElements(navigationBarSelector).Any())
            {
                NavigationBar = pageObjectFactory.PrepareComponent(
                    new AdminMainHeaderComponent(
                        navigationBarSelector,
                        pageObjectFactory,
                        WrappedDriver));
            }
            else
            {
                NavigationBar = null;
            }

            if (WrappedDriver.FindElements(footerSelector).Any())
            {
                Footer = pageObjectFactory.PrepareComponent(
                    new AdminFooterComponent(WrappedDriver));
            }
            else
            {
                Footer = null;
            }

            return this;
        }

        /// <summary>
        /// Backs to top.
        /// </summary>
        public virtual void BackToTop()
        {
            if (BackToTopElement.Displayed)
            {
                BackToTopElement.Click();

                // Wait until the element is no longer displayed.
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(5))
                    .Until(d => !BackToTopElement.Displayed);
            }
        }

        /// <summary>
        /// Determines whether the ajax busy element is present and visible.
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is ajax busy]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsAjaxBusy()
        {
            return AjaxBusyElement.Displayed;
        }

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasNotifications()
        {
            return AlertElements.Any();
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void HandleNotification(Action<IWebElement> element)
        {
            foreach (var el in AlertElements)
                element(el);
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            foreach (var el in AlertDismissElements)
                el.Click();
        }

        #endregion
    }
}