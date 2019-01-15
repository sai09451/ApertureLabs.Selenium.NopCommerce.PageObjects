namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.HeaderLinks
{
    /// <summary>
    /// HeaderLinksModel.
    /// </summary>
    public class HeaderLinksModel
    {
        /// <summary>
        /// IsAuthenticated.
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// CustomerName.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// ShoppingCartEnabled.
        /// </summary>
        public bool ShoppingCartEnabled { get; set; }

        /// <summary>
        /// ShoppingCartItems.
        /// </summary>
        public int ShoppingCartItems { get; set; }

        /// <summary>
        /// WishlistEnabled.
        /// </summary>
        public bool WishlistEnabled { get; set; }

        /// <summary>
        /// WishlistItems.
        /// </summary>
        public int WishlistItems { get; set; }

        /// <summary>
        /// AllowPrivateMessages.
        /// </summary>
        public bool AllowPrivateMessages { get; set; }

        /// <summary>
        /// UnreadPrivateMessages.
        /// </summary>
        public string UnreadPrivateMessages { get; set; }

        /// <summary>
        /// AlertMessage.
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }
    }
}
