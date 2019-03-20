using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog
{
    /// <summary>
    /// Represents the tier pricing info used in the admin edit product >
    /// general info tab > tier pricing.
    /// </summary>
    public class TierPriceModel
    {
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public string Store { get; set; }

        /// <summary>
        /// Gets or sets the customer role.
        /// </summary>
        /// <value>
        /// The customer role.
        /// </value>
        public string CustomerRole { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }
    }
}
