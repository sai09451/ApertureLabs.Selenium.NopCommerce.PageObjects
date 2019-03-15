using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApertureLabs.Selenium.Components.Boostrap.DropDown;
using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KDropDown;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Components.Kendo.KMultiSelect;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Customers;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// The admin customer list page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers.IListPage" />
    public class ListPage : StaticPageObject, IListPage
    {
        #region Fields

        #region Selectors

        private readonly By addNewSelector = By.CssSelector("*[href='/Admin/Customer/Create']");
        private readonly By exportToDropDownSelector = By.CssSelector("*[data-toggle='dropdown']");
        private readonly By exportDropDownOptionsSelector = By.CssSelector(".dropdown-menu li:not(.divider) > *:first-child");
        private readonly By emailSelector = By.CssSelector("#SearchEmail");
        private readonly By firstNameSelector = By.CssSelector("#SearchFirstName");
        private readonly By lastNameSelector = By.CssSelector("#SearchLastName");
        private readonly By dateOfBirthMonthSelector = By.CssSelector("#SearchMonthOfBirth");
        private readonly By dateOfBirthDaySelector = By.CssSelector("#SearchDayOfBirth");
        private readonly By companySelector = By.CssSelector("#SearchCompany");
        private readonly By ipAddressSelector = By.CssSelector("#SearchIpAddress");
        private readonly By searchSelector = By.CssSelector("#search-customers");

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
                  new Uri(pageSettings.BaseUrl, "Admin/Customer/List"))
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;

            CustomerRoles = new KMultiSelectComponent<IListPage>(
                By.CssSelector("#SelectedCustomerRoleIds"),
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);

            CustomersGrid = new KGridComponent<IListPage>(
                new BaseKendoConfiguration(),
                By.CssSelector("#customers-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);

            ExportDropDownComponent = new DropDownComponent(
                By.CssSelector(".btn-group"),
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddNewElement => WrappedDriver
            .FindElement(addNewSelector);

        private IWebElement ExportDropDownElement => WrappedDriver
            .FindElement(exportToDropDownSelector);

        private IReadOnlyCollection<IWebElement> ExportOptionElements => WrappedDriver
            .FindElements(exportDropDownOptionsSelector);

        private InputElement EmailElement => new InputElement(
            WrappedDriver.FindElement(
                emailSelector));

        private InputElement FirstNameElement => new InputElement(
            WrappedDriver.FindElement(
                firstNameSelector));

        private InputElement LastNameElement => new InputElement(
            WrappedDriver.FindElement(
                lastNameSelector));

        private SelectElement DateOfBirthMonthElement => new SelectElement(
            WrappedDriver.FindElement(
                dateOfBirthMonthSelector));

        private SelectElement DateOfBirthDayElement => new SelectElement(
            WrappedDriver.FindElement(
                dateOfBirthDaySelector));

        private InputElement CompanyElement => new InputElement(
            WrappedDriver.FindElement(
                companySelector));

        private InputElement IpAddressElement => new InputElement(
            WrappedDriver.FindElement(
                ipAddressSelector));

        private IWebElement SearchElement => WrappedDriver
            .FindElement(searchSelector);

        #endregion

        private KMultiSelectComponent<IListPage> CustomerRoles { get; }

        private DropDownComponent ExportDropDownComponent { get; }

        /// <summary>
        /// Gets the customers grid.
        /// </summary>
        /// <value>
        /// The customers grid.
        /// </value>
        public virtual KGridComponent<IListPage> CustomersGrid { get; }

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

            pageObjectFactory.PrepareComponent(basePage);

            // Wait for the ajax loading element to toggle.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .TrySequentialWait(
                    out var exc,
                    d => basePage.IsAjaxBusy(),
                    d => !basePage.IsAjaxBusy());

            pageObjectFactory.PrepareComponent(CustomersGrid);
            pageObjectFactory.PrepareComponent(ExportDropDownComponent);
            pageObjectFactory.PrepareComponent(CustomerRoles);

            return this;
        }

        /// <summary>
        /// Navigates to the customer creation page.
        /// </summary>
        /// <returns></returns>
        public virtual ICreatePage AddNew()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(AddNewElement, e => e.Click());

            return pageObjectFactory.PreparePage<ICreatePage>();
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
        }

        /// <summary>
        /// Locates and selects the format to export to. Will only work when
        /// running tests locally.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="downloadsPath">The downloads path.</param>
        /// <param name="expectedFileName">The expected file name.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual void ExportTo(string type,
            string downloadsPath,
            string expectedFileName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var fullPath = Path.Combine(downloadsPath, expectedFileName);
            var element = ExportDropDownComponent
                .Expand()
                .GetEnabledItems()
                .FirstOrDefault(e => String.Equals(
                    e.TextHelper().InnerText,
                    type,
                    StringComparison.Ordinal));

            if (element == null)
                throw new NoSuchElementException();

            element.Click();

            WrappedDriver
                .Wait(TimeSpan.FromMinutes(5))
                .Until(d => File.Exists(fullPath));
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
        /// Searches using the search model.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">searchModel</exception>
        public virtual IListPage Search(CustomerSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            EmailElement.SetValue(searchModel.Email);
            FirstNameElement.SetValue(searchModel.FirstName);
            LastNameElement.SetValue(searchModel.LastName);

            if (searchModel.DateOfBirth.HasValue)
            {
                DateOfBirthMonthElement.SelectByValue(
                    searchModel.DateOfBirth.Value.Month.ToString());

                DateOfBirthDayElement.SelectByValue(
                    searchModel.DateOfBirth.Value.Day.ToString());
            }
            else
            {
                DateOfBirthMonthElement.SelectByIndex(0);
                DateOfBirthDayElement.SelectByIndex(0);
            }

            CompanyElement.SetValue(searchModel.Company);
            IpAddressElement.SetValue(searchModel.IpAddress);

            var currentlySelectedItems = CustomerRoles.GetSelectedOptions();

            if (searchModel.CustomerRoles != null)
            {
                foreach (var opt in currentlySelectedItems)
                {
                    if (!searchModel.CustomerRoles?.Contains(opt) ?? true)
                        CustomerRoles.DeselectItem(opt);

                    // Check if there are items that have yet to be selected.
                    if (searchModel.CustomerRoles?.Except(currentlySelectedItems).Any() ?? false)
                    {
                        foreach (var _opt in searchModel.CustomerRoles)
                            CustomerRoles.SelectItem(_opt);
                    }
                }
            }

            SearchElement.Click();

            // Wait for the ajax indicator to toggle.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .TrySequentialWait(
                    out var exception,
                    d => IsAjaxBusy(),
                    d => !IsAjaxBusy());

            // Page should reload.
            Load();

            return this;
        }

        /// <summary>
        /// Gets the listed customers.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<ListPageCustomerRowComponent> GetListedCustomers()
        {
            var rowCount = CustomersGrid.GetNumberOfRows();

            for (var i = 0; i < rowCount; i++)
            {
                var rowEl = CustomersGrid.GetRow(i);

                yield return pageObjectFactory.PrepareComponent(
                    new ListPageCustomerRowComponent(
                        new ByElement(rowEl),
                        pageObjectFactory,
                        WrappedDriver));
            }
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
