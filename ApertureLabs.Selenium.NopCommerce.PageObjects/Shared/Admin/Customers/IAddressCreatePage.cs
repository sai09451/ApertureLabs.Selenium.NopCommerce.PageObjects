using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Represents the AddressCreate.cshtml page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.IBasePage" />
    public interface IAddressCreatePage : IBasePage
    {
        /// <summary>
        /// Enters the address.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        IAddressCreatePage EnterAddress(AddressModel model);

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <returns></returns>
        AddressModel GetAddress();

        /// <summary>
        /// Returns to the customer details.
        /// </summary>
        /// <returns></returns>
        IEditPage BackToCustomerDetails();

        /// <summary>
        /// Saves the address. Should check for any notifications after
        /// calling this method.
        /// </summary>
        /// <returns></returns>
        IAddressCreatePage Save();
    }
}
