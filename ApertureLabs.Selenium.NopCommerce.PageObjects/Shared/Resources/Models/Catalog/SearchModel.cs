namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog
{
    /// <summary>
    /// SearchModel.
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// Query string.
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Category name.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Include subcategories.
        /// </summary>
        public bool IncludeSubCategories { get; set; }

        /// <summary>
        /// Manufacturer name.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Vendor name.
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// Price - From.
        /// </summary>
        public decimal? PriceFrom { get; set; }

        /// <summary>
        /// Price - To.
        /// </summary>
        public decimal? PriceTo { get; set; }

        /// <summary>
        /// A value indicating whether to search in descriptions.
        /// </summary>
        public bool SearchInDescriptions { get; set; }

        /// <summary>
        /// A value indicating whether "advanced search" is enabled.
        /// </summary>
        public bool AdvancedSearch { get; set; }

        /// <summary>
        /// A value indicating whether "allow search by vendor" is enabled.
        /// </summary>
        public bool AllowSearchByVendor { get; set; }
    }
}
