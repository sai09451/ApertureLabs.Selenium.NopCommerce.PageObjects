using System;
using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Orders;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The order list page.
    /// </summary>
    public class ListPage : StaticPageObject, IListPage
    {
        #region Fields

        #region Selectors

        private readonly By startDateSelector = By.CssSelector("#StartDate");
        private readonly By endDateSelector = By.CssSelector("#EndDate");
        private readonly By productNameSelector = By.CssSelector("#search-product-name");
        private readonly By orderStatusesSelector = By.CssSelector("#OrderStatusIds");
        private readonly By paymentStatusesSelector = By.CssSelector("#PaymentStatusIds");
        private readonly By shippingStatusesSelector = By.CssSelector("#ShippingStatusIds");
        private readonly By vendorSelector = By.CssSelector("#VendorId");
        private readonly By billingPhoneNumberSelector = By.CssSelector("#BillingPhone");
        private readonly By billingEmailAddressSelector = By.CssSelector("#BillingEmail");
        private readonly By billingLastNameSelector = By.CssSelector("#BillingLastName");
        private readonly By billingCountrySelector = By.CssSelector("#BillingCountryId");
        private readonly By paymentMethodSelector = By.CssSelector("#PaymentMethodSystemName");
        private readonly By orderNotesSelector = By.CssSelector("#OrderNotes");
        private readonly By goDirectlyToOrderNumberSelector = By.CssSelector("#GoDirectlyToCustomOrderNumber");
        private readonly By goDirectlyToOrderNumberSubmitSelector = By.CssSelector("#go-to-order-by-number");

        #endregion

        private readonly IBasePage basePage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public ListPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  new Uri(pageSettings.BaseUrl, "Admin/Order/List"))
        {
            this.basePage = basePage;

            Orders = new KGridComponent<IListPage>(
                BaseKendoConfiguration.DefaultBaseKendoOptions(),
                By.CssSelector("#orders-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement StartDateElement => WrappedDriver
            .FindElement(startDateSelector);

        private IWebElement EndDateElement => WrappedDriver
            .FindElement(endDateSelector);

        private IWebElement ProductNameElement => WrappedDriver
            .FindElement(productNameSelector);

        private IWebElement OrderStatusesElement => WrappedDriver
            .FindElement(orderStatusesSelector);

        private IWebElement PaymentStatusesElement => WrappedDriver
            .FindElement(paymentStatusesSelector);

        private IWebElement ShippingStatusesElement => WrappedDriver
            .FindElement(shippingStatusesSelector);

        private IWebElement VendorElement => WrappedDriver
            .FindElement(vendorSelector);

        private IWebElement BillingPhoneNumberElement => WrappedDriver
            .FindElement(billingPhoneNumberSelector);

        private IWebElement BillingEmailAddressElement => WrappedDriver
            .FindElement(billingEmailAddressSelector);

        private IWebElement BillingLastNameElement => WrappedDriver
            .FindElement(billingLastNameSelector);

        private IWebElement PaymentMethodElement => WrappedDriver
            .FindElement(paymentMethodSelector);

        private IWebElement OrderNotesElement => WrappedDriver
            .FindElement(orderNotesSelector);

        private IWebElement GoDirectlyToOrderNumberElement => WrappedDriver
            .FindElement(goDirectlyToOrderNumberSelector);

        private IWebElement GoDirectlyToOrderNumberSubmitElement => WrappedDriver
            .FindElement(goDirectlyToOrderNumberSubmitSelector);

        #endregion

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

        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public KGridComponent<IListPage> Orders { get; }

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
            basePage.Load();
            Orders.Load();

            return this;
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
        /// Searches the specified order search model.
        /// </summary>
        /// <param name="orderSearchModel">The order search model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IListPage Search(OrderSearchModel orderSearchModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Goes the directly to order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEditPage GoDirectlyToOrderNumber(int orderNumber)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
