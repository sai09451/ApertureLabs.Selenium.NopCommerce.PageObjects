using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Media;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog
{
    /// <summary>
    /// ProductDetailsModel
    /// </summary>
    public class ProductDetailsModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailsModel"/> class.
        /// </summary>
        public ProductDetailsModel()
        {
            PictureModels = new List<PictureModel>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [default picture zoom enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [default picture zoom enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool DefaultPictureZoomEnabled { get; set; }

        /// <summary>
        /// Gets or sets the default picture model.
        /// </summary>
        /// <value>
        /// The default picture model.
        /// </value>
        public PictureModel DefaultPictureModel { get; set; }

        /// <summary>
        /// Gets or sets the picture models.
        /// </summary>
        /// <value>
        /// The picture models.
        /// </value>
        public IList<PictureModel> PictureModels { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short description.
        /// </summary>
        /// <value>
        /// The short description.
        /// </value>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the full description.
        /// </summary>
        /// <value>
        /// The full description.
        /// </value>
        public string FullDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords.
        /// </summary>
        /// <value>
        /// The meta keywords.
        /// </value>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        /// <value>
        /// The meta description.
        /// </value>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title.
        /// </summary>
        /// <value>
        /// The meta title.
        /// </value>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the se.
        /// </summary>
        /// <value>
        /// The name of the se.
        /// </value>
        public string SeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sku is displayed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the sku is displayed; otherwise, <c>false</c>.
        /// </value>
        public bool ShowSku { get; set; }

        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show manufacturer part number].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show manufacturer part number]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowManufacturerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer part number.
        /// </summary>
        /// <value>
        /// The manufacturer part number.
        /// </value>
        public string ManufacturerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gtin is displayed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the gtin is displayed; otherwise, <c>false</c>.
        /// </value>
        public bool ShowGtin { get; set; }

        /// <summary>
        /// Gets or sets the gtin.
        /// </summary>
        /// <value>
        /// The gtin.
        /// </value>
        public string Gtin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the vendor is displayed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the vendor is displayed; otherwise, <c>false</c>.
        /// </value>
        public bool ShowVendor { get; set; }
    }
}
