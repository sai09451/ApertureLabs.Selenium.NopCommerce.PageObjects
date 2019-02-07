using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary
{
    /// <summary>
    /// OrderSummaryReadOnlyRowComponent.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class OrderSummaryReadOnlyRowComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By skuSelector = By.CssSelector(".sku .sku-number");
        private readonly By productInfoSelector = By.CssSelector(".product");
        private readonly By productPriceSelector = By.CssSelector(".unit-price .product-unit-price");
        private readonly By productQtySelector = By.CssSelector(".quantity .qty-input");
        private readonly By productSubTotalSelector = By.CssSelector(".product-subtotal");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSummaryReadOnlyRowComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public OrderSummaryReadOnlyRowComponent(By selector,
            IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private IWebElement SkuElement => WrappedElement.FindElement(skuSelector);
        private IWebElement ProductPriceElement => WrappedElement.FindElement(productPriceSelector);
        private IWebElement ProductSubTotalElement => WrappedElement.FindElement(productSubTotalSelector);
        private IWebElement ProductQtyElement => WrappedElement.FindElement(productQtySelector);
        private IWebElement ProductInfoElement => WrappedElement.FindElement(productInfoSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the column (zero-based). Returns null if there is no such column.
        /// </summary>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public virtual IWebElement GetColumn(int columnIndex)
        {
            var selector = By.CssSelector($"td:nth-child({columnIndex + 1})");
            var col = WrappedElement.FindElements(selector).FirstOrDefault();

            return col;
        }

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetPrice()
        {
            return ProductPriceElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <returns></returns>
        public virtual string GetProductName()
        {
            return ProductInfoElement
                .FindElement(By.CssSelector(".product-name"))
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <returns></returns>
        public virtual int GetQuantity()
        {
            return ProductQtyElement.TextHelper().ExtractInteger();
        }

        /// <summary>
        /// Gets the sku.
        /// </summary>
        /// <returns></returns>
        public virtual string GetSku()
        {
            return SkuElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetTotal()
        {
            return ProductSubTotalElement.TextHelper().ExtractPrice();
        }

        #endregion
    }
}
