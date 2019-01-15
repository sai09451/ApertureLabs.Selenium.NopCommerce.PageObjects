namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings
{
    /// <summary>
    /// Defines how to interact with the component.
    /// </summary>
    public class EditorSettingsComponentConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether too use the keyboard
        /// instead of the mouse to interact with the component.
        /// </summary>
        public bool UseKeyboardInsteadOfMouseToInteract { get; set; }

        /// <summary>
        /// Returns the default configuration.
        /// </summary>
        /// <returns></returns>
        public static EditorSettingsComponentConfiguration DefaultConfiguration()
        {
            return new EditorSettingsComponentConfiguration
            {
                UseKeyboardInsteadOfMouseToInteract = false
            };
        }
    }
}
