using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Abstract base class for the Catalog pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home.HomePage" />
    public interface ICatalogTemplatePage : IBasePage
    {
        /// <summary>
        /// Selects the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        IProductsByCategoryPage SelectCategory(string category,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Selects a parent category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        IParentCategoryPage SelectParentCategory(string category,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetCategories();

        /// <summary>
        /// Gets the current category.
        /// </summary>
        /// <returns></returns>
        string GetCurrentCategory();

        /// <summary>
        /// Gets the manufacturers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetManufacturers();

        /// <summary>
        /// Gets the popular tags.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetPopularTags();

        /// <summary>
        /// Gets the recently viewed products.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetRecentlyViewedProducts();

        /// <summary>
        /// Selects the manufacturers.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SelectManufacturers(string manufacturer, StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Selects the popular tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SelectPopularTag(string tag, StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Selects the recently viewed products.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SelectRecentlyViewedProducts(string productName, StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Views all manufacturers.
        /// </summary>
        void ViewAllManufacturers();

        /// <summary>
        /// Views all popular tags.
        /// </summary>
        /// <returns></returns>
        ProductTagsAllPage ViewAllPopularTags();
    }
}