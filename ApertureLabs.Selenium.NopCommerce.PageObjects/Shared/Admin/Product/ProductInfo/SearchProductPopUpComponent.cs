using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// Wrapper for the product search pop-up window.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class SearchProductPopUpComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By productNameSelector = By.CssSelector("#SearchProductName");
        private readonly By searchSelector = By.CssSelector("#search-products");
        private readonly By saveSelector = By.CssSelector("*[name='save']");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchProductPopUpComponent"/> class.
        /// </summary>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public SearchProductPopUpComponent(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(By.CssSelector("body"), driver)
        {
            ProductsGrid = new KGridComponent<SearchProductPopUpComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#products-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement ProductNameElement => new InputElement(
            WrappedElement.FindElement(
                productNameSelector));

        private IWebElement SearchElement => WrappedElement
            .FindElement(searchSelector);

        private IWebElement SaveElement => WrappedElement
            .FindElement(saveSelector);

        #endregion

        private KGridComponent<SearchProductPopUpComponent> ProductsGrid { get; }

        #endregion

        #region Methods

        /// <summary>
        /// If overriding don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            ProductsGrid.Load();

            return this;
        }

        /// <summary>
        /// Searches for product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        public virtual void SearchForProduct(string productName)
        {
            var currentWindowHandle = WrappedDriver.CurrentWindowHandle;
            ProductNameElement.SetValue(productName);
            SearchElement.Click();

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .Until(d => !ProductsGrid.IsBusy());

            var firstCheckbox = ProductsGrid
                .GetCell(0, 0)
                .FindElement(By.TagName("input"));

            var checkbox = new CheckboxElement(firstCheckbox);
            checkbox.Check(true);
            SaveElement.Click();

            // Wait for the page to close.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => !d.WindowHandles.Contains(currentWindowHandle));
        }

        #endregion
    }
}
