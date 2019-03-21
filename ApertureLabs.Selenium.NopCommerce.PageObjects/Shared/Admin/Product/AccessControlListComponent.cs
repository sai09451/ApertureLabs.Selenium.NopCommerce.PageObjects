using ApertureLabs.Selenium.Components.Kendo.KMultiSelect;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The 'Access control list' component on the product info tab of the
    /// admin product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class AccessControlListComponent : PageComponent
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessControlListComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public AccessControlListComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        {
            CustomerRolesComponent = new KMultiSelectComponent<AccessControlListComponent>(
                By.CssSelector("#SelectedCustomerRoleIds"),
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);
        }

        #endregion

        #region Properties

        private KMultiSelectComponent<AccessControlListComponent> CustomerRolesComponent { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the customer roles.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetCustomerRoles()
        {
            return CustomerRolesComponent.GetSelectedOptions();
        }

        /// <summary>
        /// Gets the available customer roles.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableCustomerRoles()
        {
            return CustomerRolesComponent.GetAllOptions();
        }

        /// <summary>
        /// Sets the customer roles.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        public virtual AccessControlListComponent SetCustomerRoles(
            IEnumerable<string> roles)
        {
            var itemsSelected = CustomerRolesComponent.GetSelectedOptions();
            var deselectItems = itemsSelected.Except(roles);
            var itemsToSelect = roles.Except(itemsSelected);

            foreach (var item in deselectItems)
                CustomerRolesComponent.DeselectItem(item);

            foreach (var item in itemsToSelect)
                CustomerRolesComponent.SelectItem(item);

            return this;
        }

        #endregion

    }
}
