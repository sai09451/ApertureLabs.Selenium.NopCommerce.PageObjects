using System;
using System.Collections;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{
    /// <summary>
    /// Product list page.
    /// </summary>
    public class ListPage : PageObject, IListPage
    {
        #region Fields

        #region Selectors

        private readonly By searchPanelSelector = By.CssSelector(".panel-search");

        #endregion

        private readonly IBasePage basePage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="driver">The driver.</param>
        public ListPage(IBasePage basePage, IWebDriver driver) : base(driver)
        {
            this.basePage = basePage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the main side bar.
        /// </summary>
        /// <value>
        /// The main side bar.
        /// </value>
        public virtual IAdminMainSideBarComponent MainSideBar => basePage.MainSideBar;

        /// <summary>
        /// Gets the navigation bar.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        public virtual IAdminMainHeaderComponent NavigationBar => basePage.NavigationBar;

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public virtual AdminFooterComponent Footer => basePage.Footer;

        #region Elements

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// If overridding this don't forget to call base.Load().
        /// NOTE: Will navigate to the pages url if the current drivers url
        /// is empty.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// If the driver is an EventFiringWebDriver an event listener will
        /// be added to the 'Navigated' event and uses the url to determine
        /// if the page is 'stale'.
        /// </remarks>
        public override ILoadableComponent Load()
        {
            base.Load();
            basePage.Load();

            return this;
        }

        /// <summary>
        /// Gets the listed products.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IEnumerable<object> GetListedProducts()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
        }

        #endregion
    }
}
