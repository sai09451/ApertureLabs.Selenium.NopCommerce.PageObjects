using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Customer
{
    /// <summary>
    /// IChangePasswordPage.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IHasAccountNavigation{T}" />
    public interface IChangePasswordPage : IBasePage,
        IHasAccountNavigation<IChangePasswordPage>
    {
        /// <summary>
        /// Changes the password. Throws an exception if the password fails to
        /// update.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        IChangePasswordPage ChangePassword(string oldPassword,
            string newPassword);

        /// <summary>
        /// Enters the old password.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <returns></returns>
        IChangePasswordPage EnterOldPassword(string oldPassword);

        /// <summary>
        /// Enters the new password.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        IChangePasswordPage EnterNewPassword(string newPassword);

        /// <summary>
        /// Tries to save the changes. Resolve will be called upon success and
        /// reject will be called upon failure to update the changes.
        /// </summary>
        /// <param name="resolve">Can be null.</param>
        /// <param name="reject">Can be null.</param>
        /// <returns></returns>
        IChangePasswordPage Save(
            Action<IChangePasswordPage> resolve = null,
            Action<IChangePasswordPage> reject = null);
    }
}
