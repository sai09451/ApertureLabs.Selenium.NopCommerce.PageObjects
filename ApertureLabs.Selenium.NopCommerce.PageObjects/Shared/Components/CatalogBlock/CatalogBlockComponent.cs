using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CategoryNavigation
{
    /// <summary>
    /// A base component class for the partial 'block' views on the catalog
    /// page.
    /// </summary>
    public class CatalogBlockComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By TitleSelector = By.CssSelector(".title");
        private readonly By ListBoxItemsSelector = By.CssSelector(".listbox > .list li");
        private readonly By ViewAllSelector = By.CssSelector(".view-all > a");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogBlockComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="driver">The driver.</param>
        public CatalogBlockComponent(By selector, IWebDriver driver)
            : base(selector, driver)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance has view all.
        /// </summary>
        protected virtual bool HasViewAll => ViewAllElement != null;

        #region Elements

        private IWebElement TitleElement => WrappedElement.FindElement(TitleSelector);
        private IWebElement ViewAllElement => WrappedElement.FindElements(ViewAllSelector).FirstOrDefault();
        private IReadOnlyCollection<IWebElement> ListBoxItemElements => WrappedElement.FindElements(ListBoxItemsSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <returns></returns>
        public virtual string GetTitle()
        {
            return TitleElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IWebElement> GetItems()
        {
            return ListBoxItemElements;
        }

        /// <summary>
        /// Clicks the 'View all' link. Will throw a
        /// <code>NoSuchElementException</code> if there isn't a ViewAll option.
        /// </summary>
        public virtual void ViewAll()
        {
            ViewAllElement.Click();
        }

        #endregion
    }
}
