using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public
{
    /// <summary>
    /// Base page for all public pages.
    /// </summary>
    public interface IBasePage : IPageObject
    {
        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        IAdminHeaderLinksComponent AdminHeaderLinks { get; }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        ICartPage GoToShoppingCart();

        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns></returns>
        bool IsLoggedIn();

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        IHomePage Login(string email, string password);

        /// <summary>
        /// Logs a user out if logged in.
        /// </summary>
        /// <returns></returns>
        T Logout<T>() where T : IPageObject;

        /// <summary>
        /// Used to search for a product.
        /// </summary>
        /// <param name="searchFor">Partial or full name of product.</param>
        /// <returns></returns>
        ISearchPage Search(string searchFor);

        /// <summary>
        /// Similar to <c>Search</c> but waits for the ajax results to resolve
        /// and returns those items.
        /// </summary>
        /// <param name="searchFor">The search for.</param>
        IReadOnlyCollection<IWebElement> SearchAjax(string searchFor);
    }
}