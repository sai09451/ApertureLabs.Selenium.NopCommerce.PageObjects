using System;
using System.Collections.Generic;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Components.OrderSummary;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.ShoppingCart;
using ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Public.Order
{
    /// <summary>
    /// IOrderDetailsPage.
    /// </summary>
    public interface IOrderDetailsPage : IBasePage
    {
        /// <summary>
        /// Prints the order details.
        /// </summary>
        void Print();

        /// <summary>
        /// Downloads a pdf of the order details.
        /// </summary>
        void PdfInvoice();

        /// <summary>
        /// Gets the order number.
        /// </summary>
        /// <returns></returns>
        int GetOrderNumber();

        /// <summary>
        /// Gets the order date.
        /// </summary>
        /// <returns></returns>
        DateTime GetOrderDate();

        /// <summary>
        /// Gets the order status.
        /// </summary>
        /// <returns></returns>
        string GetOrderStatus();

        /// <summary>
        /// Gets the order total.
        /// </summary>
        /// <returns></returns>
        decimal GetOrderTotal();

        /// <summary>
        /// Gets the billing address.
        /// </summary>
        /// <returns></returns>
        AddressModel GetBillingAddress();

        /// <summary>
        /// Gets the shipping address.
        /// </summary>
        /// <returns></returns>
        AddressModel GetShippingAddress();

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <returns></returns>
        string GetShippingMethod();

        /// <summary>
        /// Gets the shipping status.
        /// </summary>
        /// <returns></returns>
        string GetShippingStatus();

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderSummaryReadOnlyRowComponent> GetProducts();

        /// <summary>
        /// Determines whether [has gift wrapping].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has gift wrapping]; otherwise, <c>false</c>.
        /// </returns>
        bool HasGiftWrapping();

        /// <summary>
        /// Gets the sub total.
        /// </summary>
        /// <returns></returns>
        decimal GetSubTotal();

        /// <summary>
        /// Gets the shipping.
        /// </summary>
        /// <returns></returns>
        decimal GetShippingCost();

        /// <summary>
        /// Gets the tax.
        /// </summary>
        /// <returns></returns>
        decimal GetTax();

        /// <summary>
        /// Gifts the card.
        /// </summary>
        /// <returns></returns>
        decimal? GiftCard();

        /// <summary>
        /// Re-orders the order.
        /// </summary>
        /// <returns></returns>
        ICartPage ReOrder();
    }
}
