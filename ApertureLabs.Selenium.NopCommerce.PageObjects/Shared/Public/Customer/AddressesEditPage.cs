using System;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CustomerNavigation;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// AddressesEditPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer.AddressesCreateOrUpdatePage{T}" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer.IAddressesEditPage" />
    public class AddressesEditPage : AddressesCreateOrUpdatePage<IAddressesEditPage>,
        IAddressesEditPage
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressesEditPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public AddressesEditPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(basePage,
                  pageObjectFactory,
                  driver,
                  pageSettings)
        {
            Uri = new Uri(
                new Uri(pageSettings.BaseUrl),
                "customer/addressedit/");

            AccountNavigation = new CustomerNavigationComponent<IAddressesEditPage>(
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion
    }
}
