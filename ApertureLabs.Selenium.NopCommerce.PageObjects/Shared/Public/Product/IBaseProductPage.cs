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
        /// Adds the product to the cart.
        /// </summary>
        /// <param name="productAttributesCommand">
        /// An action that will be passed the modal body that appears after
        /// clicking 'Add to cart' that appears asking the user to enter
        /// additional information. If null then the default handler is used.
        /// </param>
        void AddToCart(Func<IWebElement, IList<string>> productAttributesCommand = null);

        /// <summary>
        /// Retrieves a list of all payment plan options.
        /// </summary>
        /// <returns></returns>
        IList<IWebElement> GetPaymentPlanOptions();
    }
}