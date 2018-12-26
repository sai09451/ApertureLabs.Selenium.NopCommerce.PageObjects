using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News
{
    /// <summary>
    /// NewsComponent.
    /// </summary>
    public class NewsComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private By ExpanderSelector = By.CssSelector("[data-widget=\"collapse\"]");
        private By PanelHeadingSelector => By.CssSelector(".panel-heading");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="driver"></param>
        public NewsComponent(IWebDriver driver)
            : base(driver, By.ClassName("#nopcommerce-news-box"))
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

        private IWebElement ExpanderElement => WrappedElement.FindElement(ExpanderSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of news items.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<NewsItem> NewsItems()
        {
            return WrappedElement.FindElements(PanelHeadingSelector)
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
