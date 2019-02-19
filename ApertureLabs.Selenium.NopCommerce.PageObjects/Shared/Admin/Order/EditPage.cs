using System;
using ApertureLabs.Selenium.Components.Boostrap.Navs;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order
{
    public class EditPage : PageObject, IEditPage
    {
        #region Fields

        #region Selectors

        private readonly By tabsSelector = By.CssSelector(".nav-tabs.nav");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        public EditPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(driver)
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;

            Tabs = new NavsTabComponent<IEditPage>(
                By.CssSelector(""),
                WrappedDriver,
                new NavsTabComponentConfiguration(),
                this);
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        private NavsTabComponent<IEditPage> Tabs { get; }

        public IAdminMainSideBarComponent MainSideBar => basePage.MainSideBar;

        public IAdminMainHeaderComponent NavigationBar => basePage.NavigationBar;

        public AdminFooterComponent Footer => basePage.Footer;

        #endregion

        #region Methods

        public IListPage BackToOrderList()
        {
            throw new NotImplementedException();
        }

        public void BackToTop()
        {
            throw new NotImplementedException();
        }

        public bool IsAjaxBusy()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
