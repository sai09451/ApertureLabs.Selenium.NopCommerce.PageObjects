using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.News;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

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

        public IAdminMainSideBarComponent MainSideBar => basePage.MainSideBar;

        public IAdminMainHeaderComponent NavigationBar => basePage.NavigationBar;

        #endregion

        #region Methods

        public void BackToTop()
        {
            basePage.BackToTop();
        }

        #endregion
    }
}
