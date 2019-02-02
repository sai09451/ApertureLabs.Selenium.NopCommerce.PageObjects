using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// Abstract class for any catalog search results page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public interface ISearchResultsTemplatePage : ICatalogTemplatePage
    {
        /// <summary>
        /// Gets the active view mode.
        /// </summary>
        /// <returns></returns>
        string GetActiveViewMode();

        /// <summary>
        /// Gets the current 'Display per page' value.
        /// </summary>
        /// <returns></returns>
        int GetCurrentDisplayOption();

        /// <summary>
        /// Gets the current sort option.
        /// </summary>
        /// <returns></returns>
        string GetCurrentSortOption();

        /// <summary>
        /// Gets all 'Display per page' options.
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> GetDisplayOptions();

        /// <summary>
        /// Retrieves the items listed.
        /// </summary>
        /// <returns></returns>
        IList<SearchResult> GetResults();

        /// <summary>
        /// Gets the sort options.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSortOptions();

        /// <summary>
        /// Gets the view modes.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetViewModes();

        /// <summary>
        /// Sets the active view mode.
        /// </summary>
        /// <param name="viewMode">The view mode.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SetActiveViewMode(string viewMode, StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Selects the 'Display per page' option.
        /// </summary>
        /// <param name="display">The display.</param>
        void SetDisplayOption(int display);

        /// <summary>
        /// Sets the sort options.
        /// </summary>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="stringComparison">The string comparison.</param>
        void SetSortOptions(string sortOption, StringComparison stringComparison = StringComparison.Ordinal);
    }
}