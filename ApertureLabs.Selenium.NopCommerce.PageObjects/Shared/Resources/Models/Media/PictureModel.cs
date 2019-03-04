namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Media
{
    /// <summary>
    /// The picture model used in the <see cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog.ProductDetailsModel"/>
    /// </summary>
    public class PictureModel
    {
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the thumb image URL.
        /// </summary>
        /// <value>
        /// The thumb image URL.
        /// </value>
        public string ThumbImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the full size image URL.
        /// </summary>
        /// <value>
        /// The full size image URL.
        /// </value>
        public string FullSizeImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the alternate text.
        /// </summary>
        /// <value>
        /// The alternate text.
        /// </value>
        public string AlternateText { get; set; }
    }
}
