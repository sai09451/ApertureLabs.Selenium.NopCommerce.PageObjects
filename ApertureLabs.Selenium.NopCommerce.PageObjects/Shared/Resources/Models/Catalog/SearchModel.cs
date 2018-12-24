using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog
{
    /// <summary>
    /// SearchModel.
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// Query string
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Category ID
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// Include subcategories.
        /// </summary>
        public bool IncludeSubCategories { get; set; }

        /// <summary>
        /// Manufacturer ID
        /// </summary>
        public int Manufacturer { get; set; }

        /// <summary>
        /// Vendor ID
        /// </summary>
        public int Vendor { get; set; }

        /// <summary>
        /// Price - From 
        /// </summary>
        public string PriceFrom { get; set; }

        /// <summary>
        /// Price - To
        /// </summary>
        public string PriceTo { get; set; }

        /// <summary>
        /// A value indicating whether to search in descriptions
        /// </summary>
        public bool SearchInDescriptions { get; set; }

        /// <summary>
        /// A value indicating whether "advanced search" is enabled
        /// </summary>
        public bool AdvancedSearch { get; set; }

        /// <summary>
        /// A value indicating whether "allow search by vendor" is enabled
        /// </summary>
        public bool AllowSearchByVendor { get; set; }
    }
}
