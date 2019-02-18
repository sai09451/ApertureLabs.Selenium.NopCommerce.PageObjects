using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar
{
    /// <summary>
    /// The default implementation of the main sidebar on the admin pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class AdminMainSideBarComponent : PageComponent,
        IAdminMainSideBarComponent
    {
        #region Fields

        #region Selectors

        private readonly By parentTreeSelector = By.CssSelector(".sidebar-menu.tree");
        private readonly By searchBoxSelector = By.CssSelector("#search-box .admin-search-box");
        private readonly By searchBoxResultsSelector = By.CssSelector("#search-box .tt-menu .tt-suggestion");
        private readonly By searchBoxEmptyResultSelector = By.CssSelector("#search-box .tt-menu .empty-message");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminMainSideBarComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public AdminMainSideBarComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement ParentTreeElement => WrappedElement
            .FindElement(parentTreeSelector);

        private InputElement SearchBoxElement => new InputElement(
            WrappedElement.FindElement(searchBoxSelector));

        private IReadOnlyCollection<IWebElement> SearchBoxResultElements =>
            WrappedElement.FindElements(searchBoxResultsSelector);

        private IWebElement SearchBoxEmptyMessageElement => WrappedElement
            .FindElements(searchBoxEmptyResultSelector)
            .FirstOrDefault();

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAdminMainSideBarNode> GetItems()
        {
            foreach (var el in ParentTreeElement.Children())
            {
                yield return pageObjectFactory.PrepareComponent(
                    new AdminMainSideBarNode(
                        ByElement.FromElement(el),
                        null,
                        pageObjectFactory,
                        WrappedDriver));
            }
        }

        /// <summary>
        /// Searches the specified search term and goes to the first result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public T Search<T>(string searchTerm) where T : IPageObject
        {
            SearchBoxElement.SetValue(searchTerm);

            if (SearchBoxEmptyMessageElement != null)
                throw new NoSuchElementException();

            SearchBoxResultElements
                .First()
                .Click();

            return pageObjectFactory.PreparePage<T>();
        }

        #endregion
    }
}
