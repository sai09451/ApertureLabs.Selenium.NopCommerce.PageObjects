using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// IAddressesPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public interface IAddressesPage : IBasePage,
        IHasAccountNavigation<IAddressesPage>
    {
        /// <summary>
        /// Begins the process of adding a new address.
        /// </summary>
        /// <returns></returns>
        IAddressesAddPage AddNew();

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <returns></returns>
        IEnumerable<AddressesRowComponent<IAddressesPage>> GetAddresses();
    }
}
