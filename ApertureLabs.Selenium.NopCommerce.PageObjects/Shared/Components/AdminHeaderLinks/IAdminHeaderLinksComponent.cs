using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks
{
    /// <summary>
    /// Represents the '.admin-header-links' element on the public pages. This
    /// should only appear if the current customer is admin accessible.
    /// </summary>
    public interface IAdminHeaderLinksComponent : IPageComponent
    {
        /// <summary>
        /// Checks if the 'Manage Page' link is available.
        /// </summary>
        /// <returns></returns>
        bool CanManagePage();

        /// <summary>
        /// Goes to the admin page.
        /// </summary>
        /// <returns></returns>
        IHomePage GoToAdmin();

        /// <summary>
        /// Navigates to the edit product page on the admin.
        /// </summary>
        /// <returns></returns>
        IEditPage ManagePage();
    }
}