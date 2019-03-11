using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Home;
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
        /// Determines whether this customer being impersonating.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is impersonating; otherwise, <c>false</c>.
        /// </returns>
        bool IsImpersonating();

        /// <summary>
        /// Goes to the admin page.
        /// </summary>
        /// <returns></returns>
        IHomePage GoToAdmin();

        /// <summary>
        /// Navigates to the edit product page on the admin.
        /// </summary>
        /// <returns></returns>
        Admin.Product.IEditPage ManagePage();

        /// <summary>
        /// Finishes the impersonation.
        /// </summary>
        /// <returns></returns>
        Admin.Customers.IEditPage FinishImpersonation();
    }
}