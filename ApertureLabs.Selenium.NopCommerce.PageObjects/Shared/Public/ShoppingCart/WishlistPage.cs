﻿using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.Extensions;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.AdminHeaderLinks;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Catalog;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Home;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart
{
    /// <summary>
    /// The wishlist page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.PageObjects.PageObject" />
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart.IWishListPage" />
    public class WishlistPage : StaticPageObject, IWishListPage
    {
        #region Fields

        #region Selectors

        private readonly By addToCartSelector = By.CssSelector("*[name='addtocartbutton']");
        private readonly By updateWishlistSelector = By.CssSelector("*[name='updatecart']");
        private readonly By wishListRowsSelector = By.CssSelector(".wishlist-content tbody tr");

        #endregion

        private readonly IBasePage basePage;
        private readonly IPageObjectFactory pageObjectFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WishlistPage"/> class.
        /// </summary>
        /// <param name="basePage">The base page.</param>
        /// <param name="pageObjectFactory">The page object factory.</param>
        /// <param name="driver">The driver.</param>
        /// <param name="pageSettings">The page settings.</param>
        public WishlistPage(IBasePage basePage,
            IPageObjectFactory pageObjectFactory,
            IWebDriver driver,
            PageSettings pageSettings)
            : base(driver,
                  new Uri(pageSettings.BaseUrl, "wishlist"))
        {
            this.basePage = basePage;
            this.pageObjectFactory = pageObjectFactory;
        }

        #endregion

        #region Properties

        #region Elements

        private IWebElement AddToCartElement => WrappedDriver
            .FindElement(addToCartSelector);

        private IWebElement UpdateWishlistElement => WrappedDriver
            .FindElement(updateWishlistSelector);

        private IReadOnlyCollection<IWebElement> RowElements => WrappedDriver
            .FindElements(wishListRowsSelector);

        #endregion

        /// <summary>
        /// Gets the admin header links.
        /// </summary>
        public virtual IAdminHeaderLinksComponent AdminHeaderLinks => basePage.AdminHeaderLinks;

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

            return this;
        }

        /// <summary>
        /// Tries and adds the items marked as 'Add to cart' cart. Should throw
        /// an exception if the it fails to add the items to the cart.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">After attempting to add the items to " +
        ///                     "the cart, the page didn't naviage to the cart.</exception>
        public virtual ICartPage AddToCart()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .UntilPageReloads(AddToCartElement, e => e.Click());

            var cartPage = pageObjectFactory.PreparePage<ICartPage>();

            if (cartPage.IsStale())
            {
                throw new Exception("After attempting to add the items to " +
                    "the cart, the page didn't naviage to the cart.");
            }

            return cartPage;
        }

        /// <summary>
        /// Dismisses the notifications.
        /// </summary>
        public virtual void DismissNotifications()
        {
            basePage.DismissNotifications();
        }

        /// <summary>
        /// Retrieves the wishlist items.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IEnumerable<WishlistRowComponent> GetItems()
        {
            foreach (var item in RowElements)
            {
                yield return pageObjectFactory.PrepareComponent(
                    new WishlistRowComponent(
                        ByElement.FromElement(item),
                        pageObjectFactory,
                        WrappedDriver));
            }
        }

        /// <summary>
        /// Goes to the shopping cart page.
        /// </summary>
        /// <returns></returns>
        public virtual ICartPage GoToShoppingCart()
        {
            return basePage.GoToShoppingCart();
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
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLoggedIn()
        {
            return basePage.IsLoggedIn();
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual IHomePage Login(string email, string password)
        {
            return basePage.Login(email, password);
        }

        /// <summary>
        /// Logs a user out if logged in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T Logout<T>() where T : IPageObject
        {
            return basePage.Logout<T>();
        }

        /// <summary>
        /// Used to search for a product.
        /// </summary>
        /// <param name="searchFor">Partial or full name of product.</param>
        /// <returns></returns>
        public virtual ISearchPage Search(string searchFor)
        {
            return basePage.Search(searchFor);
        }

        /// <summary>
        /// Similar to <c>Search</c> but waits for the ajax results to resolve
        /// and returns those items.
        /// </summary>
        /// <param name="searchFor">The search for.</param>
        /// <returns></returns>
        public virtual IReadOnlyCollection<IWebElement> SearchAjax(string searchFor)
        {
            return basePage.SearchAjax(searchFor);
        }


        /// <summary>
        /// Tries the add the marked items to the cart. After the attempt
        /// resolve or reject will be called (if they're not null) upon success
        /// or failure respectively.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="resolve">Resolve handler. Can be null.</param>
        /// <param name="reject">Reject handler. Can be null.</param>
        /// <returns></returns>
        public virtual bool TryAddToCart<T>(out Exception exception,
            Action<ICartPage> resolve = null,
            Action<T> reject = null)
            where T : IPageObject
        {
            exception = null;

            try
            {
                AddToCart();
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception == null)
            {
                var cartPage = pageObjectFactory.PreparePage<ICartPage>();
                resolve(cartPage);
            }
            else
            {
                var errorPage = pageObjectFactory.PreparePage<T>();
                reject(errorPage);
            }

            return exception == null;
        }

        /// <summary>
        /// Updates the wishlist.
        /// </summary>
        /// <returns></returns>
        public virtual IWishListPage UpdateWishlist()
        {
            WrappedDriver
                .Wait(TimeSpan.FromSeconds(5))
                .UntilPageReloads(UpdateWishlistElement, e => e.Click());

            // Page should have reloaded.
            Load();

            return this;
        }

        #endregion
    }
}
