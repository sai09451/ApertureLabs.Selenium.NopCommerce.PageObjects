using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    public class EditPage : PageObject, IEditPage
    {
        #region Fields

        #region Selectors

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
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

        public IAdminMainSideBarComponent MainSideBar => basePage.MainSideBar;

        public IAdminMainHeaderComponent NavigationBar => basePage.NavigationBar;

        public AdminFooterComponent Footer => basePage.Footer;

        #endregion

        #region Methods

        public IListPage BackToCustomerList()
        {
            throw new System.NotImplementedException();
        }

        public void BackToTop()
        {
            basePage.BackToTop();
        }

        public IListPage Delete()
        {
            throw new System.NotImplementedException();
        }

        public bool IsAjaxBusy()
        {
            return basePage.IsAjaxBusy();
        }

        public IListPage Save()
        {
            throw new System.NotImplementedException();
        }

        public IEditPage SaveAndContinueEdit()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
