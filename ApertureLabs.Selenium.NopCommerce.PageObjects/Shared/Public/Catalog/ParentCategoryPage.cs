using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog
{
    /// <summary>
    /// ParentCategoryPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog.IParentCategoryPage" />
    public class ParentCategoryPage : CatalogTemplatePage, IParentCategoryPage
    {
        #region Fields

        #region Selectors

        private readonly By categoriesSelector = By.CssSelector(".sub-category-item a");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ParentCategoryPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public ParentCategoryPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(basePage, pageObjectFactory, driver)
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private IReadOnlyCollection<IWebElement> CategoryElements => WrappedDriver.FindElements(categoriesSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the sub categories.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSubCategories()
        {
            return CategoryElements.Select(e => e.TextHelper().InnerText);
        }

        #endregion
    }
}
