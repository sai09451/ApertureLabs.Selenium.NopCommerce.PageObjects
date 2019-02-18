using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CustomerNavigation
{
    /// <summary>
    /// The customer account navigation component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.FluidPageComponent{T}" />
    public class CustomerNavigationComponent<T> : FluidPageComponent<T>
    {
        #region Fields

        #region Selectors

        private readonly By activeTabSelector = By.CssSelector(".list .active");
        private readonly By allTabsSelector = By.CssSelector(".list a");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNavigationComponent{T}"/> class.
        /// </summary>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="selector">The selector.</param>
        public CustomerNavigationComponent(
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            T parent,
            By selector = null)
            : base(selector ?? By.CssSelector(".block-account-navigation"),
                  driver,
                  parent)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement ActiveTabElement => WrappedDriver.FindElements(activeTabSelector).FirstOrDefault();
        private IReadOnlyCollection<IWebElement> AllTabElements => WrappedDriver.FindElements(allTabsSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Goes to the tab.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        public virtual U GoToTab<U>(string tabName,
            StringComparison stringComparison = StringComparison.Ordinal)
            where U : IPageObject
        {
            if (!String.Equals(tabName, GetActiveTabName(), stringComparison))
            {
                var tabEl = AllTabElements
                    .FirstOrDefault(e => String.Equals(
                        e.TextHelper().InnerText,
                        tabName,
                        stringComparison));

                if (tabEl == null)
                    throw new NoSuchElementException();

                tabEl.Click();
            }

            return pageObjectFactory.PreparePage<U>();
        }

        /// <summary>
        /// Gets the name of the active tab.
        /// </summary>
        /// <returns></returns>
        public virtual string GetActiveTabName()
        {
            return ActiveTabElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets all tab names.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllTabNames()
        {
            foreach (var tab in AllTabElements)
            {
                yield return tab.TextHelper().InnerText;
            }
        }

        #endregion
    }
}
