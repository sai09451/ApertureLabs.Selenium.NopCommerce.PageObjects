using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// A component that represents a 'row' on the admin product list page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class ListPageProductRowComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By checkboxSelector = By.CssSelector("td:nth-child(1) input");
        private readonly By imageSelector = By.CssSelector("td:nth-child(2) img");
        private readonly By productNameSelector = By.CssSelector("td:nth-child(3)");
        private readonly By skuSelector = By.CssSelector("td:nth-child(4)");
        private readonly By priceSelector = By.CssSelector("td:nth-child(5)");
        private readonly By stockQuantitySelector = By.CssSelector("td:nth-child(6)");
        private readonly By productTypeSelector = By.CssSelector("td:nth-child(7)");
        private readonly By publishedSelector = By.CssSelector("td:nth-child(8) .fa-check");
        private readonly By editSelector = By.CssSelector("td:nth-child(9) [href]");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPageProductRowComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public ListPageProductRowComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));
        }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement CheckboxElement => new CheckboxElement(
            WrappedElement.FindElement(
                checkboxSelector));

        private IWebElement ImageElement => WrappedElement
            .FindElement(imageSelector);

        private IWebElement ProductNameElement => WrappedElement
            .FindElement(productNameSelector);

        private IWebElement SkuElement => WrappedElement
            .FindElement(skuSelector);

        private IWebElement PriceElement => WrappedElement
            .FindElement(priceSelector);

        private IWebElement StockQuantityElement => WrappedElement
            .FindElement(stockQuantitySelector);

        private IWebElement ProductTypeElement => WrappedElement
            .FindElement(productTypeSelector);

        private IWebElement PublishedElement => WrappedElement
            .FindElement(publishedSelector);

        private IWebElement EditElement => WrappedElement
            .FindElement(editSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Checks the specified check.
        /// </summary>
        /// <param name="check">if set to <c>true</c> [check].</param>
        /// <returns></returns>
        public virtual ListPageProductRowComponent Check(bool check)
        {
            CheckboxElement.Check(check);

            return this;
        }

        /// <summary>
        /// Determines whether this instance is checked.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsChecked()
        {
            return CheckboxElement.IsChecked;
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <returns></returns>
        public virtual string GetImageUrl()
        {
            return ImageElement.GetAttribute("src");
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <returns></returns>
        public virtual string GetProductName()
        {
            return ProductNameElement.TextHelper().InnerText;
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
        /// Gets the price.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetPrice()
        {
            return PriceElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the stock quantity.
        /// </summary>
        /// <returns></returns>
        public virtual int? GetStockQuantity()
        {
            var innerText = StockQuantityElement.TextHelper().InnerText;

            return String.IsNullOrEmpty(innerText)
                ? null
                : (int?)Int32.Parse(innerText);
        }

        /// <summary>
        /// Gets the type of the product.
        /// </summary>
        /// <returns></returns>
        public virtual string GetProductType()
        {
            return ProductTypeElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Checks if the product is published.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetIsPublished()
        {
            return PublishedElement.Classes().Contains("true-icon");
        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        /// <returns></returns>
        public virtual IEditPage Edit()
        {
            EditElement.Click();

            return pageObjectFactory.PreparePage<IEditPage>();
        }

        #endregion
    }
}
