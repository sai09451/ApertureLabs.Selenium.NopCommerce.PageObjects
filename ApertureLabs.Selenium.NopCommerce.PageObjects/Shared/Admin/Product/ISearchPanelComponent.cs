using ApertureLabs.Selenium.PageObjects;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// The interface for the search panel component on the admin product
    /// search page.
    /// </summary>
    public interface ISearchPanelComponent : IPageComponent
    {
        /// <summary>
        /// Enters the name of the product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns></returns>
        ISearchPanelComponent EnterProductName(string productName);

        /// <summary>
        /// Goes the directly to sku.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <returns></returns>
        IEditPage GoDirectlyToSku(string sku);

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns></returns>
        IListPage Search();

        /// <summary>
        /// Selects the category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        ISearchPanelComponent SelectCategory(string categoryName,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Selects the type of the product.
        /// </summary>
        /// <param name="productType">Type of the product.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        ISearchPanelComponent SelectProductType(string productType,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Selects the published.
        /// </summary>
        /// <param name="publishedStatus">The published status.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        ISearchPanelComponent SelectPublished(string publishedStatus,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Selects the store.
        /// </summary>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        ISearchPanelComponent SelectStore(string storeName,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Sets the search sub categories.
        /// </summary>
        /// <param name="searchSubCats">if set to <c>true</c> [search sub cats].</param>
        /// <returns></returns>
        ISearchPanelComponent SetSearchSubCategories(bool searchSubCats);
    }
}