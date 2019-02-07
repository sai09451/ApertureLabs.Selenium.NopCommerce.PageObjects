using System;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.CustomerNavigation;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// IInfoPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public interface IInfoPage : IBasePage
    {
        /// <summary>
        /// Gets the account navigation.
        /// </summary>
        /// <value>
        /// The account navigation.
        /// </value>
        CustomerNavigationComponent<IInfoPage> AccountNavigation { get; }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        /// <returns></returns>
        string GetGender();

        /// <summary>
        /// Sets the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <param name="stringComparison">The string comparison.</param>
        /// <returns></returns>
        IInfoPage SetGender(string gender,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns></returns>
        string GetFirstName();

        /// <summary>
        /// Sets the first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <returns></returns>
        IInfoPage SetFirstName(string firstName);

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <returns></returns>
        string GetLastName();

        /// <summary>
        /// Sets the last name.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        IInfoPage SetLastName(string lastName);

        /// <summary>
        /// Gets the date of birth.
        /// </summary>
        /// <returns></returns>
        DateTime? GetDateOfBirth();

        /// <summary>
        /// Sets the date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns></returns>
        IInfoPage SetDateOfBirth(DateTime? dateOfBirth);

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        string GetEmail();

        /// <summary>
        /// Sets the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        IInfoPage SetEmail(string email);

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <returns></returns>
        string GetCompanyName();

        /// <summary>
        /// Sets the name of the company.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        IInfoPage SetCompanyName(string companyName);

        /// <summary>
        /// Gets the is registered for news letter.
        /// </summary>
        /// <returns></returns>
        bool GetIsRegisteredForNewsLetter();

        /// <summary>
        /// Sets the is registered for news letter.
        /// </summary>
        /// <param name="isRegistered">if set to <c>true</c> [is registered].</param>
        /// <returns></returns>
        IInfoPage SetIsRegisteredForNewsLetter(bool isRegistered);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        IInfoPage Save();
    }
}
