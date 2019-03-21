using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product.ProductInfo
{
    /// <summary>
    /// Represents the 'Associated products' component on the product info tab
    /// of the admin product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class AssociatedProductsVariantsComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By addNewAssociatedProductSelector = By.CssSelector("#btnAddNewAssociatedProduct");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociatedProductsVariantsComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public AssociatedProductsVariantsComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            AssociatedProductsGridComponent = new KGridComponent<AssociatedProductsVariantsComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#associatedproducts-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddNewAssociatedProductElement => WrappedElement
            .FindElement(addNewAssociatedProductSelector);

        #endregion

        /// <summary>
        /// Gets the associated products grid component.
        /// </summary>
        /// <value>
        /// The associated products grid component.
        /// </value>
        public virtual KGridComponent<AssociatedProductsVariantsComponent> AssociatedProductsGridComponent { get; }

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
            AssociatedProductsGridComponent.Load();

            return this;
        }

        /// <summary>
        /// Adds the new associated products.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns></returns>
        public virtual AssociatedProductsVariantsComponent AddNewAssociatedProducts(
            string productName)
        {
            var productNameSelector = By.CssSelector("#SearchProductName");
            var searchSelector = By.CssSelector("#search-products");
            var saveSelector = By.CssSelector("*[name='save']");

            var currentWindowHandle = WrappedDriver.CurrentWindowHandle;
            var windowHandles = WrappedDriver.WindowHandles;
            var newWindowHandle = default(string);

            AddNewAssociatedProductElement.Click();

            // Wait for the new window to appear.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .Until(d => newWindowHandle = d.WindowHandles
                    .Except(windowHandles)
                    .FirstOrDefault());

            // Switch to new window.
            WrappedDriver.SwitchTo().Window(newWindowHandle);

            var productNameEl = new InputElement(
                WrappedDriver.FindElement(
                    productNameSelector));

            var searchElement = WrappedDriver.FindElement(searchSelector);

            var productsGrid = pageObjectFactory.PrepareComponent(
                new KGridComponent<AssociatedProductsVariantsComponent>(
                    new BaseKendoConfiguration(),
                    By.CssSelector("#products-grid"),
                    pageObjectFactory,
                    WrappedDriver,
                    this));

            var saveElement = WrappedDriver.FindElement(saveSelector);

            productNameEl.SetValue(productName);
            searchElement.Click();

            // Wait until ajax request finishes.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => !productsGrid.IsBusy());

            // Select the first product that appears.
            var firstCheckbox = productsGrid
                .GetCell(0, 0)
                .FindElement(By.TagName("input"));

            var checkbox = new CheckboxElement(firstCheckbox);
            checkbox.Check(true);

            // Save.
            saveElement.Click();

            // Wait until the window handle no longer exists.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => !d.WindowHandles.Contains(newWindowHandle));

            // Switch back to the main window.
            WrappedDriver.SwitchTo().Window(currentWindowHandle);

            // Wait for the ajx request to finish.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .UntilChain(d => AssociatedProductsGridComponent.IsBusy())
                .UntilChain(d => !AssociatedProductsGridComponent.IsBusy());

            return this;
        }

        #endregion
    }
}
