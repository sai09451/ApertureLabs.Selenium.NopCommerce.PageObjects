using ApertureLabs.Selenium.Components.Boostrap.Navs;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// Represents the _CreateOrUpdate.cshtml page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.BasePageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IHasTabsPage{T}" />
    public partial class _CreateOrUpdatePage : BasePageObject,
        IBasePage,
        IHasTabsPage<_CreateOrUpdatePage>
    {
        #region Fields

        #region Selectors

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdatePage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">
        /// basePage
        /// or
        /// pageObjectFactory
        /// </exception>
        public _CreateOrUpdatePage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver)
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            Tabs = new NavsTabComponent<_CreateOrUpdatePage>(
                By.CssSelector("#product-form"),
                new ILoadableComponent[]
                {

                },
                WrappedDriver,
                new NavsTabComponentConfiguration
                {
                    ActiveTabContentElementSelector = By.CssSelector(".tab-content .tab-pane.active"),
                    ActiveTabHeaderElementSelector = By.CssSelector(".nav-tabs li.active"),
                    ActiveTabHeaderNameSelector = By.CssSelector(".nav-tabs li.active"),
                    TabContentElementsSelector = By.CssSelector(".tab-content .tab-pane"),
                    TabHeaderElementsSelector = By.CssSelector(".nav-tabs li"),
                    TabHeaderNamesSelector = By.CssSelector(".nav-tabs li")
                },
                this);
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        public virtual NavsTabComponent<_CreateOrUpdatePage> Tabs { get; }

        /// <summary>
        /// Gets the main side bar.
        /// </summary>
        /// <value>
        /// The main side bar.
        /// </value>
        public virtual IAdminMainSideBarComponent MainSideBar => basePage.MainSideBar;

        /// <summary>
        /// Gets the navigation bar.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        public virtual IAdminMainHeaderComponent NavigationBar => basePage.NavigationBar;

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public virtual AdminFooterComponent Footer => basePage.Footer;

        #endregion

        #region Methods

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasNotifications()
        {
            return basePage.HasNotifications();
        }

        /// <summary>
        /// Determines whether the ajax busy element is present and visible.
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is ajax busy]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsAjaxBusy()
        {
            return basePage.IsAjaxBusy();
        }

        #endregion
    }
}
