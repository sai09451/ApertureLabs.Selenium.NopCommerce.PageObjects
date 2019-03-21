using ApertureLabs.Selenium.Components.Kendo.KMultiSelect;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// Represents the 'Mapping' component on the product info tab of the
    /// admin product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class MappingComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By categoriesSelector = By.CssSelector("#SelectedCategoryIds");
        private readonly By manufacturersSelector = By.CssSelector("#SelectedManufacturerIds");
        private readonly By limitedToStoresSelector = By.CssSelector("#SelectedStoreIds");
        private readonly By vendorSelector = By.CssSelector("#VendorId");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public MappingComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        {
            CategoriesComponent = new KMultiSelectComponent<MappingComponent>(
                categoriesSelector,
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);

            ManufacturerComponent = new KMultiSelectComponent<MappingComponent>(
                manufacturersSelector,
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);

            LimitedToStoresComponent = new KMultiSelectComponent<MappingComponent>(
                limitedToStoresSelector,
                WrappedDriver,
                new KMultiSelectConfiguration(),
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private SelectElement VendorElement => new SelectElement(
            WrappedElement.FindElement(
                vendorSelector));

        #endregion

        private KMultiSelectComponent<MappingComponent> CategoriesComponent { get; }

        private KMultiSelectComponent<MappingComponent> ManufacturerComponent { get; }

        private KMultiSelectComponent<MappingComponent> LimitedToStoresComponent { get; }

        #endregion

        #region Methods

        /// <summary>
        /// If overriding don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            CategoriesComponent.Load();
            ManufacturerComponent.Load();
            LimitedToStoresComponent.Load();

            return this;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetCategories()
        {
            return CategoriesComponent.GetSelectedOptions();
        }

        /// <summary>
        /// Gets the available options for categories.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForCategories()
        {
            return CategoriesComponent.GetAllOptions();
        }

        /// <summary>
        /// Sets the categories.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <returns></returns>
        public virtual MappingComponent SetCategories(IEnumerable<string> categories)
        {
            var selectedItems = CategoriesComponent.GetSelectedOptions();
            var deselectedItems = selectedItems.Except(categories);
            var itemsToSelect = categories.Except(selectedItems);

            foreach (var item in deselectedItems)
                CategoriesComponent.DeselectItem(item);

            foreach (var category in itemsToSelect)
                CategoriesComponent.SelectItem(category);

            return this;
        }

        /// <summary>
        /// Gets the manufacturers.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetManufacturers()
        {
            return ManufacturerComponent.GetSelectedOptions();
        }

        /// <summary>
        /// Gets the available options for manufacturers.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForManufacturers()
        {
            return ManufacturerComponent.GetAllOptions();
        }

        /// <summary>
        /// Sets the manufacturers.
        /// </summary>
        /// <param name="manufacturers">The manufacturers.</param>
        /// <returns></returns>
        public virtual MappingComponent SetManufacturers(IEnumerable<string> manufacturers)
        {
            var selectedItems = ManufacturerComponent.GetSelectedOptions();
            var deselectedItems = selectedItems.Except(manufacturers);
            var itemsToSelect = manufacturers.Except(selectedItems);

            foreach (var item in deselectedItems)
                ManufacturerComponent.DeselectItem(item);

            foreach (var manufacturer in itemsToSelect)
                ManufacturerComponent.SelectItem(manufacturer);

            return this;
        }

        /// <summary>
        /// Gets the stores.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetStores()
        {
            return LimitedToStoresComponent.GetSelectedOptions();
        }

        /// <summary>
        /// Gets the available options for stores.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForStores()
        {
            return LimitedToStoresComponent.GetAllOptions();
        }

        /// <summary>
        /// Sets the stores.
        /// </summary>
        /// <param name="stores">The stores.</param>
        /// <returns></returns>
        public virtual MappingComponent SetStores(IEnumerable<string> stores)
        {
            var selectedItems = LimitedToStoresComponent.GetSelectedOptions();
            var deselectedItems = selectedItems.Except(stores);
            var itemsToSelect = stores.Except(selectedItems);

            foreach (var item in deselectedItems)
                LimitedToStoresComponent.DeselectItem(item);

            foreach (var store in itemsToSelect)
                LimitedToStoresComponent.SelectItem(store);

            return this;
        }

        /// <summary>
        /// Gets the vendor.
        /// </summary>
        /// <returns></returns>
        public virtual string GetVendor()
        {
            return VendorElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the available options for vendors.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForVendors()
        {
            foreach (var option in VendorElement.Options)
                yield return option.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual MappingComponent SetVendor(string vendor,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = VendorElement.Options.IndexOf(
                el => String.Equals(
                    el.TextHelper().InnerText,
                    vendor,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            VendorElement.SelectByIndex(index);

            return this;
        }

        #endregion

    }
}
