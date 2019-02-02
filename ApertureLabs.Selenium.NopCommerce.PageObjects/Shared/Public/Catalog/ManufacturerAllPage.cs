using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// The page that lists all manufacturers.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public class ManufacturerAllPage : CatalogTemplatePage, IManufacturerAllPage
    {
        #region Fields

        #region Selectors

        private readonly By ManufacturerPictureSelector = By.CssSelector(".manufacturer-item .picture > a");
        private readonly By ManufacturerTitleSelector = By.CssSelector(".manufacturer-item .title > a");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerAllPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public ManufacturerAllPage(
            IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(basePage,
                  pageObjectFactory,
                  driver)
        {
            this.pageObjectFactory = pageObjectFactory;
            this.Uri = new Uri(
                new Uri(pageSettings.BaseUrl),
                "manufacturer/all");
        }

        #endregion

        #region Properties

        #region Elements

        private IReadOnlyCollection<IWebElement> ManufacturerPictureElements => WrappedDriver.FindElements(ManufacturerPictureSelector);
        private IReadOnlyCollection<IWebElement> ManufacturerTitleElements => WrappedDriver.FindElements(ManufacturerTitleSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the manufacturers.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> GetManufacturers()
        {
            var manufacturers = ManufacturerTitleElements
                .Select(e => e.TextHelper().InnerText);

            return manufacturers;
        }

        /// <summary>
        /// Selects the manufacturer.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual ProductsByManufacturerPage SelectManufacturer(string manufacturer,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            ManufacturerTitleElements
                .First(e => String.Equals(
                    manufacturer,
                    e.TextHelper().InnerText,
                    stringComparison))
                .Click();

            return pageObjectFactory.PreparePage<ProductsByManufacturerPage>();
        }

        #endregion
    }
}
