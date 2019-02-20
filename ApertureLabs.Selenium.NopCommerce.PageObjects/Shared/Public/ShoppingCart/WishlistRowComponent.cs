using System;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart
{
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

        public WishlistRowComponent MarkForRemoval(bool remove)
        {
            RemoveFromCartElement.Check(remove);

            return this;
        }

        public bool IsMarkedForRemoval()
        {
            return RemoveFromCartElement.IsChecked;
        }

        public WishlistRowComponent MarkForAddToCart(bool addToCart)
        {
            if (!AddToCartElement.Displayed)
                throw new ElementNotVisibleException();

            AddToCartElement.Check(addToCart);

            return this;
        }

        public string GetImageUrl()
        {
            return ImageElement.GetAttribute("href");
        }

        public string GetProductName()
        {
            return ProductTitleElement.TextHelper().InnerText;
        }

        public decimal GetPrice()
        {
            return PriceElement.TextHelper().ExtractPrice();
        }

        public int GetQuantity()
        {
            return int.Parse(QuantityElement.GetValue<string>());
        }

        public WishlistRowComponent SetQuantity(int quantity)
        {
            QuantityElement.SetValue(quantity);

            return this;
        }

        public decimal GetTotal()
        {
            return SubTotalElement.TextHelper().ExtractPrice();
        }

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
