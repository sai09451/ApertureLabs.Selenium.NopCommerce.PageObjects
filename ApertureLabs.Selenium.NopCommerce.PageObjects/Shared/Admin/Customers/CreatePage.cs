using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Customers;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Default implementation of the admin create customer page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.StaticPageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers.ICreatePage" />
    public class CreatePage : StaticPageObject, ICreatePage
    {
        #region Fields

        #region Selectors

        private readonly By backToCustomerListSelector = By.CssSelector("*[href='/Admin/Customer/List']");
        private readonly By saveSelector = By.CssSelector("*[name='save']");
        private readonly By saveAndContinueSelector = By.CssSelector("*[name='save-continue']");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        /// <exception cref="ArgumentNullException">
        /// basePage
        /// or
        /// pageObjectFactory
        /// </exception>
        public CreatePage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  new Uri(pageSettings.AdminBaseUrl, "Customer/Create"))
        {
            this.basePage = basePage
                ?? throw new ArgumentNullException(nameof(basePage));

            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            Info = new _CreateOrUpdateInfoComponent(
                By.CssSelector(""),
                pageObjectFactory,
                WrappedDriver);
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement BackToCustomerListElement => WrappedDriver
            .FindElement(backToCustomerListSelector);

        private IWebElement SaveElement => WrappedDriver
            .FindElement(saveSelector);

        private IWebElement SaveAndContinueElement => WrappedDriver
            .FindElement(saveAndContinueSelector);

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
        /// Gets the Info tab.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        public virtual _CreateOrUpdateInfoComponent Info { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the component. Checks to see if the current url matches
        /// the Route and if not an exception is thrown. If the WrappedDriver
        /// is an <see cref="T:OpenQA.Selenium.Support.Events.EventFiringWebDriver" /> event listeners will be
        /// added to the <see cref="E:OpenQA.Selenium.Support.Events.EventFiringWebDriver.Navigated" /> event
        /// which will call <see cref="M:ApertureLabs.Selenium.PageObjects.PageObject.Dispose" /> on this instance.
        /// NOTE:
        /// If overriding don't forget to either call base.Load() or make sure
        /// the <see cref="P:ApertureLabs.Selenium.PageObjects.PageObject.Uri" /> and the <see cref="P:ApertureLabs.Selenium.PageObjects.PageObject.WindowHandle" /> are
        /// assigned to.
        /// </summary>
        /// <returns>
        /// A reference to this
        /// <see cref="T:OpenQA.Selenium.Support.UI.ILoadableComponent" />.
        /// </returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            pageObjectFactory.PrepareComponent(Info);

            return this;
        }

        /// <summary>
        /// Cancels creating the customer and returns to the customer list.
        /// </summary>
        /// <returns></returns>
        public virtual IListPage BackToCustomerList()
        {
            BackToCustomerListElement.Click();

            return pageObjectFactory.PreparePage<IListPage>();
        }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        public virtual void BackToTop()
        {
            basePage.BackToTop();
        }

        /// <summary>
        /// Enters the models info.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">model</exception>
        public virtual ICreatePage EnterInformation(CustomerCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Info.EnterEmail(model.Email)
                .EnterPassword(model.Password)
                .SetCustomerRoles(model.CustomerRoles)
                .SetManagerOfVendor(model.ManagerOfVendor)
                .SetGender(model.Gender)
                .EnterFirstName(model.FirstName)
                .EnterLastName(model.LastName)
                .EnterDateOfBirth(model.DateOfBirth)
                .EnterCompanyName(model.CompanyName)
                .EnterAdminComment(model.AdminComment)
                .SetTaxExcempt(model.IsTaxExempt)
                .SetNewsLetters(model.NewsLetters)
                .SetIsActive(model.Active);

            return this;
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
        /// Saves the customer and returns to the list page.
        /// </summary>
        /// <returns></returns>
        public virtual IListPage Save()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(SaveElement, el => el.Click());

            return pageObjectFactory.PreparePage<IListPage>();
        }

        /// <summary>
        /// Saves the customer and continues to edit them on the
        /// <see cref="T:ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers.IEditPage" />.
        /// </summary>
        /// <returns></returns>
        public virtual IEditPage SaveAndContinue()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(SaveAndContinueElement, el => el.Click());

            return pageObjectFactory.PreparePage<IEditPage>();
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
