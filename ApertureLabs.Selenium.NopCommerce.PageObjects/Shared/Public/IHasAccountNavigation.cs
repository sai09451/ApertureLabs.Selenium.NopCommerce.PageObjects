using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CustomerNavigation;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public
{
    /// <summary>
    /// IHasAccountNavigation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasAccountNavigation<T>
    {
        /// <summary>
        /// Gets the account navigation.
        /// </summary>
        /// <value>
        /// The account navigation.
        /// </value>
        CustomerNavigationComponent<T> AccountNavigation { get; }
    }
}
