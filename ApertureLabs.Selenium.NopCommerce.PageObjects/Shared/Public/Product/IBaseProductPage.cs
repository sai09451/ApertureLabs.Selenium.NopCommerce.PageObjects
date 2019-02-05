using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.ProductBreadCrumb;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Catalog;
using ApertureLabs.Selenium.PageObjects;
using OpenQA.Selenium;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Product
{
    /// <summary>
    /// Base class for the product pages.
    /// </summary>
    public interface IBaseProductPage : IBasePage, IViewModel<ProductDetailsModel>
    {
        /// <summary>
        /// Retrieves the bread crumb.
        /// </summary>
        ProductBreadCrumbComponent BreadCrumb { get; }

        /// <summary>
        /// Entering/removing codes on the product. These codes can be coupons,
        /// discounts, access codes, etc...
        /// </summary>
        ChallengeCodeComponent CodeHandler { get; }

        /// <summary>
        /// Checks if the product has a payment plan.
        /// </summary>
        bool HasPaymentPlan { get; }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="attributeElementHandler">The attribute element handler.</param>
        /// <param name="stringComparison">The string comparison.</param>
        IBaseProductPage SetAttribute(string attributeName,
            Action<IWebElement> attributeElementHandler,
            StringComparison stringComparison = StringComparison.Ordinal);

        /// <summary>
        /// Sets the attribute. The termPredicate will be passed in the term
        /// element (dt) of each term to determine which attribute to set.
        /// </summary>
        /// <param name="termPredicate">The term predicate.</param>
        /// <param name="attributeElementHandler">The attribute element handler.</param>
        /// <returns></returns>
        IBaseProductPage SetAttribute(Predicate<IWebElement> termPredicate,
            Action<IWebElement> attributeElementHandler);

        /// <summary>
        /// Adds the product to the cart. Throws an exception if it fails to
        /// add the product to the cart.
        /// </summary>
        IBaseProductPage AddToCart();

        /// <summary>
        /// Adds to cart and calls resolve/reject if the product was or wasn't
        /// added to the cart.
        /// </summary>
        /// <param name="resolve">The resolve.</param>
        /// <param name="reject">The reject.</param>
        /// <returns></returns>
        IBaseProductPage AddToCart(
            Action<IBaseProductPage> resolve,
            Action<IBaseProductPage> reject);

        /// <summary>
        /// Retrieves a list of all payment plan options.
        /// </summary>
        /// <returns></returns>
        IList<IWebElement> GetPaymentPlanOptions();

        /// <summary>
        /// Gets the full description.
        /// </summary>
        /// <returns></returns>
        string GetFullDescription();

        /// <summary>
        /// Gets the short description.
        /// </summary>
        /// <returns></returns>
        string GetShortDescription();

        /// <summary>
        /// Gets the product tags.
        /// </summary>
        /// <returns></returns>

        IEnumerable<string> GetProductTags();

        /// <summary>
        /// Sets the quantity.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns></returns>
        IBaseProductPage SetQuantity(int quantity);
    }
}