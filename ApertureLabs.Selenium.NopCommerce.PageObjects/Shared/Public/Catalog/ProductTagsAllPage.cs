using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// The 'All Tags' page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.CatalogTemplatePage" />
    public class ProductTagsAllPage : CatalogTemplatePage
    {
        #region Fields

        #region Selectors

        private readonly By TagsSelector = By.CssSelector(".product-tags-list .producttag");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTagsAllPage"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        public ProductTagsAllPage(IWebDriver driver,
            PageSettings pageSettings,
            IPageObjectFactory pageObjectFactory)
            : base(pageObjectFactory, driver, pageSettings)
        { }

        #endregion

        #region Properties

        #region Elements

        private IReadOnlyCollection<IWebElement> TagElements => WrappedDriver.FindElements(TagsSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllTags()
        {
            return TagElements.Select(e => e.TextHelper().InnerText);
        }

        /// <summary>
        /// Selects the tag.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="stringComparison">The string comparison.</param>
        public void SelectTag(string tagName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            TagElements.First(e => String.Equals(
                    tagName,
                    e.TextHelper().InnerText,
                    stringComparison))
                .Click();
        }

        #endregion
    }
}
