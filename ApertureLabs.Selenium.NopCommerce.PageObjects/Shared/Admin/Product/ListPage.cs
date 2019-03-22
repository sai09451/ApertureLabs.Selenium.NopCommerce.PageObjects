using System;
using System.Collections;
using System.Collections.Generic;
using ApertureLabs.Selenium.Components.Kendo;
using ApertureLabs.Selenium.Components.Kendo.KGrid;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
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
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public ListPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  pageSettings.AdminBaseUrl,
                  new UriTemplate("Product/List"))
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));

            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            ProductsGrid = new KGridComponent<IListPage>(
                new BaseKendoConfiguration(),
                By.CssSelector("#products-grid"),
                pageObjectFactory,
                WrappedDriver,
                this);

            SearchPanel = new SearchPanelComponent(
                By.CssSelector(".panel-search"),
                pageObjectFactory,
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        #endregion

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

        /// <summary>
        /// Gets the products grid.
        /// </summary>
        /// <value>
        /// The products grid.
        /// </value>
        public virtual KGridComponent<IListPage> ProductsGrid { get; }

        /// <summary>
        /// Gets the search panel.
        /// </summary>
        /// <value>
        /// The search panel.
        /// </value>
        public virtual ISearchPanelComponent SearchPanel { get; }

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
            ProductsGrid.Load();
            SearchPanel.Load();

            return this;
        }

        /// <summary>
        /// Gets the listed products.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IEnumerable<ListPageProductRowComponent> GetListedProducts()
        {
            foreach (var rowElement in ProductsGrid.EnumerateOverAllRows())
            {
                yield return pageObjectFactory.PrepareComponent(
                    new ListPageProductRowComponent(
                        new ByElement(rowElement),
                        pageObjectFactory,
                        WrappedDriver));
            }
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
        }

        /// <summary>
        /// Determines whether the ajax busy element is present and visible.
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is ajax busy]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsAjaxBusy()
        {
            return basePage.IsAjaxBusy();
        }

        /// <summary>
        /// Navigates to the create new product page.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual ICreatePage AddNew()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Locates the export format and begins the export.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void ExportTo(string format)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="pathToFile">The path to file.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IListPage Import(string pathToFile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the selected products.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IListPage DeleteSelected()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasNotifications()
        {
            return basePage.HasNotifications();
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void HandleNotification(Action<IWebElement> element)
        {
            basePage.HandleNotification(element);
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        #endregion
    }
}
