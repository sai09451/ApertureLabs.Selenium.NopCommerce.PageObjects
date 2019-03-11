using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminFooter;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainHeader;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminMainSideBar;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using System;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin
{
    /// <summary>
    /// The base page for all admin pages.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.IPageObject" />
    public interface IBasePage : IPageObject
    {
        /// <summary>
        /// Gets the main side bar.
        /// </summary>
        /// <value>
        /// The main side bar.
        /// </value>
        IAdminMainSideBarComponent MainSideBar { get; }

        /// <summary>
        /// Gets the navigation bar.
        /// </summary>
        /// <value>
        /// The navigation bar.
        /// </value>
        IAdminMainHeaderComponent NavigationBar { get; }

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        AdminFooterComponent Footer { get; }

        /// <summary>
        /// Back to top if displayed.
        /// </summary>
        void BackToTop();

        /// <summary>
        /// Determines whether the ajax busy element is present and visible.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is ajax busy]; otherwise, <c>false</c>.
        /// </returns>
        bool IsAjaxBusy();

        /// <summary>
        /// Determines whether this instance has notifications.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has notifications; otherwise, <c>false</c>.
        /// </returns>
        bool HasNotifications();

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="element">The element.</param>
        void HandleNotification(Action<IWebElement> element);

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        void DismissNotifications();
    }
}