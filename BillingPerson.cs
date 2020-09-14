using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleLogBook.Data
{
    public class BillingPerson
    {
        public string number { get; set; }
        public string name { get; set; }
        public string companyName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string postalCode { get; set; }
        public string stateOrProvinceCode { get; set; }
        public string stateName { get; set; }
        public string countryName { get; set; }
        public string phone { get; set; }
    }
}