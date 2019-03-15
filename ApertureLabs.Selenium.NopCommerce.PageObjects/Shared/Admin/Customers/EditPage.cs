using ApertureLabs.Selenium.Components.Boostrap.Navs;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// The admin customer edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers.IEditPage" />
    public class EditPage : ParameterPageObject,
        IEditPage
    {
        #region Fields

        #region Selectors

        private readonly By backToCustomerListSelector = By.CssSelector("*[href='/Admin/Customer/List']");
        private readonly By deleteCustomerSelector = By.CssSelector("#customer-delete");
        private readonly By deleteConfirmationSelector = By.CssSelector("#customermodel-Delete-delete-confirmation .modal-footer button[type='submit']");
        private readonly By saveSelector = By.CssSelector("*[name='save']");
        private readonly By saveAndContinueSelector = By.CssSelector("*[name='save-continue']");
        private readonly By impersonateSelector = By.CssSelector("*[name='impersonate']");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public EditPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                new Uri(pageSettings.BaseUrl, "/Admin"),
                new UriTemplate("Customer/Edit/{id}"))
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));

            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            InfoTab = new _CreateOrUpdateInfoComponent(
                By.CssSelector("#tab-info"),
                pageObjectFactory,
                WrappedDriver);

            OrdersTab = new _CreateOrUpdateOrdersComponent(
                By.CssSelector("#tab-orders"),
                pageObjectFactory,
                WrappedDriver);

            AddressesTab = new _CreateOrUpdateAddressesComponent(
                By.CssSelector("#tab-address"),
                pageObjectFactory,
                WrappedDriver);

            CurrentShoppingCartAndWishlistTab = new _CreateOrUpdateCurrentShoppingCartComponent(
                By.CssSelector("#tab-cart"),
                pageObjectFactory,
                WrappedDriver);

            ActivityLogTab = new _CreateOrUpdateActivityLogComponent(
                By.CssSelector("#tab-activitylog"),
                pageObjectFactory,
                driver);

            ImpersonateTab = new _CreateOrUpdateImpersonateComponent(
                By.CssSelector("#tab-impersonate"),
                pageObjectFactory,
                driver);

            Tabs = new NavsTabComponent<IEditPage>(
                By.CssSelector("#customer-edit"),
                new ILoadableComponent[]
                {
                    InfoTab,
                    OrdersTab,
                    AddressesTab,
                    CurrentShoppingCartAndWishlistTab,
                    ActivityLogTab,
                    ImpersonateTab
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

        private IWebElement BackToCustomerListElement => WrappedDriver
            .FindElement(backToCustomerListSelector);

        private IWebElement DeleteElement => WrappedDriver
            .FindElement(deleteCustomerSelector);

        private IWebElement DeleteConfirmationElement => WrappedDriver
            .FindElement(deleteConfirmationSelector);

        private IWebElement SaveElement => WrappedDriver
            .FindElement(saveSelector);

        private IWebElement SaveAndContinueElement => WrappedDriver
            .FindElement(saveAndContinueSelector);

        private IWebElement ImpersonateElement => WrappedDriver
            .FindElement(impersonateSelector);

        #endregion

        private _CreateOrUpdateInfoComponent InfoTab { get; }

        private _CreateOrUpdateOrdersComponent OrdersTab { get; }

        private _CreateOrUpdateAddressesComponent AddressesTab { get; }

        private _CreateOrUpdateCurrentShoppingCartComponent CurrentShoppingCartAndWishlistTab { get; }

        private _CreateOrUpdateActivityLogComponent ActivityLogTab { get; }

        private _CreateOrUpdateImpersonateComponent ImpersonateTab { get; }

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

        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        public virtual NavsTabComponent<IEditPage> Tabs { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the component. Checks to see if the current url matches
        /// the Route and if not an exception is thrown. If the WrappedDriver
        /// is an <see cref="T:OpenQA.Selenium.Support.Events.EventFiringWebDriver" /> event listeners will be
        /// added to the <see cref="E:OpenQA.Selenium.Support.Events.EventFiringWebDriver.Navigated" /> event
        /// which will call <see cref="M:ApertureLabs.Selenium.PageObjects.PageObject.Dispose" /> on this instance.
        /// NOTE:
        /// If overriding don't forget to either call base.Load() or make sure
        /// the <see cref="P:ApertureLabs.Selenium.PageObjects.PageObject.Uri" /> and the <see cref="P:ApertureLabs.Selenium.PageObjects.PageObject.WindowHandle" /> are
        /// assigned to.
        /// </summary>
        /// <returns>
        /// A reference to this
        /// <see cref="T:OpenQA.Selenium.Support.UI.ILoadableComponent" />.
        /// </returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            pageObjectFactory.PrepareComponent(basePage);
            pageObjectFactory.PrepareComponent(Tabs);
            pageObjectFactory.PrepareComponent(InfoTab);
            pageObjectFactory.PrepareComponent(OrdersTab);
            pageObjectFactory.PrepareComponent(AddressesTab);
            pageObjectFactory.PrepareComponent(CurrentShoppingCartAndWishlistTab);
            pageObjectFactory.PrepareComponent(ActivityLogTab);
            pageObjectFactory.PrepareComponent(ImpersonateTab);

            return this;
        }

        /// <summary>
        /// Returns to the customer list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IListPage BackToCustomerList()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .UntilPageReloads(BackToCustomerListElement, e => e.Click());

            return pageObjectFactory.PreparePage<IListPage>();
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IListPage Delete()
        {
            DeleteElement.Click();

            // Wait for confirmation modal to appear.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .Until(d => DeleteConfirmationElement.Displayed);

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .UntilPageReloads(DeleteConfirmationElement, e => e.Click());

            return pageObjectFactory.PreparePage<IListPage>();
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

        /// <summary>
        /// Saves the customer and returns to the customer list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IListPage Save()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(SaveElement, e => e.Click());

            return pageObjectFactory.PreparePage<IListPage>();
        }

        /// <summary>
        /// Saves the customer and continues to edit.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEditPage SaveAndContinueEdit()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .UntilPageReloads(SaveAndContinueElement, e => e.Click());

            return pageObjectFactory.PrepareComponent(this);
        }

        /// <summary>
        /// Impersonates the customer.
        /// </summary>
        /// <returns></returns>
        public virtual Public.Home.IHomePage ImpersonateCustomer()
        {
            Tabs.SelectTab("Place order (impersonate)");

            return ImpersonateTab.PlaceOrder();
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
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        #endregion
    }
}
