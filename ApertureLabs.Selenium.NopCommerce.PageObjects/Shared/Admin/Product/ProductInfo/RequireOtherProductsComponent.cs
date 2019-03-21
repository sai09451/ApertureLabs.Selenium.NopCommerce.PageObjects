using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// Represents the 'Require other products' component on the product info
    /// tab of the admin product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class RequireOtherProductsComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By requireOtherProductsSelector = By.CssSelector("#RequireOtherProducts");
        private readonly By requiredProductIdsSelector = By.CssSelector("#RequiredProductIds");
        private readonly By autoAddTheseProductsToCartSelector = By.CssSelector("#AutomaticallyAddRequiredProducts");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RequireOtherProductsComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public RequireOtherProductsComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement RequireOtherProductsElement => new CheckboxElement(
            WrappedElement.FindElement(
                requireOtherProductsSelector));

        private InputElement RequiredProductIdsElement => new InputElement(
            WrappedElement.FindElement(
                requiredProductIdsSelector));

        private CheckboxElement AutoAddTheseProductsToCartElement => new CheckboxElement(
            WrappedElement.FindElement(
                autoAddTheseProductsToCartSelector));

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the require other products.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetRequireOtherProducts()
        {
            return RequireOtherProductsElement.IsChecked;
        }

        /// <summary>
        /// Sets the require other products.
        /// </summary>
        /// <param name="requireOtherProducts">if set to <c>true</c> [require other products].</param>
        /// <returns></returns>
        public virtual RequireOtherProductsComponent SetRequireOtherProducts(
            bool requireOtherProducts)
        {
            RequireOtherProductsElement.Check(requireOtherProducts);

            return this;
        }

        /// <summary>
        /// Gets the required product ids.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<int> GetRequiredProductIds()
        {
            var culture = CultureInfo.GetCultureInfo("en-US");

            return RequiredProductIdsElement.GetValue<string>()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(str => Int32.Parse(str, culture));
        }

        /// <summary>
        /// Sets the required product ids.
        /// </summary>
        /// <param name="productIds">The product ids.</param>
        /// <returns></returns>
        public virtual RequireOtherProductsComponent SetRequiredProductIds(
            IEnumerable<int> productIds)
        {
            RequiredProductIdsElement.SetValue(String.Join(", ", productIds));

            return this;
        }

        /// <summary>
        /// Gets the automatic add these products to cart.
        /// </summary>
        /// <returns></returns>
        public virtual bool GetAutoAddTheseProductsToCart()
        {
            return AutoAddTheseProductsToCartElement.IsChecked;
        }

        /// <summary>
        /// Sets the automatic add these products to cart.
        /// </summary>
        /// <param name="autoAdd">if set to <c>true</c> [automatic add].</param>
        /// <returns></returns>
        public virtual RequireOtherProductsComponent SetAutoAddTheseProductsToCart(
            bool autoAdd)
        {
            AutoAddTheseProductsToCartElement.Check(autoAdd);

            return this;
        }

        #endregion
    }
}
