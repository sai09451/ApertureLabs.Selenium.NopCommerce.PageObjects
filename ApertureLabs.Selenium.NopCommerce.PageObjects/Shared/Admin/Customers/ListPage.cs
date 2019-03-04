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
                By.CssSelector("#SearchCustomerRoleIds"),
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

        #endregion

        private KMultiSelectComponent<IListPage> CustomerRoles { get; }

        private DropDownComponent ExportDropDownComponent { get; }

        /// <summary>
        /// Gets the customers grid.
        /// </summary>
        /// <value>
        /// The customers grid.
        /// </value>
        public KGridComponent<IListPage> CustomersGrid { get; }

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
            CustomersGrid.Load();
            ExportDropDownComponent.Load();

            // Wait for the ajax loading element to toggle.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .TrySequentialWait(
                    out var exc,
                    d => basePage.IsAjaxBusy(),
                    d => !basePage.IsAjaxBusy());

            return this;
        }

        /// <summary>
        /// Navigates to the customer creation page.
        /// </summary>
        /// <returns></returns>
        public ICreatePage AddNew()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(AddNewElement, e => e.Click());

            return pageObjectFactory.PreparePage<ICreatePage>();
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public void BackToTop()
        {
            basePage.BackToTop();
        }

        /// <summary>
        /// Locates and selects the format to export to.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="downloadsPath">The downloads path.</param>
        /// <param name="expectedFileName">The expected file name.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <exception cref="NoSuchElementException"></exception>
        public void ExportTo(string type,
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
        public bool IsAjaxBusy()
        {
            return basePage.IsAjaxBusy();
        }

        /// <summary>
        /// Searches using the search model.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">searchModel</exception>
        public IListPage Search(CustomerSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            EmailElement.SetValue(searchModel.Email);
            FirstNameElement.SetValue(searchModel.FirstName);
            LastNameElement.SetValue(searchModel.LastName);

            DateOfBirthMonthElement.SelectByValue(
                searchModel.DateOfBirth.Month.ToString());

            DateOfBirthDayElement.SelectByValue(
                searchModel.DateOfBirth.Day.ToString());

            CompanyElement.SetValue(searchModel.Company);
            IpAddressElement.SetValue(searchModel.IpAddress);

            var currentlySelectedItems = CustomerRoles.GetSelectedOptions();

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

            // Page should reload.
            Load();

            return this;
        }

        /// <summary>
        /// Gets the listed customers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ListPageCustomerRowComponent> GetListedCustomers()
        {
            foreach (var row in CustomersGrid.Rows)
        }

        #endregion
    }
}
