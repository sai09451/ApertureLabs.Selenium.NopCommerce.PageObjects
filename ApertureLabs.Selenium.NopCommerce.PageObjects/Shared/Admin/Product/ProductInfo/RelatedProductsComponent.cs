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
    /// The 'Related products' component of the product info tab on the admin
    /// product edit page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class RelatedProductsComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By addNewRelatedProductSelector = By.CssSelector("#btnAddNewRelatedProduct");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedProductsComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public RelatedProductsComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            RelatedProductsGrid = new KGridComponent<RelatedProductsComponent>(
                new BaseKendoConfiguration(),
                By.CssSelector("#relatedproducts-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);

            SearchProductPopup = new SearchProductPopUpComponent(
                pageObjectFactory,
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddNewRelatedProductElement => WrappedElement
            .FindElement(addNewRelatedProductSelector);

        #endregion

        private SearchProductPopUpComponent SearchProductPopup { get; }

        /// <summary>
        /// Gets the related products grid.
        /// </summary>
        /// <value>
        /// The related products grid.
        /// </value>
        public virtual KGridComponent<RelatedProductsComponent> RelatedProductsGrid { get; }

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
            RelatedProductsGrid.Load();

            return this;
        }

        /// <summary>
        /// Adds the new related product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns></returns>
        public virtual RelatedProductsComponent AddNewRelatedProduct(string productName)
        {
            var currentPageHandle = WrappedDriver.CurrentWindowHandle;
            var windowHandles = WrappedDriver.WindowHandles;
            var newWindowHandle = default(string);

            AddNewRelatedProductElement.Click();

            // Wait for the new window to appear.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => newWindowHandle = d.WindowHandles
                    .Except(windowHandles)
                    .FirstOrDefault());

            // Switch to the new window.
            WrappedDriver.SwitchTo().Window(newWindowHandle);

            SearchProductPopup.Load();
            SearchProductPopup.SearchForProduct(productName);

            // Switch back.
            WrappedDriver.SwitchTo().Window(currentPageHandle);

            // Wait for the grid to finish loading.
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .UntilChain(d => RelatedProductsGrid.IsBusy())
                .UntilChain(d => !RelatedProductsGrid.IsBusy());

            return this;
        }

        /// <summary>
        /// Removes the related product.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual RelatedProductsComponent RemoveRelatedProduct(
            string productName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            var totalRows = RelatedProductsGrid.GetNumberOfRows();

            for (var i = 0; i < totalRows; i++)
            {
                var name = RelatedProductsGrid.GetCell(i, 0)
                    .TextHelper()
                    .InnerText;

                var matches = String.Equals(
                    name,
                    productName,
                    stringComparison);

                if (!matches)
                    continue;

                var deleteButton = RelatedProductsGrid.GetCell(i, 2)
                    .FindElement(By.CssSelector(".k-grid-delete"));

                deleteButton.Click();

                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(10))
                    .UntilChain(d => RelatedProductsGrid.IsBusy())
                    .UntilChain(d => !RelatedProductsGrid.IsBusy());

                break;
            }

            return this;
        }

        /// <summary>
        /// Gets the related product names.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetRelatedProductNames()
        {
            var totalRowCount = RelatedProductsGrid.GetNumberOfRows();

            for (var i = 0; i < totalRowCount; i++)
            {
                var name = RelatedProductsGrid.GetCell(i, 0)
                    .TextHelper()
                    .InnerText;

                yield return name;
            }
        }

        #endregion
    }
}
