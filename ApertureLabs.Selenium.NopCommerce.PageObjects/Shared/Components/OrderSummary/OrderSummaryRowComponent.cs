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
    public class OrderSummaryRowComponent : OrderSummaryReadOnlyRowComponent
    {
        #region Fields

        #region Selectors

        private readonly By removeFromCartSelector = By.CssSelector(".remove-from-cart input");
        private readonly By productPictureSelector = By.CssSelector(".product-picture img");
        private readonly By productInfoSelector = By.CssSelector(".product");
        private readonly By productEditSelector = By.CssSelector(".product .edit a");
        private readonly By productQtySelector = By.CssSelector(".quantity .qty-input");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSummaryRowComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public OrderSummaryRowComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement RemoveFromCartElement => new CheckboxElement(WrappedElement.FindElement(removeFromCartSelector));
        private IWebElement ProductPictureElement => WrappedElement.FindElement(productPictureSelector);
        private IWebElement ProductInfoElement => WrappedElement.FindElement(productInfoSelector);
        private IWebElement ProductEditElement => WrappedElement.FindElement(productEditSelector);
        private InputElement ProductQtyElement => new InputElement(WrappedElement.FindElement(productQtySelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <returns></returns>
        public override int GetQuantity()
        {
            return ProductQtyElement.GetValue<int>();
        }

        /// <summary>
        /// Sets the quantity.
        /// </summary>
        /// <param name="qty">The qty.</param>
        /// <returns></returns>
        public virtual OrderSummaryRowComponent SetQuantity(int qty)
        {
            ProductQtyElement.SetValue(qty);

            return this;
        }

        /// <summary>
        /// Marks for removal.
        /// </summary>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        /// <returns></returns>
        public virtual OrderSummaryRowComponent MarkForRemoval(bool remove)
        {
            RemoveFromCartElement.Check(remove);

            return this;
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
        /// Edits this instance.
        /// </summary>
        /// <returns></returns>
        public virtual T Edit<T>() where T : IBaseProductPage
        {
            ProductEditElement.Click();

            return pageObjectFactory.PreparePage<T>();
        }

        #endregion
    }
}
