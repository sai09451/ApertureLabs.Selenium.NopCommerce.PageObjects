using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News
{
    /// <summary>
    /// NewsItem.
    /// </summary>
    public class NewsItem : IWrapsElement
    {
        #region Fields

        #region Selectors

        private By PanelHeadingSelector => By.CssSelector(".panel-heading");
        private By PanelBodySelector => By.CssSelector(".panel-body");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="element"></param>
        public NewsItem(IWebElement element)
        {
            WrappedElement = element;
        }

        #endregion

        #region Properties

        #region Elements

        /// <summary>
        /// WrappedElement.
        /// </summary>
        public IWebElement WrappedElement { get; private set; }

        private IWebElement PanelHeadingElement => WrappedElement.FindElement(PanelHeadingSelector);
        private IWebElement PanelBodyElement => WrappedElement.FindElement(PanelBodySelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Go to the news item.
        /// TODO: Implement a news page object and return it here.
        /// </summary>
        public void View()
        {
            PanelHeadingElement.FindElement(By.TagName("a")).Click();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the title of the news article.
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return PanelHeadingElement.Text;
        }

        /// <summary>
        /// Returns the text of the description.
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return PanelBodyElement.TextHelper().InnerHtml;
        }

        /// <summary>
        /// Returns all links in the description.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IWebElement> GetLinksInDescription()
        {
            return PanelBodyElement.FindElements(By.TagName("a"));
        }

        #endregion
    }
}
