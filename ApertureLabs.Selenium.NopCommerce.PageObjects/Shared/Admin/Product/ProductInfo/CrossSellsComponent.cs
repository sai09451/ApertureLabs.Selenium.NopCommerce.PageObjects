using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// The 'Cross-sells' component on the product info tab of the admin
    /// product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class CrossSellsComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By addNewCrossSellProductSelector = By.CssSelector("#btnAddNewCrossSellProduct");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossSellsComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">pageObjectFactory</exception>
        public CrossSellsComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            ProductsGrid = new KGridComponent<CrossSellsComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#crosssellproducts-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);

            SearchProductPopUp = new SearchProductPopUpComponent(
                pageObjectFactory,
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddNewCrossSellProductElement => WrappedElement
            .FindElement(addNewCrossSellProductSelector);

        #endregion

        private KGridComponent<CrossSellsComponent> ProductsGrid { get; }

        private SearchProductPopUpComponent SearchProductPopUp { get; }

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
        /// Adds the new cross sell product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns></returns>
        public virtual CrossSellsComponent AddNewCrossSellProduct(string productName)
        {
            var currentPageHandle = WrappedDriver.CurrentWindowHandle;
            var windowHandles = WrappedDriver.WindowHandles;
            var newWindowHandle = default(string);

            AddNewCrossSellProductElement.Click();

            // Wait for the new window to appear.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => newWindowHandle = d.WindowHandles
                    .Except(windowHandles)
                    .FirstOrDefault());

            // Switch to the new window.
            WrappedDriver.SwitchTo().Window(newWindowHandle);

            SearchProductPopUp.Load();
            SearchProductPopUp.SearchForProduct(productName);

            // Switch back.
            WrappedDriver.SwitchTo().Window(currentPageHandle);

            // Wait for the grid to finish loading.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .UntilChain(d => ProductsGrid.IsBusy())
                .UntilChain(d => !ProductsGrid.IsBusy());

            return this;
        }

        #endregion
    }
}
