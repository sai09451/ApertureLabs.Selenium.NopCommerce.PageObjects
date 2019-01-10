using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.Pager
{
    /// <summary>
    /// Represents the pager component found on the public catalog page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class PagerComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By FirstPageSelector = By.CssSelector(".first-page > a");
        private readonly By LastPageSelector = By.CssSelector(".last-page > a");
        private readonly By PreviousPageSelector = By.CssSelector(".previous-page > a");
        private readonly By NextPageSelector = By.CssSelector(".next-page > a");
        private readonly By PagesSelector = By.CssSelector(".individual-page,.current-page");
        private readonly By SelectedPageSelector = By.CssSelector(".current-page");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerComponent"/>
        /// class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        public PagerComponent(IWebDriver driver, By selector)
            : base(driver, selector)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is on first page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is on first page; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsOnFirstPage => FirstPageElement == null;

        /// <summary>
        /// Gets a value indicating whether this instance is on last page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is on last page; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsOnLastPage => LastPageElement == null;

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasNextPage => NextPageElement != null;

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasPreviousPage => PreviousPageElement != null;

        #region Elements

        private IWebElement FirstPageElement => WrappedElement.FindElements(FirstPageSelector).FirstOrDefault();
        private IWebElement LastPageElement => WrappedElement.FindElements(LastPageSelector).FirstOrDefault();
        private IWebElement PreviousPageElement => WrappedElement.FindElements(PreviousPageSelector).FirstOrDefault();
        private IWebElement NextPageElement => WrappedElement.FindElements(NextPageSelector).FirstOrDefault();
        private IReadOnlyList<IWebElement> PageElements => WrappedElement.FindElements(PagesSelector);
        private IWebElement SelectedPageElement => WrappedElement.FindElement(SelectedPageSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <returns></returns>
        public virtual int GetCurrentPage()
        {
            return SelectedPageElement.TextHelper().ExtractInteger();
        }

        /// <summary>
        /// Gets the listed pages.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<int> GetListedPages()
        {
            return PageElements.Select(
                e => e.TextHelper().ExtractInteger());
        }

        /// <summary>
        /// Sets the current page if not already on that page.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        public virtual void SetCurrentPage(int pageNumber)
        {
            if (GetCurrentPage() == pageNumber)
                return;

            PageElements
                .First(e => e.TextHelper().ExtractInteger() == pageNumber)
                .Click();
        }

        /// <summary>
        /// Goes to the first page if not already on the first page.
        /// </summary>
        public virtual void GoToFirstPage()
        {
            FirstPageElement?.Click();
        }

        /// <summary>
        /// Goes to the last page if not already on the last page.
        /// </summary>
        public virtual void GoToLastPage()
        {
            LastPageElement?.Click();
        }

        /// <summary>
        /// Previouses the page if available.
        /// </summary>
        public virtual void PreviousPage()
        {
            PreviousPageElement?.Click();
        }

        /// <summary>
        /// Nexts the page if available.
        /// </summary>
        public virtual void NextPage()
        {
            NextPageElement?.Click();
        }

        #endregion
    }
}
