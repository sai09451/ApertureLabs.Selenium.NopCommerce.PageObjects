using System;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart
{
    /// <summary>
    /// Represents a row on a <see cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart.IWishListPage"/>.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class WishlistRowComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By removeSelector = By.CssSelector(".remove-from-cart input");
        private readonly By addToCartSelector = By.CssSelector(".add-to-cart input");
        private readonly By imageSelector = By.CssSelector(".product-picture img");
        private readonly By productTitleSelector = By.CssSelector(".product .product-name");
        private readonly By priceSelector = By.CssSelector(".unit-price .product-unit-price");
        private readonly By quantitySelector = By.CssSelector(".quantity input");
        private readonly By subTotalSelector = By.CssSelector(".subtotal .product-subtotal");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WishlistRowComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public WishlistRowComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement RemoveFromCartElement => new CheckboxElement(
            WrappedElement.FindElement(
                removeSelector));

        private CheckboxElement AddToCartElement => new CheckboxElement(
            WrappedElement.FindElement(
                addToCartSelector));

        private IWebElement ImageElement => WrappedDriver
            .FindElement(imageSelector);

        private IWebElement ProductTitleElement => WrappedDriver
            .FindElement(productTitleSelector);

        private IWebElement PriceElement => WrappedDriver
            .FindElement(priceSelector);

        private InputElement QuantityElement => new InputElement(
            WrappedDriver.FindElement(
                quantitySelector));

        private IWebElement SubTotalElement => WrappedDriver
            .FindElement(subTotalSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Marks the product for removal.
        /// </summary>
        /// <param name="remove">
        /// if set to <c>true</c> marks the product for removal; else if
        /// <c>false</c> unmarks the product for removal.
        /// </param>
        /// <returns></returns>
        public WishlistRowComponent MarkForRemoval(bool remove)
        {
            RemoveFromCartElement.Check(remove);

            return this;
        }

        /// <summary>
        /// Determines whether the product is marked for removal.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the product is marked for removal; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMarkedForRemoval()
        {
            return RemoveFromCartElement.IsChecked;
        }

        /// <summary>
        /// Marks the product to be added to cart.
        /// </summary>
        /// <param name="addToCart">if set to <c>true</c> [add to cart].</param>
        /// <returns></returns>
        /// <exception cref="ElementNotVisibleException"></exception>
        public WishlistRowComponent MarkForAddToCart(bool addToCart)
        {
            if (!AddToCartElement.Displayed)
                throw new ElementNotVisibleException();

            AddToCartElement.Check(addToCart);

            return this;
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl()
        {
            return ImageElement.GetAttribute("href");
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        /// <returns></returns>
        public string GetProductName()
        {
            return ProductTitleElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <returns></returns>
        public decimal GetPrice()
        {
            return PriceElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <returns></returns>
        public int GetQuantity()
        {
            return int.Parse(QuantityElement.GetValue<string>());
        }

        /// <summary>
        /// Sets the quantity.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns></returns>
        public WishlistRowComponent SetQuantity(int quantity)
        {
            QuantityElement.SetValue(quantity);

            return this;
        }

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotal()
        {
            return SubTotalElement.TextHelper().ExtractPrice();
        }

        /// <summary>
        /// Goes to the product.
        /// </summary>
        /// <returns></returns>
        public IBaseProductPage GoToProduct()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .UntilPageReloads(ProductTitleElement, e => e.Click());

            return pageObjectFactory.PreparePage<IBaseProductPage>();
        }

        #endregion
    }
}
