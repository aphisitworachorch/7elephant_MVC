using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _7Elephant.Models;

namespace _7Elephant.Controllers
{
    public class item
    {
        private Phone phone = new Phone(); 

        public Phone Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public item()
        {

        }
        public item(Phone phone, int quantity)
        {
            this.phone = phone;
            this.quantity = quantity;
        }
    }
}