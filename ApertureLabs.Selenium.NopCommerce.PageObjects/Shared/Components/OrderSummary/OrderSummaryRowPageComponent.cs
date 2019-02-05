using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary
{
    /// <summary>
    /// OrderSummaryRowPageComponent.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class OrderSummaryRowPageComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By removeFromCartSelector = By.CssSelector(".remove-from-cart input");
        private readonly By skuSelector = By.CssSelector(".sku .sku-number");
        private readonly By productPictureSelector = By.CssSelector(".product-picture img");
        private readonly By productInfoSelector = By.CssSelector(".product");
        private readonly By productEditSelector = By.CssSelector(".product .edit a");
        private readonly By productPriceSelector = By.CssSelector(".unit-price .product-unit-price");
        private readonly By productQtySelector = By.CssSelector(".quantity .qty-input");
        private readonly By productSubTotalSelector = By.CssSelector(".subtotal .product-subtotal");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSummaryRowPageComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OrderSummaryRowPageComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver, selector)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement RemoveFromCartElement => new CheckboxElement(WrappedElement.FindElement(removeFromCartSelector));
        private IWebElement SkuElement => WrappedElement.FindElement(skuSelector);
        private IWebElement ProductPictureElement => WrappedElement.FindElement(productPictureSelector);
        private IWebElement ProductInfoElement => WrappedElement.FindElement(productInfoSelector);
        private IWebElement ProductEditElement => WrappedElement.FindElement(productEditSelector);
        private IWebElement ProductPriceElement => WrappedElement.FindElement(productPriceSelector);
        private InputElement ProductQtyElement => new InputElement(WrappedElement.FindElement(productQtySelector));
        private IWebElement ProductSubTotalElement => WrappedElement.FindElement(productSubTotalSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <returns></returns>
        public virtual int GetQuantity()
        {
            return ProductQtyElement.GetValue<int>();
        }

        /// <summary>
        /// Sets the quantity.
        /// </summary>
        /// <param name="qty">The qty.</param>
        /// <returns></returns>
        public virtual OrderSummaryRowPageComponent SetQuantity(int qty)
        {
            ProductQtyElement.SetValue(qty);

            return this;
        }

        /// <summary>
        /// Marks for removal.
        /// </summary>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        /// <returns></returns>
        public virtual OrderSummaryRowPageComponent MarkForRemoval(bool remove)
        {
            RemoveFromCartElement.Check(remove);

            return this;
        }

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
        /// Gets the sku.
        /// </summary>
        /// <returns></returns>
        public virtual string GetSku()
        {
            return SkuElement.TextHelper().InnerText;
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
        /// Gets the attributes.
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyDictionary<string, string> GetAttributes()
        {
            var attributes = new Dictionary<string, string>();

            var text = ProductInfoElement
                .FindElement(By.CssSelector(".attributes"))
                .TextHelper()
                .InnerText;

            var matches = Regex.Matches(text,
                @"^(.*):\s(.*)$",
                RegexOptions.Multiline);

            foreach (Match match in matches)
            {
                var key = match.Groups[1].Value;
                var value = match.Groups[2].Value;

                attributes[key] = value;
            }

            return attributes;
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
        /// Gets the total.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetTotal()
        {
            return ProductSubTotalElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        /// <returns></returns>
        public virtual T Edit<T>() where T : IBaseProductPage
        {
            ProductEditElement.Click();

            return pageObjectFactory.PreparePage<T>();
        }

        #endregion

        public string GetElementProperty(IWebElement element,
            string propertyName,
            string defaultValueIfNull = null)
        {
            var value = default(string);
            var driver = element.GetDriver();

            var capabilities = driver.Capabilities();
            var isChrome = false;

            if (capabilities != null)
            {
                var browserName = (string)capabilities.GetCapability(
                    CapabilityType.BrowserName);

                if (browserName == "chrome")
                {
                    isChrome = true;
                }
            }

            if (isChrome)
            {
                // Use js to get element property.
                var script =
                    "var el = arguments[0];" +
                    "return el['" + propertyName + "'];";

                value = driver
                    .JavaScriptExecutor()
                    .ExecuteScript(script, element)
                    .ToString();
            }
            else
            {
                // Browser should support get property.
                value = element.GetProperty(propertyName);
            }

            return value ?? defaultValueIfNull;
        }
    }
}
