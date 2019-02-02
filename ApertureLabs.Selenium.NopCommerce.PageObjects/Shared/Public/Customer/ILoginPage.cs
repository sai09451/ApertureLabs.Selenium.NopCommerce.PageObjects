using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// LoginPage.
    /// </summary>
    public interface ILoginPage : IBasePage
    {
        /// <summary>
        /// A list of errors on the page.
        /// </summary>
        IEnumerable<string> Errors { get; }

        /// <summary>
        /// Clicks the login button.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>

        void ClickLogin(Action<HomePage> success, Action<LoginPage> error);

        /// <summary>
        /// Enters the users email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        ILoginPage EnterEmail(string email);

        /// <summary>
        /// Enters the users password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        ILoginPage EnterPassword(string password);

        /// <summary>
        /// Checks if the email is invalid.
        /// </summary>
        /// <returns></returns>
        bool HasInvalidEmail();

        /// <summary>
        /// Checks for any errors.
        /// </summary>
        /// <returns></returns>
        bool HasMessageErrorSummary();
    }
}