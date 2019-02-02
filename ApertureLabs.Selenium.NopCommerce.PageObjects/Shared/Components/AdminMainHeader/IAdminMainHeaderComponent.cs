using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader
{
    /// <summary>
    /// The main header of all admin pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IPageComponent" />
    public interface IAdminMainHeaderComponent : IPageComponent
    {
        /// <summary>
        /// Collapses or expandes the sidebar depending on <c>collapse</c>.
        /// </summary>
        /// <param name="collapse">if set to <c>true</c> [collapse].</param>
        /// <returns></returns>
        IAdminMainHeaderComponent CollapseSidebar(bool collapse);

        /// <summary>
        /// Goes to the admin home page.
        /// </summary>
        /// <returns></returns>
        Admin.Home.IHomePage GoHome();

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserName();

        /// <summary>
        /// Goes to the public home page.
        /// </summary>
        /// <returns></returns>
        Public.Home.IHomePage PublicStore();

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Restarts the application.
        /// </summary>
        void RestartApplication();
    }
}
