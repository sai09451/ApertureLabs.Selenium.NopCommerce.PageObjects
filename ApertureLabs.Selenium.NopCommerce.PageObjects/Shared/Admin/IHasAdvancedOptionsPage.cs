using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin
{
    /// <summary>
    /// Standard way of declaring that a page has the 'Advanced' and 'Settings'
    /// options available to them.
    /// </summary>
    public interface IHasAdvancedOptionsPage
    {
        /// <summary>
        /// Gets the advanced options component.
        /// </summary>
        /// <value>
        /// The advanced options component.
        /// </value>
        EditorSettingsComponent AdvancedOptions { get; }
    }
}
