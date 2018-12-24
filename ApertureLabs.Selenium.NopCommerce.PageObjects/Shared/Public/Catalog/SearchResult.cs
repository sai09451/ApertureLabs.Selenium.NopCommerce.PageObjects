using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// SearchResult model.
    /// </summary>
    public class SearchResult : IPageComponent
    {
        #region Fields

        #region Selectors

        private readonly By nameSelector = By.CssSelector(".product-title a");
        private readonly By priceSelector = By.CssSelector(".price.actual-price");
        private readonly By imageSelector = By.CssSelector(".picture img");

        #endregion

        private readonly PageSettings pageSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="element"></param>
        /// <param name="pageSettings"></param>
        public SearchResult(By selector,
            IWebElement element,
            PageSettings pageSettings)
        {
            this.By = selector;
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

        #endregion

        /// <inheritdoc/>
        public IWebDriver WrappedDriver { get; private set; }

        /// <inheritdoc/>
        public IWebElement WrappedElement { get; private set; }

        /// <inheritdoc/>
        public By By { get; private set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name => NameElement.Text;

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price => PriceElement.GetTextHelper().ExtractPrice();

        /// <summary>
        /// Url of the image.
        /// </summary>
        public string ImageUrl => ImageElement.GetAttribute("href");

        #endregion

        #region Methods

        /// <inheritdoc/>
        public ILoadableComponent Load()
        {
            return this;
        }

        /// <summary>
        /// Navigates to the product page.
        /// </summary>
        /// <returns></returns>
        public BaseProductPage GoToProductPage()
        {
            var link = WrappedElement.FindElement(nameSelector);
            link.Click();

            var page = new BaseProductPage(WrappedDriver, pageSettings);
            page.Load();

            return page;
        }

        /// <inheritdoc/>
        public bool IsStale()
        {
            try
            {
                WrappedElement.GetAttribute("any");
                return false;
            }
            catch
            {
                return true;
            }
        }

        #endregion
    }
}
