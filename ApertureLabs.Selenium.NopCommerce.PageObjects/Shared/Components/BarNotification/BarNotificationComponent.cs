using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.BarNotification
{
    /// <summary>
    /// Represents the '#bar-notification' element on the public pages.
    /// </summary>
    public class BarNotificationComponent<T> : FluidPageComponent<T>
    {
        #region Fields

        #region Selectors

        private readonly By closeSelector = By.CssSelector(".close");
        private readonly By contentSelector = By.CssSelector(".content");
        private readonly By contentLinksSelector = By.CssSelector(".content a");

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BarNotificationComponent{T}"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="parent"></param>
        /// <param name="selector">
        /// The selector. Defaults to <c>By.FromCss("#bar-notification")</c>.
        /// </param>
        public BarNotificationComponent(
            IWebDriver driver,
            T parent,
            By selector = null)
            : base(selector ?? By.CssSelector("bar-notification"),
                  driver,
                  parent)
        { }

        #endregion

        #region Properties

        #region Elements

        private IWebElement CloseElement => WrappedElement.FindElement(closeSelector);
        private IWebElement ContentElement => WrappedElement.FindElement(contentSelector);
        private IReadOnlyList<IWebElement> LinkElements => WrappedElement.FindElements(contentLinksSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Closes this component and returns the parent page.
        /// </summary>
        /// <returns></returns>
        public virtual T Close()
        {
            CloseElement.Click();

            return Parent();
        }

        /// <summary>
        /// Retrieves the InnerText of the content.
        /// </summary>
        /// <returns></returns>
        public virtual string GetContent()
        {
            return ContentElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Retrieves all link elements in the content.
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyList<IWebElement> GetLinks()
        {
            return LinkElements;
        }

        /// <summary>
        /// Determines whether this bar was displayed in response to a
        /// successfull action or an error action.
        /// </summary>
        public virtual bool IsSuccessNotification()
        {
            return WrappedElement.Classes().Contains("success");
        }

        #endregion
    }
}
