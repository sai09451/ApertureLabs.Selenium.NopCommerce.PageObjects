﻿using System;
using ApertureLabs.Selenium.Css;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.BarNotification;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// SearchResult model.
    /// </summary>
    public class SearchResultComponent : IPageComponent
    {
        #region Fields

        #region Selectors

        private readonly By nameSelector = By.CssSelector(".product-title a");
        private readonly By priceSelector = By.CssSelector(".price.actual-price");
        private readonly By imageSelector = By.CssSelector(".picture img");
        private readonly By ratingSelector = By.CssSelector(".rating > div");
        private readonly By addToCartSelector = By.CssSelector(".buttons .product-box-add-to-cart-button");
        private readonly By wishlistSelector = By.CssSelector(".buttons .add-to-wishlist-button");
        private readonly By comparelistSelector = By.CssSelector(".buttons .add-to-compare-list-button");
        private readonly By ajaxLoadingSelector = By.CssSelector(".ajax-loading-block-window");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;
        private readonly PageSettings pageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="element">The element.</param>
        /// <param name="pageSettings">The page settings.</param>
        public SearchResultComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebElement element,
            PageSettings pageSettings)
        {
            this.By = selector;
            this.pageObjectFactory = pageObjectFactory;
            this.pageSettings = pageSettings;
            this.WrappedDriver = element.GetDriver();
            this.WrappedElement = element;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement NameElement => WrappedElement.FindElement(nameSelector);
        private IWebElement PriceElement => WrappedElement.FindElement(priceSelector);
        private IWebElement ImageElement => WrappedElement.FindElement(imageSelector);
        private IWebElement RatingElement => WrappedElement.FindElement(ratingSelector);
        private IWebElement AddToCartElement => WrappedElement.FindElement(addToCartSelector);
        private IWebElement AddToCompareListElement => WrappedElement.FindElement(comparelistSelector);
        private IWebElement AddToWishListElement => WrappedElement.FindElement(wishlistSelector);
        private IWebElement AjaxLoadingElement => WrappedElement.FindElement(ajaxLoadingSelector);

        #endregion

        /// <inheritdoc/>
        public virtual IWebDriver WrappedDriver { get; private set; }

        /// <inheritdoc/>
        public virtual IWebElement WrappedElement { get; private set; }

        /// <inheritdoc/>
        public virtual By By { get; private set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public virtual string Name => NameElement.Text;

        /// <summary>
        /// Price of the product.
        /// </summary>
        public virtual decimal Price => PriceElement.TextHelper().ExtractPrice();

        /// <summary>
        /// Url of the image.
        /// </summary>
        public virtual string ImageUrl => ImageElement.GetAttribute("href");

        /// <summary>
        /// Gets the rating. Returns a percentage.
        /// </summary>
        public virtual double Rating { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public virtual ILoadableComponent Load()
        {
            var widthCssValue = RatingElement.GetCssValue("width");
            Rating = new CssDimension(widthCssValue).Number;

            return this;
        }

        /// <summary>
        /// Navigates to the product page.
        /// </summary>
        /// <returns></returns>
        public virtual IBaseProductPage GoToProductPage()
        {
            var link = WrappedElement.FindElement(nameSelector);
            link.Click();

            return pageObjectFactory.PreparePage<IBaseProductPage>();
        }

        /// <summary>
        /// Adds to cart.
        /// </summary>
        public virtual BarNotificationComponent<SearchResultComponent> AddToCart()
        {
            AddToCartElement.Click();

            // Wait for the loading indicator to appear then disappear.
            WrappedDriver.Wait(TimeSpan.FromSeconds(30))
                .TrySequentialWait(
                    out var exception,
                    d => AjaxLoadingElement.Displayed,
                    d => !AjaxLoadingElement.Displayed);

            return pageObjectFactory.PrepareComponent(
                new BarNotificationComponent<SearchResultComponent>(
                    WrappedDriver,
                    this));
        }

        /// <summary>
        /// Adds to wish list.
        /// </summary>
        public virtual void AddToWishList()
        {
            AddToWishListElement.Click();
        }

        /// <summary>
        /// Adds to compare list.
        /// </summary>
        public virtual void AddToCompareList()
        {
            AddToCompareListElement.Click();
        }

        /// <inheritdoc/>
        public virtual bool IsStale()
        {
            return WrappedElement.IsStale();
        }

        #endregion
    }
}
