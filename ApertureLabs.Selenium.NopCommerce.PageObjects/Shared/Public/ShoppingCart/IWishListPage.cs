using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.PageObjects;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart
{
    /// <summary>
    /// Represents the wishlist page.
    /// </summary>
    /// <seealso cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.IBasePage" />
    public interface IWishListPage : IBasePage
    {
        /// <summary>
        /// Retrieves the wishlist items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<WishlistRowComponent> GetItems();

        /// <summary>
        /// Updates the wishlist.
        /// </summary>
        /// <returns></returns>
        IWishListPage UpdateWishlist();

        /// <summary>
        /// Tries and adds the items marked as 'Add to cart' cart. Should throw
        /// an exception if the it fails to add the items to the cart.
        /// </summary>
        /// <returns></returns>
        ICartPage AddToCart();

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
        bool TryAddToCart<T>(out Exception exception,
            Action<ICartPage> resolve = null,
            Action<T> reject = null)
            where T : IPageObject;
    }
}
