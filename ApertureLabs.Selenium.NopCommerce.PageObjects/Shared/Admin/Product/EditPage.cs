using System;
using System.Collections.Generic;
using System.Linq;
using ApertureLabs.Selenium.Components.Boostrap.Navs;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.EditorSettings;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Product
{

    /// <summary>
    /// Corresponds to the "Admin/Views/Product/Edit.cshtml" page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.BasePage" />
    public class EditPage : ParameterPageObject, IEditPage,
        IHasAdvancedOptionsPage
    {
        #region Fields

        #region Selectors

        private readonly By backToProductListSelector = By.CssSelector("#product-form > div.content-header.clearfix > h1 > small > a");
        private readonly By advancedSwitchSelector = By.CssSelector(".onoffswitch");
        private readonly By settingsBySelector = By.CssSelector("#product-editor-settings");
        private readonly By previewButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button.btn.bg-purple");
        private readonly By saveButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button:nth-child(2)");
        private readonly By saveAndContinueEditButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button:nth-child(3)");
        private readonly By copyProductButtonSelector = By.CssSelector("#product-form > div.content-header.clearfix > div > button.btn.bg-olive");
        private readonly By deleteButtonSelector = By.CssSelector("#product-delete");
        private readonly By navsTabComponentSelector = By.CssSelector("#product-edit");
        private readonly By productInfoComponentSelector = By.CssSelector("#tab-info");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPage"/> class.
        /// </summary>
        /// <param name="basePage"></param>
        /// <param name="pageObjectFactory"></param>
        /// <param name="driver"></param>
        /// <param name="pageSettings"></param>
        public EditPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  pageSettings.AdminBaseUrl,
                  new UriTemplate("Product/Edit/{id}"))
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;

            AdvancedOptions = new EditorSettingsComponent(
                advancedSwitchSelector,
                settingsBySelector,
                new EditorSettingsComponentConfiguration(),
                WrappedDriver);

            Tabs = new NavsTabComponent<IEditPage>(
                navsTabComponentSelector,
                new ILoadableComponent[]
                {
                    GeneralInfoTab
                },
                WrappedDriver,
                new NavsTabComponentConfiguration
                {
                    ActiveTabContentElementSelector = By.CssSelector(".tab-content .tab-pane.active"),
                    ActiveTabHeaderElementSelector = By.CssSelector(".navs-tab > .active"),
                    ActiveTabHeaderNameSelector = By.CssSelector(".navs-tab > .active > a"),
                    TabHeaderElementsSelector = By.CssSelector(".navs-tab > li"),
                    TabHeaderNamesSelector = By.CssSelector(".navs-tab > li > a")
                },
                this);

            GeneralInfoTab = new ProductInfoComponent(
                productInfoComponentSelector,
                this,
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement BackToProductListElement => WrappedDriver.FindElement(backToProductListSelector);
        private IWebElement PreviewButtonElement => WrappedDriver.FindElement(previewButtonSelector);
        private IWebElement SaveButtonElement => WrappedDriver.FindElement(saveButtonSelector);
        private IWebElement SaveAndContinueButtonElement => WrappedDriver.FindElement(saveAndContinueEditButtonSelector);
        private IWebElement CopyProductButtonElement => WrappedDriver.FindElement(copyProductButtonSelector);
        private IWebElement DeleteButtonElement => WrappedDriver.FindElement(deleteButtonSelector);

        #endregion

        /// <summary>
        /// Gets the general information tab.
        /// </summary>
        /// <value>
        /// The general information tab.
        /// </value>
        public virtual ProductInfoComponent GeneralInfoTab { get; private set; }

        /// <summary>
        /// Gets the tabs.
        /// </summary>
        /// <value>
        /// The tabs.
        /// </value>
        public virtual NavsTabComponent<IEditPage> Tabs { get; private set; }

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
        /// Gets the advanced options component.
        /// </summary>
        /// <value>
        /// The advanced options component.
        /// </value>
        public virtual EditorSettingsComponent AdvancedOptions { get; }

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

            AdvancedOptions.Load();
            Tabs.Load();
            GeneralInfoTab.Load();

            return this;
        }

        /// <summary>
        /// Backs to product list.
        /// </summary>
        /// <returns></returns>
        public virtual IListPage BackToProductList()
        {
            BackToProductListElement.Click();

            return pageObjectFactory.PreparePage<ListPage>();
        }

        /// <summary>
        /// Previews product and switches to the new window.
        /// </summary>
        /// <param name="switchToNewWindow">if set to <c>true</c> [switch to new window].</param>
        /// <returns></returns>
        public virtual string Preview(bool switchToNewWindow = true)
        {
            var oldWindowHandles = WrappedDriver.WindowHandles;
            PreviewButtonElement.Click();

            WrappedDriver
                .Wait(TimeSpan.FromSeconds(10))
                .Until(d => d.WindowHandles.Count != oldWindowHandles.Count);

            var newHandle = WrappedDriver.WindowHandles
                .Except(oldWindowHandles)
                .First();

            if (switchToNewWindow)
                WrappedDriver.SwitchTo().Window(newHandle);

            return newHandle;
        }

        /// <summary>
        /// Saves this product and returns the list.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IListPage Save()
        {
            SaveButtonElement.Click();

            return pageObjectFactory.PreparePage<ListPage>();
        }

        /// <summary>
        /// Saves the and continue edit.
        /// </summary>
        /// <returns></returns>
        public virtual IEditPage SaveAndContinueEdit()
        {
            SaveAndContinueButtonElement.Click();

            return pageObjectFactory.PreparePage<EditPage>();
        }

        /// <summary>
        /// Copies the product.
        /// TODO: Create a 'CopyProductComponent' and return that instead of
        /// the PageComponent.
        /// </summary>
        /// <returns></returns>
        public virtual PageComponent CopyProduct()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <returns></returns>
        public virtual IListPage Delete()
        {
            DeleteButtonElement.Click();

            return pageObjectFactory.PreparePage<ListPage>();
        }

        /// <summary>
        /// Goes to tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="stringComparison"></param>
        public virtual void GoToTab(string tabName,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the tab names.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IEnumerable<string> GetTabNames()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the name of the active tab.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual string GetActiveTabName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Backs to top.
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
