using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News
{
    /// <summary>
    /// NewsComponent.
    /// </summary>
    public class NewsComponent : PageComponent, INewsComponent
    {
        #region Fields

        #region Selectors

        private readonly By expanderSelector = By.CssSelector("[data-widget=\"collapse\"]");
        private readonly By panelHeadingSelector = By.CssSelector(".panel-heading");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsComponent"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public NewsComponent(IWebDriver driver)
            : base(By.ClassName("#nopcommerce-news-box"), driver)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Used to get/set if the news element box is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return !WrappedElement.Classes().Contains("collapsed-box");
            }
            set
            {
                if (value != IsExpanded)
                    ExpanderElement.Click();
            }
        }

        #region Elements

        private IWebElement ExpanderElement => WrappedElement.FindElement(expanderSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of news items.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<NewsItem> NewsItems()
        {
            return WrappedElement.FindElements(panelHeadingSelector)
                .Select(e => new NewsItem(e))
                .ToList();
        }

        /// <ineritdoc/>
        public override bool IsStale()
        {
            return WrappedElement.IsStale();
        }

        #endregion
    }
}
