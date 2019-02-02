using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    public interface ISearchPanelComponent
    {
        SearchPanelComponent EnterProductName(string productName);
        IEditPage GoDirectlyToSku(string sku);
        IListPage Search();
        SearchPanelComponent SelectCategory(string categoryName, StringComparison stringComparison = StringComparison.Ordinal);
        SearchPanelComponent SelectProductType(string productType, StringComparison stringComparison = StringComparison.Ordinal);
        SearchPanelComponent SelectPublished(string publishedStatus, StringComparison stringComparison = StringComparison.Ordinal);
        SearchPanelComponent SelectStore(string storeName, StringComparison stringComparison = StringComparison.Ordinal);
        SearchPanelComponent SetSearchSubCategories(bool searchSubCats);
    }
}