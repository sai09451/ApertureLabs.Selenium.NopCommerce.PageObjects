using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Home
{
    /// <summary>
    /// The admin home page.
    /// </summary>
    public class HomePage : PageObject, IHomePage
    {
        #region Fields

        #region Selectors

        private By NewsComponentSelector => By.CssSelector("#nopcommerce-news-box");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public HomePage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver)
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        /// <summary>
        /// News section of the home page.
        /// </summary>
        public virtual INewsComponent News => pageObjectFactory
            .PrepareComponent(new NewsComponent(WrappedDriver));

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
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
        }

        #endregion
    }
}
