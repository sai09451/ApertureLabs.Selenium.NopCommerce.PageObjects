using System;
using ApertureLabs.Selenium.Components.Boostrap.Navs;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The order edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order.IEditPage" />
    public class EditPage : ParameterPageObject, IEditPage
    {
        #region Fields

        #region Selectors

        private readonly By backToOrderListSelector = By.CssSelector("*[href='/Admin/Order/List']");
        private readonly By invoiceSelector = By.CssSelector("*[href^='/Admin/Order/PdfInvoice?orderId']");
        private readonly By deleteSelector = By.CssSelector("#order-delete");

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
                  new Uri(pageSettings.BaseUrl, "Admin"),
                  new UriTemplate("Order/Edit/{id}"))
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;

            Info = new OrderDetailsInfoComponent(
                By.CssSelector("#tab-info"),
                WrappedDriver,
                this);

            Tabs = new NavsTabComponent<IEditPage>(
                By.CssSelector(".nav-tabs.nav"),
                WrappedDriver,
                new NavsTabComponentConfiguration(),
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement BackToOrderListElement => WrappedDriver
            .FindElement(backToOrderListSelector);

        private IWebElement InvoiceElement => WrappedDriver
            .FindElement(invoiceSelector);

        private IWebElement DeleteElement => WrappedDriver
            .FindElement(deleteSelector);

        #endregion

        /// <summary>
        /// Gets the information tab.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        public OrderDetailsInfoComponent Info { get; }

        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        public NavsTabComponent<IEditPage> Tabs { get; }

        /// <summary>
        /// Gets the main side bar.
        /// </summary>
        /// <value>
        /// The main side bar.
        /// </value>
        public IAdminMainSideBarComponent MainSideBar => basePage.MainSideBar;

        /// <summary>
        /// Gets the navigation bar.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        public IAdminMainHeaderComponent NavigationBar => basePage.NavigationBar;

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public AdminFooterComponent Footer => basePage.Footer;

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
            basePage.Load();
            Tabs.Load();
            Info.Load();

            // Wait for the loading indicator.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .TrySequentialWait(
                    out var exc,
                    d => basePage.IsAjaxBusy(),
                    d => !basePage.IsAjaxBusy());

            return this;
        }

        /// <summary>
        /// Backs to order list.
        /// </summary>
        /// <returns></returns>
        public IListPage BackToOrderList()
        {
            var backToOrderListEl = BackToOrderListElement;
            backToOrderListEl.Click();

            // Wait until the element is stale.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .Until(d => backToOrderListEl.IsStale());

            return pageObjectFactory.PreparePage<IListPage>();
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public void BackToTop()
        {
            basePage.BackToTop();
        }

        /// <summary>
        /// Determines whether the ajax busy element is present and visible.
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is ajax busy]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAjaxBusy()
        {
            return basePage.IsAjaxBusy();
        }

        /// <summary>
        /// Downloads an invoice of the order.
        /// </summary>
        public void InvoicePdf()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .UntilPageReloads(InvoiceElement, e => e.Click());
        }

        /// <summary>
        /// Deletes this order.
        /// </summary>
        /// <returns></returns>
        public IListPage Delete()
        {
            var deleteEl = DeleteElement;
            deleteEl.Click();

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .Until(d => deleteEl.IsStale());

            return pageObjectFactory.PreparePage<IListPage>();
        }

        #endregion

    }
}
