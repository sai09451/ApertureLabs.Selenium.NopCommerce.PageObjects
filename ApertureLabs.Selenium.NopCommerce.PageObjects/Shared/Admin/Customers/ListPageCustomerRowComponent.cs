using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// The row component of the admin customer list page grid.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class ListPageCustomerRowComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By checkboxSelector = By.CssSelector(".checkboxGroups");
        private readonly By emailSelector = By.CssSelector("td:nth-child(2)");
        private readonly By nameSelector = By.CssSelector("td:nth-child(3)");
        private readonly By customerRolesSelector = By.CssSelector("td:nth-child(4)");
        private readonly By companyNameSelector = By.CssSelector("td:nth-child(5)");
        private readonly By activeSelector = By.CssSelector("td:nth-child(6) .fa");
        private readonly By createdOnSelector = By.CssSelector("td:nth-child(7)");
        private readonly By lastActiveOnSelector = By.CssSelector("td:nth-child(8)");
        private readonly By editSelector = By.CssSelector("td:last-child .btn");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPageCustomerRowComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public ListPageCustomerRowComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private CheckboxElement CheckboxElement => new CheckboxElement(
            WrappedElement.FindElement(
                checkboxSelector));

        private IWebElement EmailElement => WrappedElement
            .FindElement(emailSelector);

        private IWebElement NameElement => WrappedElement
            .FindElement(nameSelector);

        private IWebElement CustomerRolesElement => WrappedElement
            .FindElement(customerRolesSelector);

        private IWebElement CompanyNameElement => WrappedElement
            .FindElement(companyNameSelector);

        private IWebElement ActiveElement => WrappedElement
            .FindElement(activeSelector);

        private IWebElement CreatedOnElement => WrappedElement
            .FindElement(createdOnSelector);

        private IWebElement LastActiveOnElement => WrappedElement
            .FindElement(lastActiveOnSelector);

        private IWebElement EditElement => WrappedElement
            .FindElement(editSelector);

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Checks/unchecks this row.
        /// </summary>
        /// <param name="check">if set to <c>true</c> [check].</param>
        /// <returns></returns>
        public virtual ListPageCustomerRowComponent Check(bool check)
        {
            CheckboxElement.Check(check);

            return this;
        }

        /// <summary>
        /// Determines whether this row is checked.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsChecked()
        {
            return CheckboxElement.IsChecked;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        public virtual string GetEmail()
        {
            return EmailElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return NameElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Gets the customer roles.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetCustomerRoles()
        {
            return CustomerRolesElement
                .TextHelper()
                .InnerText
                .Split(',')
                .Select(cr => cr.Trim());
        }

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <returns></returns>
        public virtual string GetCompanyName()
        {
            return CompanyNameElement.TextHelper().InnerText;
        }

        /// <summary>
        /// Determines whether the customer is active.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsActive()
        {
            return ActiveElement.Classes().Contains("fa-check");
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> the customer was created on.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetCreatedOn()
        {
            return CreatedOnElement
                .TextHelper()
                .ExtractDateTime("M/d/yyyy H:MM:ss tt");
        }

        /// <summary>
        /// Lasts the activity <see cref="DateTime"/> of the customer.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime LastActivity()
        {
            return LastActiveOnElement
                .TextHelper()
                .ExtractDateTime("M/d/yyyy H:MM:ss tt");
        }

        /// <summary>
        /// Clicks the edit button.
        /// </summary>
        /// <returns></returns>
        public virtual IEditPage Edit()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(2))
                .UntilPageReloads(EditElement, e => e.Click());

            return pageObjectFactory.PreparePage<IEditPage>();
        }

        #endregion
    }
}
