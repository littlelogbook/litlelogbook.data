using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleLogBook.Data
{
    public class Order
    {
        public int number { get; set; }
        public string vendorNumber { get; set; }
        public string externalTransactionId { get; set; }
        public string created { get; set; }
        public string paymentStatus { get; set; }
        public string oldPaymentStatus { get; set; }
        public string dateFlaggedAsPayed { get; set; }
        public string fulfillmentStatus { get; set; }
        public string dateFlaggedAsShipped { get; set; }
        public string oldFulfillmentStatus { get; set; }
        public string shippingMethod { get; set; }
        public ShippingPerson shippingPerson { get; set; }
        public string paymentMethod { get; set; }
        public BillingPerson billingPerson { get; set; }
        //public PaymentParameters paymentParameters { get; set; }
        public int customerId { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerIP { get; set; }
        public string customerCountryCodeByIP { get; set; }
        public double subtotalCost { get; set; }
        public string discountCoupon { get; set; }
        public double couponDiscountCost { get; set; }
        public double volumeDiscountCost { get; set; }
        public double discountCost { get; set; }
        public double shippingCost { get; set; }
        public double taxCost { get; set; }
        public double totalCost { get; set; }
        public List<Item> items { get; set; }
        public string lastChangeDate { get; set; }
    }
}
