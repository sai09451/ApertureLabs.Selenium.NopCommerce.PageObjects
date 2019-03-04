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
        /// Gets or sets the order status ids.
        /// </summary>
        /// <value>
        /// The order status ids.
        /// </value>
        public IList<int> OrderStatusIds { get; set; }

        /// <summary>
        /// Gets or sets the payment status ids.
        /// </summary>
        /// <value>
        /// The payment status ids.
        /// </value>
        public IList<int> PaymentStatusIds { get; set; }

        /// <summary>
        /// Gets or sets the shipping status ids.
        /// </summary>
        /// <value>
        /// The shipping status ids.
        /// </value>
        public IList<int> ShippingStatusIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method system.
        /// </summary>
        /// <value>
        /// The name of the payment method system.
        /// </value>
        public string PaymentMethodSystemName { get; set; }

        /// <summary>
        /// Gets or sets the store identifier.
        /// </summary>
        /// <value>
        /// The store identifier.
        /// </value>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the vendor identifier.
        /// </summary>
        /// <value>
        /// The vendor identifier.
        /// </value>
        public int VendorId { get; set; }

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
        /// Gets or sets the billing country identifier.
        /// </summary>
        /// <value>
        /// The billing country identifier.
        /// </value>
        public int BillingCountryId { get; set; }

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
