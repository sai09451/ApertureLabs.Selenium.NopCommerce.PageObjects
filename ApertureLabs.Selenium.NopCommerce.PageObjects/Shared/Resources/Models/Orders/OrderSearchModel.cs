using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureLabs.Selenium.NopCommerce.PageObjects.Shared.Resources.Models.Orders
{
    public class OrderSearchModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IList<int> OrderStatusIds { get; set; }
        public IList<int> PaymentStatusIds { get; set; }
        public IList<int> ShippingStatusIds { get; set; }
        public string PaymentMethodSystemName { get; set; }
        public int StoreId { get; set; }
        public int VendorId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public string BillingEmail { get; set; }
        public string BillingPhone { get; set; }
        public bool BillingPhoneEnabled { get; set; }
        public string BillingLastName { get; set; }
        public int BillingCountryId { get; set; }
        public string OrderNotes { get; set; }
        public string GoDirectlyToCustomOrderNumber { get; set; }
    }
}
