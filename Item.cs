using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleLogBook.Data
{
    public class Item
    {
        public string sku { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double weight { get; set; }
        public int productId { get; set; }
        public int categoryId { get; set; }
        public string taxName { get; set; }
        public double taxTotal { get; set; }
        public float taxValue { get; set; }
        public List<Tax> taxes { get; set; }

    }
}