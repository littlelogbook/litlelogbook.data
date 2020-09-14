using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleLogBook.Data
{
    public class RootObject
    {
        public int count { get; set; }
        public int total { get; set; }
        public List<Order> orders { get; set; }
    }
}