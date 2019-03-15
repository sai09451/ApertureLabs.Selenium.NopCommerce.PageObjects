using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Represents the _CreateOrUpdate.Impersonate.cshtml partial view.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class _CreateOrUpdateImpersonateComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By placeOrderSelector = By.CssSelector("");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdateImpersonateComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public _CreateOrUpdateImpersonateComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector,
                  driver)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement PlaceOrderElement => WrappedDriver
            .FindElement(placeOrderSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Begins the impersonation session.
        /// </summary>
        /// <returns></returns>
        public virtual Public.Home.IHomePage PlaceOrder()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .UntilPageReloads(PlaceOrderElement, e => e.Click());

            return pageObjectFactory.PreparePage<Public.Home.IHomePage>();
        }

        #endregion
    }
}
