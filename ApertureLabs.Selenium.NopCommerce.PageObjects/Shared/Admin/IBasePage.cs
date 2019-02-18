using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin
{
    /// <summary>
    /// The base page for all admin pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IPageObject" />
    public interface IBasePage : IPageObject
    {
        /// <summary>
        /// Gets the main side bar.
        /// </summary>
        /// <value>
        /// The main side bar.
        /// </value>
        IAdminMainSideBarComponent MainSideBar { get; }

        /// <summary>
        /// Gets the navigation bar.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        IAdminMainHeaderComponent NavigationBar { get; }

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        AdminFooterComponent Footer { get; }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        void BackToTop();
    }
}