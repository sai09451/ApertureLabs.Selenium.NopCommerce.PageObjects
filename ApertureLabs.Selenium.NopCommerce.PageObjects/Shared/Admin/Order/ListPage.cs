using System;
using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Orders;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    /// <summary>
    /// The order list page.
    /// </summary>
    public class ListPage : PageObject, IListPage
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

        public ListPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver)
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

        public IAdminMainSideBarComponent MainSideBar => basePage.MainSideBar;

        public IAdminMainHeaderComponent NavigationBar => basePage.NavigationBar;

        public AdminFooterComponent Footer => basePage.Footer;

        public KGridComponent<IListPage> Orders { get; }

        #endregion

        #region Methods

        public override ILoadableComponent Load()
        {
            base.Load();
            basePage.Load();
            Orders.Load();

            return this;
        }

        public void BackToTop()
        {
            basePage.BackToTop();
        }

        public bool IsAjaxBusy()
        {
            return basePage.IsAjaxBusy();
        }

        public IListPage Search(OrderSearchModel orderSearchModel)
        {
            throw new NotImplementedException();
        }

        public IEditPage GoDirectlyToOrderNumber(int orderNumber)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
