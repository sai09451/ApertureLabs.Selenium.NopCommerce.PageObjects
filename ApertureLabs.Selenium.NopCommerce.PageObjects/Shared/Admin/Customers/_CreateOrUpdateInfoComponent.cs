using ApertureLabs.Selenium.Components.Kendo.KDatePicker;
using ApertureLabs.Selenium.Components.Kendo.KMultiSelect;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.PageObjects;
using ApertureLabs.Selenium.WebElements.Inputs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Customers
{
    /// <summary>
    /// Corresponds to the _CreateOrUpdate.Info.cshtml view.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageComponent" />
    public class _CreateOrUpdateInfoComponent : PageComponent
    {
        #region Fields

        #region Selectors

        private readonly By emailSelector = By.CssSelector("#Email");
        private readonly By passwordSelector = By.CssSelector("#Password");
        private readonly By customerRolesSelector = By.CssSelector("#SelectedCustomerRoleIds");
        private readonly By managerOfVendorSelector = By.CssSelector("#VendorId");
        private readonly By genderRadioSelector = By.CssSelector("*[name='Gender']");
        private readonly By firstNameSelector = By.CssSelector("#FirstName");
        private readonly By lastNameSelector = By.CssSelector("#LastName");
        private readonly By dateOfBirthSelector = By.CssSelector("#DateOfBirth");
        private readonly By companyNameSelector = By.CssSelector("#Company");
        private readonly By adminCommentSelector = By.CssSelector("#AdminComment");
        private readonly By taxExemptSelector = By.CssSelector("#IsTaxExempt");
        private readonly By newsLettersSelector = By.CssSelector("*[name='SelectedNewsletterSubscriptionStoreIds']");
        private readonly By activeSelector = By.CssSelector("#Active");
        private readonly By changePasswordSelector = By.CssSelector("*[name='changepassword']");

        #endregion

        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="_CreateOrUpdateInfoComponent"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        public _CreateOrUpdateInfoComponent(By selector,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver)
            : base(selector, driver)
        {
            this.pageObjectFactory = pageObjectFactory
                ?? throw new ArgumentNullException(nameof(pageObjectFactory));

            CustomerRolesElement = new KMultiSelectComponent<_CreateOrUpdateInfoComponent>(
                customerRolesSelector,
                driver,
                new KMultiSelectConfiguration(),
                this);

            DateOfBirthElement = new KDatePickerComponent<_CreateOrUpdateInfoComponent>(
                new KDatePickerConfiguration(),
                dateOfBirthSelector,
                driver,
                this);
        }

        #endregion

        #region Properties

        #region Elements

        private InputElement EmailElement => new InputElement(
            WrappedElement.FindElement(
                emailSelector));

        private InputElement PasswordElement => new InputElement(
            WrappedDriver.FindElement(
                passwordSelector));

        private SelectElement ManagerOfVendorElement => new SelectElement(
            WrappedDriver.FindElement(
                managerOfVendorSelector));

        private RadioElementSet GenderElements => new RadioElementSet(
            WrappedDriver.FindElements(
                genderRadioSelector));

        private InputElement FirstNameElement => new InputElement(
            WrappedDriver.FindElement(
                firstNameSelector));

        private InputElement LastNameElement => new InputElement(
            WrappedDriver.FindElement(
                lastNameSelector));

        private InputElement CompanyNameElement => new InputElement(
            WrappedDriver.FindElement(
                companyNameSelector));

        private InputElement AdminCommentElement => new InputElement(
            WrappedDriver.FindElement(
                companyNameSelector));

        private CheckboxElement TaxExemptElement => new CheckboxElement(
            WrappedDriver.FindElement(
                taxExemptSelector));

        private IReadOnlyCollection<CheckboxElement> NewsLetterElements => WrappedDriver
            .FindElements(newsLettersSelector)
            .Select(e => new CheckboxElement(e))
            .ToList()
            .AsReadOnly();

        private CheckboxElement ActiveElement => new CheckboxElement(
            WrappedDriver.FindElement(
                activeSelector));

        private IWebElement ChangePasswordElement => WrappedDriver
            .FindElements(changePasswordSelector)
            .FirstOrDefault();

        #endregion

        private KMultiSelectComponent<_CreateOrUpdateInfoComponent> CustomerRolesElement { get; }

        private KDatePickerComponent<_CreateOrUpdateInfoComponent> DateOfBirthElement { get; }

        #endregion

        #region Methods

        /// <summary>
        /// If overloaded don't forget to call base.Load() or make sure to
        /// assign the WrappedElement.
        /// </summary>
        /// <returns></returns>
        public override ILoadableComponent Load()
        {
            base.Load();
            CustomerRolesElement.Load();

            return this;
        }

        /// <summary>
        /// Enters the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent EnterEmail(string email)
        {
            EmailElement.SetValue(email);

            return this;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        public string GetEmail()
        {
            return EmailElement.GetValue<string>();
        }

        /// <summary>
        /// Enters the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent EnterPassword(string password)
        {
            PasswordElement.SetValue(password);
            var changePasswordEl = ChangePasswordElement;

            if (changePasswordSelector != null)
            {
                WrappedDriver
                    .Wait(TimeSpan.FromSeconds(2))
                    .UntilPageReloads(changePasswordEl, e => e.Click());
            }

            return pageObjectFactory.PrepareComponent(this);
        }

        /// <summary>
        /// Sets the customer roles.
        /// </summary>
        /// <param name="customerRoles">The customer roles.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent SetCustomerRoles(
            IEnumerable<string> customerRoles)
        {
            var selectedItems = CustomerRolesElement.GetSelectedOptions();
            var itemsToSelect = customerRoles.Except(selectedItems);

            if (customerRoles?.Any() ?? true)
            {
                // Remove all items.
                foreach (var item in selectedItems)
                    CustomerRolesElement.DeselectItem(item);
            }
            else
            {
                // Remove all items not in customerRoles.
                foreach (var selectedItem in selectedItems)
                {
                    if (customerRoles.Contains(selectedItem))
                        continue;

                    // Remove item.
                    CustomerRolesElement.DeselectItem(selectedItem);
                }

                // Add items from customer roles that aren't already selected.
                foreach (var item in itemsToSelect)
                    CustomerRolesElement.SelectItem(item);
            }

            return this;
        }

        /// <summary>
        /// Gets the customer roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetCustomerRoles()
        {
            return CustomerRolesElement.GetSelectedOptions();
        }

        /// <summary>
        /// Sets the manager of vendor.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent SetManagerOfVendor(string manager)
        {
            ManagerOfVendorElement.SelectByText(manager);

            return this;
        }

        /// <summary>
        /// Gets the manager of vendor.
        /// </summary>
        /// <returns></returns>
        public string GetManagerOfVendor()
        {
            return ManagerOfVendorElement.SelectedOption
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Sets the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public _CreateOrUpdateInfoComponent SetGender(string gender,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            // Convert gender string to value of radio buttons.
            var value = GenderElements.Options
                .Select((e, i) => new
                {
                    innerText = e.GetParentElement().TextHelper().InnerText,
                    index = i
                })
                .FirstOrDefault(e =>
                    String.Equals(
                        gender,
                        e.innerText,
                        stringComparison));

            if (value == null)
                throw new NoSuchElementException();

            GenderElements.SelectByIndex(value.index);

            return this;
        }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        /// <returns></returns>
        public string GetGender()
        {
            return GenderElements.SelectedOption
                .GetParentElement()
                .TextHelper()
                .InnerText;
        }

        /// <summary>
        /// Enters the first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent EnterFirstName(string firstName)
        {
            FirstNameElement.SetValue(firstName);

            return this;
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns></returns>
        public string GetFirstName()
        {
            return FirstNameElement.GetValue<string>();
        }

        /// <summary>
        /// Enters the last name.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent EnterLastName(string lastName)
        {
            LastNameElement.SetValue(lastName);

            return this;
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <returns></returns>
        public string GetLastName()
        {
            return LastNameElement.GetValue<string>();
        }

        /// <summary>
        /// Enters the date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent EnterDateOfBirth(
            DateTime? dateOfBirth)
        {
            DateOfBirthElement.SetValue(dateOfBirth);

            return this;
        }

        /// <summary>
        /// Gets the date of birth.
        /// </summary>
        /// <returns></returns>
        public DateTime? GetDateOfBirth()
        {
            return DateOfBirthElement.GetValue();
        }

        /// <summary>
        /// Enters the name of the company.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent EnterCompanyName(
            string companyName)
        {
            CompanyNameElement.SetValue(companyName);

            return this;
        }

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <returns></returns>
        public string GetCompanyName()
        {
            return CompanyNameElement.GetValue<string>();
        }

        /// <summary>
        /// Enters the admin comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent EnterAdminComment(string comment)
        {
            AdminCommentElement.SetValue(comment);

            return this;
        }

        /// <summary>
        /// Gets the admin comment.
        /// </summary>
        /// <returns></returns>
        public string GetAdminComment()
        {
            return AdminCommentElement.GetValue<string>();
        }

        /// <summary>
        /// Sets the tax excempt.
        /// </summary>
        /// <param name="excempt">if set to <c>true</c> [excempt].</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent SetTaxExcempt(bool excempt)
        {
            TaxExemptElement.Check(excempt);

            return this;
        }

        /// <summary>
        /// Gets the tax excempt.
        /// </summary>
        /// <returns></returns>
        public bool GetTaxExcempt()
        {
            return TaxExemptElement.IsChecked;
        }

        /// <summary>
        /// Sets the news letters.
        /// </summary>
        /// <param name="newsLetters">The news letters.</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent SetNewsLetters(IEnumerable<string> newsLetters)
        {
            foreach (var newsLetterEl in NewsLetterElements)
            {
                var parentElText = newsLetterEl
                    .GetParentElement()
                    .TextHelper()
                    .InnerText;

                var check = newsLetters?.Contains(parentElText) ?? false;
                newsLetterEl.Check(check);
            }

            return this;
        }

        /// <summary>
        /// Gets the news letters.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetNewsLetters()
        {
            foreach (var newsLetterEl in NewsLetterElements)
            {
                yield return newsLetterEl.GetParentElement()
                    .TextHelper()
                    .InnerText;
            }
        }

        /// <summary>
        /// Sets the is active.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <returns></returns>
        public _CreateOrUpdateInfoComponent SetIsActive(bool active)
        {
            ActiveElement.Check(active);

            return this;
        }

        /// <summary>
        /// Gets the is active.
        /// </summary>
        /// <returns></returns>
        public bool GetIsActive()
        {
            return ActiveElement.IsChecked;
        }

        #endregion
    }
}
