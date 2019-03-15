using System.Collections.Generic;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar
{
    /// <summary>
    /// The main side bar on all admin pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IPageComponent" />
    public interface IAdminMainSideBarComponent : IPageComponent
    {
        /// <summary>
        /// Searches the specified search term and goes to the first result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        T Search<T>(string searchTerm) where T : IPageObject;

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAdminMainSideBarNodeComponent> GetItems();
    }
}
