using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7Elephant.Models
{
    public class OrdersViewModel
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal? OrderGrand_Total { get; set; }
        public string OrderStatus { get; set; }
    }
}