using ApertureLabs.Selenium.Components.Boostrap.Navs;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin
{

    /// <summary>
    /// The interface for pages that have a 'tab' based interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasTabsPage<T> where T : IPageObject
    {
        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        NavsTabComponent<T> Tabs { get; }
    }
}
