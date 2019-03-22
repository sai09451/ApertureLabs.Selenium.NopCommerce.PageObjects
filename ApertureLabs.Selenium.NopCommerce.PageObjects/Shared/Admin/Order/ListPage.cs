using System;
using System.Linq;
using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KDatePicker;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Components.Kendo.KMultiSelect;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Orders;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
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
        private readonly By storeSelector = By.CssSelector("#StoreId");
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
        private readonly IPageObjectFactory pageObjectFactory;

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
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));

            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            StartDateComponent = new KDatePickerComponent<IListPage>(
                new KDatePickerConfiguration(),
                startDateSelector,
                WrappedDriver,
                this);

            EndDateComponent = new KDatePickerComponent<IListPage>(
                new KDatePickerConfiguration(),
                endDateSelector,
                WrappedDriver,
                this);

            Orders = new KGridComponent<IListPage>(
                new BaseKendoConfiguration(),
                By.CssSelector("#orders-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);

            OrderStatusesComponent = new KMultiSelectComponent<IListPage>(
                orderStatusesSelector,
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);

            PaymentStatusesComponent = new KMultiSelectComponent<IListPage>(
                paymentStatusesSelector,
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);

            ShippingStatusesComponent = new KMultiSelectComponent<IListPage>(
                shippingStatusesSelector,
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement ProductNameElement => new InputElement(
            WrappedDriver.FindElement(
                productNameSelector));

        private SelectElement StoreElement => new SelectElement(
            WrappedDriver.FindElement(
                storeSelector));

        private SelectElement VendorElement => new SelectElement(
            WrappedDriver.FindElement(
                vendorSelector));

        private InputElement BillingPhoneNumberElement => new InputElement(
            WrappedDriver.FindElement(
                billingPhoneNumberSelector));

        private InputElement BillingEmailAddressElement => new InputElement(
            WrappedDriver.FindElement(
                billingEmailAddressSelector));

        private InputElement BillingLastNameElement => new InputElement(
            WrappedDriver.FindElement(
                billingLastNameSelector));

        private SelectElement BillingCountryElement => new SelectElement(
            WrappedDriver.FindElement(
                billingCountrySelector));

        private SelectElement PaymentMethodElement => new SelectElement(
            WrappedDriver.FindElement(
                paymentMethodSelector));

        private InputElement OrderNotesElement => new InputElement(
            WrappedDriver.FindElement(
                orderNotesSelector));

        private InputElement GoDirectlyToOrderNumberElement => new InputElement(
            WrappedDriver.FindElement(
                goDirectlyToOrderNumberSelector));

        private IWebElement GoDirectlyToOrderNumberSubmitElement => WrappedDriver
            .FindElement(goDirectlyToOrderNumberSubmitSelector);

        #endregion

        private KDatePickerComponent<IListPage> StartDateComponent { get; }

        private KDatePickerComponent<IListPage> EndDateComponent { get; }

        private KMultiSelectComponent<IListPage> OrderStatusesComponent { get; }

        private KMultiSelectComponent<IListPage> PaymentStatusesComponent { get; }

        private KMultiSelectComponent<IListPage> ShippingStatusesComponent { get; }

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
        /// Gets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public virtual KGridComponent<IListPage> Orders { get; }

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
            StartDateComponent.Load();
            EndDateComponent.Load();
            OrderStatusesComponent.Load();
            PaymentStatusesComponent.Load();
            ShippingStatusesComponent.Load();

            return this;
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
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
        /// Searches the specified order search model.
        /// </summary>
        /// <param name="orderSearchModel">The order search model.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IListPage Search(OrderSearchModel orderSearchModel)
        {
            StartDateComponent.SetValue(orderSearchModel.StartDate);
            EndDateComponent.SetValue(orderSearchModel.EndDate);
            ProductNameElement.SetValue(orderSearchModel.ProductName);
            OrderStatusesComponent.SelectOptions(orderSearchModel.OrderStatuses);
            PaymentStatusesComponent.SelectOptions(orderSearchModel.PaymentStatuses);
            ShippingStatusesComponent.SelectOptions(orderSearchModel.ShippingStatuses);

            if (!String.IsNullOrEmpty(orderSearchModel.StoreName))
            {
                var storeIndex = StoreElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    orderSearchModel.StoreName,
                    StringComparison.Ordinal));

                if (storeIndex == -1)
                    throw new NoSuchElementException();

                StoreElement.SelectByIndex(storeIndex);
            }

            if (!String.IsNullOrEmpty(orderSearchModel.VendorName))
            {
                var vendorIndex = StoreElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    orderSearchModel.VendorName,
                    StringComparison.Ordinal));

                if (vendorIndex == -1)
                    throw new NoSuchElementException();

                VendorElement.SelectByIndex(vendorIndex);
            }

            BillingPhoneNumberElement.SetValue(orderSearchModel.BillingPhone);
            BillingEmailAddressElement.SetValue(orderSearchModel.BillingEmail);
            BillingLastNameElement.SetValue(orderSearchModel.BillingLastName);

            if (!String.IsNullOrEmpty(orderSearchModel.BillingCountryName))
            {
                var billingCountryIndex = BillingCountryElement.Options.IndexOf(
                    el => String.Equals(
                        el.TextHelper().InnerText,
                        orderSearchModel.BillingCountryName,
                        StringComparison.Ordinal));

                if (billingCountryIndex == -1)
                    throw new NoSuchElementException();

                BillingCountryElement.SelectByIndex(billingCountryIndex);
            }

            if (!String.IsNullOrEmpty(orderSearchModel.PaymentMethodName))
            {
                var paymentMethodIndex = PaymentMethodElement.Options.IndexOf(
                    el => String.Equals(
                        el.TextHelper().InnerText,
                        orderSearchModel.PaymentMethodName,
                        StringComparison.Ordinal));

                if (paymentMethodIndex == -1)
                    throw new NoSuchElementException();

                PaymentMethodElement.SelectByIndex(paymentMethodIndex);
            }

            OrderNotesElement.SetValue(orderSearchModel.OrderNotes);

            return this;
        }

        /// <summary>
        /// Goes the directly to order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IEditPage GoDirectlyToOrderNumber(int orderNumber)
        {
            GoDirectlyToOrderNumberElement.SetValue(orderNumber);
            GoDirectlyToOrderNumberSubmitElement.Click();

            return pageObjectFactory.PreparePage<IEditPage>();
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
