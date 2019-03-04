using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models
{
    /// <summary>
    /// Used to configure PageObjects.
    /// </summary>
    public class PageSettings
    {
        #region Constructor(s)

        /// <summary>
        /// Default ctor. Need to manually set all of the properties.
        /// </summary>
        public PageSettings()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// The BaseUrl of the site.
        /// </summary>
        /// <example>http://localhost:15536</example>
        public Uri BaseUrl { get; set; }

        /// <summary>
        /// Gets the admin base URL.
        /// </summary>
        /// <value>
        /// The admin base URL.
        /// </value>
        public Uri AdminBaseUrl => new Uri(BaseUrl, "Admin");

        #endregion
    }
}
