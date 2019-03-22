using System;
using System.Collections.Generic;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Orders
{
    /// <summary>
    /// Represents the model used to search orders on the <see cref="ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Admin.Order.IListPage"/>.
    /// </summary>
    public class OrderSearchModel
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the order statuses.
        /// </summary>
        /// <value>
        /// The order statuses.
        /// </value>
        public IList<string> OrderStatuses { get; set; }

        /// <summary>
        /// Gets or sets the payment statuses.
        /// </summary>
        /// <value>
        /// The payment statuses.
        /// </value>
        public IList<string> PaymentStatuses { get; set; }

        /// <summary>
        /// Gets or sets the shipping statuses.
        /// </summary>
        /// <value>
        /// The shipping statuses.
        /// </value>
        public IList<string> ShippingStatuses { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method system.
        /// </summary>
        /// <value>
        /// The name of the payment method system.
        /// </value>
        public string PaymentMethodName { get; set; }

        /// <summary>
        /// Gets or sets the store name.
        /// </summary>
        /// <value>
        /// The store name.
        /// </value>
        public string StoreName { get; set; }

        /// <summary>
        /// Gets or sets the vendor name.
        /// </summary>
        /// <value>
        /// The vendor name.
        /// </value>
        public string VendorName { get; set; }

        /// <summary>
        /// Gets or sets the warehouse identifier.
        /// </summary>
        /// <value>
        /// The warehouse identifier.
        /// </value>
        public int WarehouseId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the billing email.
        /// </summary>
        /// <value>
        /// The billing email.
        /// </value>
        public string BillingEmail { get; set; }

        /// <summary>
        /// Gets or sets the billing phone.
        /// </summary>
        /// <value>
        /// The billing phone.
        /// </value>
        public string BillingPhone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [billing phone enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [billing phone enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool BillingPhoneEnabled { get; set; }

        /// <summary>
        /// Gets or sets the last name of the billing.
        /// </summary>
        /// <value>
        /// The last name of the billing.
        /// </value>
        public string BillingLastName { get; set; }

        /// <summary>
        /// Gets or sets the billing country name.
        /// </summary>
        /// <value>
        /// The billing country name.
        /// </value>
        public string BillingCountryName { get; set; }

        /// <summary>
        /// Gets or sets the order notes.
        /// </summary>
        /// <value>
        /// The order notes.
        /// </value>
        public string OrderNotes { get; set; }

        /// <summary>
        /// Gets or sets the go directly to custom order number.
        /// </summary>
        /// <value>
        /// The go directly to custom order number.
        /// </value>
        public string GoDirectlyToCustomOrderNumber { get; set; }
    }
}
