using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// The 'Inventory' component on the admin product edit page > product info
    /// > inventory.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class InventoryComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By inventoryMethodSelector = By.CssSelector("#ManageInventoryMethodId");
        private readonly By stockQuantitySelector = By.CssSelector("#StockQuantity");
        private readonly By warehouseSelector = By.CssSelector("#WarehouseId");
        private readonly By multipleWarehousesSelector = By.CssSelector("#UseMultipleWarehouses");
        private readonly By displayAvailabilitySelector = By.CssSelector("#DisplayStockAvailability");
        private readonly By displayStockQtySelector = By.CssSelector("#DisplayStockQuantity");
        private readonly By minimumStockQtySelector = By.CssSelector("#MinStockQuantity");
        private readonly By lowStockActivitySelector = By.CssSelector("#LowStockActivityId");
        private readonly By notifyForQtyBelowSelector = By.CssSelector("#NotifyAdminForQuantityBelow");
        private readonly By backordersSelector = By.CssSelector("#BackorderModeId");
        private readonly By allowBackInStockSubSelector = By.CssSelector("#AllowBackInStockSubscriptions");
        private readonly By productAvailabilityRangeSelector = By.CssSelector("#ProductAvailabilityRangeId");
        private readonly By minimumCartQtySelector = By.CssSelector("#OrderMinimumQuantity");
        private readonly By maximumCartQtySelector = By.CssSelector("#OrderMaximumQuantity");
        private readonly By allowedQuantitiesSelector = By.CssSelector("#AllowedQuantities");
        private readonly By notReturnableSelector = By.CssSelector("#NotReturnable");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public InventoryComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private SelectElement InventoryMethodElement => new SelectElement(
            WrappedElement.FindElement(
                inventoryMethodSelector));

        private InputElement StockQuantityElement => new InputElement(
            WrappedElement.FindElement(
                stockQuantitySelector));

        private SelectElement WarehouseElement => new SelectElement(
            WrappedElement.FindElement(
                warehouseSelector));

        private CheckboxElement MultipleWarehousesElement => new CheckboxElement(
            WrappedElement.FindElement(
                multipleWarehousesSelector));

        private CheckboxElement DisplayAvailabilityElement => new CheckboxElement(
            WrappedElement.FindElement(
                displayAvailabilitySelector));

        private CheckboxElement DisplayStockQuantityElement => new CheckboxElement(
            WrappedElement.FindElement(
                stockQuantitySelector));

        private InputElement MinimumStockQuantityElement => new InputElement(
            WrappedElement.FindElement(
                minimumStockQtySelector));

        private SelectElement LowStockActivityElement => new SelectElement(
            WrappedElement.FindElement(
                lowStockActivitySelector));

        private InputElement NotifyForQtyBelowElement => new InputElement(
            WrappedElement.FindElement(
                notifyForQtyBelowSelector));

        private SelectElement BackordersElement => new SelectElement(
            WrappedElement.FindElement(
                backordersSelector));

        private CheckboxElement AllowBackInStockSubElement => new CheckboxElement(
            WrappedElement.FindElement(
                allowBackInStockSubSelector));

        private SelectElement ProductAvailabilityRangeElement => new SelectElement(
            WrappedElement.FindElement(
                productAvailabilityRangeSelector));

        private InputElement MinimumCartQuantityElement => new InputElement(
            WrappedElement.FindElement(
                minimumCartQtySelector));

        private InputElement MaximumCartQuantityElement => new InputElement(
            WrappedElement.FindElement(
                maximumCartQtySelector));

        private InputElement AllowedQuantitiesElement => new InputElement(
            WrappedElement.FindElement(
                allowedQuantitiesSelector));

        private CheckboxElement NotReturnableElement => new CheckboxElement(
            WrappedElement.FindElement(
                notReturnableSelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the inventory method.
        /// </summary>
        /// <returns></returns>
        public virtual string GetInventoryMethod()
        {
            return InventoryMethodElement.SelectedOption
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets the available inventory methods.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableInventoryMethods()
        {
            foreach (var opt in InventoryMethodElement.Options)
                yield return opt.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the inventory method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual InventoryComponent SetInventoryMethod(string method,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = InventoryMethodElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    method,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            InventoryMethodElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Gets the stock quantity.
        /// </summary>
        /// <returns></returns>
        public virtual string GetStockQuantity()
        {
            return StockQuantityElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the stock quantity.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns></returns>
        public virtual InventoryComponent SetStockQuantity(int quantity)
        {
            StockQuantityElement.SetValue(quantity);

            return this;
        }

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <returns></returns>
        public virtual string GetWarehouse()
        {
            return WarehouseElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the available warehouses.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableWarehouses()
        {
            foreach (var option in WarehouseElement.Options)
                yield return option.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual InventoryComponent SetWarehouse(string warehouse,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = WarehouseElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    warehouse,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            WarehouseElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Gets the has multiple warehouses.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetHasMultipleWarehouses()
        {
            return MultipleWarehousesElement.IsChecked;
        }

        /// <summary>
        /// Sets the has multiple warehouses.
        /// </summary>
        /// <param name="hasMultiple">if set to <c>true</c> [has multiple].</param>
        /// <returns></returns>
        public virtual InventoryComponent SetHasMultipleWarehouses(bool hasMultiple)
        {
            MultipleWarehousesElement.Check(hasMultiple);

            return this;
        }

        /// <summary>
        /// Gets the display availability.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetDisplayAvailability()
        {
            return DisplayAvailabilityElement.IsChecked;
        }

        /// <summary>
        /// Sets the display availability.
        /// </summary>
        /// <param name="display">if set to <c>true</c> [display].</param>
        /// <returns></returns>
        public virtual InventoryComponent SetDisplayAvailability(bool display)
        {
            DisplayAvailabilityElement.Check(display);

            return this;
        }

        /// <summary>
        /// Gets the display stock quantity.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetDisplayStockQuantity()
        {
            return DisplayStockQuantityElement.IsChecked;
        }

        /// <summary>
        /// Sets the display stock quantity.
        /// </summary>
        /// <param name="display">if set to <c>true</c> [display].</param>
        /// <returns></returns>
        public virtual InventoryComponent SetDisplayStockQuantity(bool display)
        {
            DisplayStockQuantityElement.Check(display);

            return this;
        }

        /// <summary>
        /// Gets the minimum stock quantity.
        /// </summary>
        /// <returns></returns>
        public virtual int GetMinimumStockQuantity()
        {
            return MinimumStockQuantityElement.GetValue<int>();
        }

        /// <summary>
        /// Sets the minimum stock quantity.
        /// </summary>
        /// <param name="minimumStockQty">The minimum stock qty.</param>
        /// <returns></returns>
        public virtual InventoryComponent SetMinimumStockQuantity(
            int minimumStockQty)
        {
            MinimumStockQuantityElement.SetValue(minimumStockQty);

            return this;
        }

        /// <summary>
        /// Gets the low stock activity.
        /// </summary>
        /// <returns></returns>
        public virtual string GetLowStockActivity()
        {
            return LowStockActivityElement.SelectedOption
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets the available options for low stock activity.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForLowStockActivity()
        {
            foreach (var option in LowStockActivityElement.Options)
                yield return option.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the low stock activity.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual InventoryComponent SetLowStockActivity(string option,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = LowStockActivityElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    option,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            LowStockActivityElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Gets the notify for qty below.
        /// </summary>
        /// <returns></returns>
        public virtual int GetNotifyForQtyBelow()
        {
            return NotifyForQtyBelowElement.GetValue<int>();
        }

        /// <summary>
        /// Sets the notify for qty below.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns></returns>
        public virtual InventoryComponent SetNotifyForQtyBelow(int quantity)
        {
            NotifyForQtyBelowElement.SetValue(quantity);

            return this;
        }

        /// <summary>
        /// Gets the backorders.
        /// </summary>
        /// <returns></returns>
        public virtual string GetBackorders()
        {
            return BackordersElement.SelectedOption.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the available options for backorders.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForBackorders()
        {
            foreach (var option in BackordersElement.Options)
                yield return option.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the backorders.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual InventoryComponent SetBackorders(string option,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = BackordersElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    option,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            BackordersElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Gets the allow back in stock subscriptions.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetAllowBackInStockSubscriptions()
        {
            return AllowBackInStockSubElement.IsChecked;
        }

        /// <summary>
        /// Sets the allow back in stock subscriptions.
        /// </summary>
        /// <param name="allow">if set to <c>true</c> [allow].</param>
        /// <returns></returns>
        public virtual InventoryComponent SetAllowBackInStockSubscriptions(
            bool allow)
        {
            AllowBackInStockSubElement.Check(allow);

            return this;
        }

        /// <summary>
        /// Gets the product availability range.
        /// </summary>
        /// <returns></returns>
        public virtual string GetProductAvailabilityRange()
        {
            return ProductAvailabilityRangeElement.SelectedOption
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets the available options for product availability range.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetAvailableOptionsForProductAvailabilityRange()
        {
            foreach (var option in ProductAvailabilityRangeElement.Options)
                yield return option.TextHelper().InnerText;
        }

        /// <summary>
        /// Sets the product availability range.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public virtual InventoryComponent SetProductAvailabilityRange(
            string option,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var index = ProductAvailabilityRangeElement.Options.IndexOf(
                e => String.Equals(
                    e.TextHelper().InnerText,
                    option,
                    stringComparison));

            if (index == -1)
                throw new NoSuchElementException();

            ProductAvailabilityRangeElement.SelectByIndex(index);

            return this;
        }

        /// <summary>
        /// Gets the minimum cart quantity.
        /// </summary>
        /// <returns></returns>
        public virtual int GetMinimumCartQuantity()
        {
            return MinimumCartQuantityElement.GetValue<int>();
        }

        /// <summary>
        /// Sets the minimum cart quantity.
        /// </summary>
        /// <param name="minQty">The minimum qty.</param>
        /// <returns></returns>
        public virtual InventoryComponent SetMinimumCartQuantity(int minQty)
        {
            MinimumCartQuantityElement.SetValue(minQty);

            return this;
        }

        /// <summary>
        /// Gets the allowed quantities.
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyCollection<int> GetAllowedQuantities()
        {
            var csv = AllowedQuantitiesElement.GetValue<string>();

            var numbers = csv.Split(',')
                .Select(str => Int32.Parse(str.Trim()))
                .ToList()
                .AsReadOnly();

            return numbers;
        }

        /// <summary>
        /// Sets the allowed quantities.
        /// </summary>
        /// <param name="allowedQuantities">The allowed quantities.</param>
        /// <returns></returns>
        public virtual InventoryComponent SetAllowedQuantities(
            IEnumerable<int> allowedQuantities)
        {
            if (allowedQuantities?.Any() ?? true)
            {
                AllowedQuantitiesElement.Clear();
            }
            else
            {
                var asStr = String.Join(",", allowedQuantities);
                AllowedQuantitiesElement.SetValue(asStr);
            }

            return this;
        }

        /// <summary>
        /// Gets the not returnable.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetNotReturnable()
        {
            return NotReturnableElement.IsChecked;
        }

        /// <summary>
        /// Sets the not returnable.
        /// </summary>
        /// <param name="notReturnable">if set to <c>true</c> [not returnable].</param>
        /// <returns></returns>
        public virtual InventoryComponent SetNotReturnable(bool notReturnable)
        {
            NotReturnableElement.Check(notReturnable);

            return this;
        }

        #endregion
    }
}
