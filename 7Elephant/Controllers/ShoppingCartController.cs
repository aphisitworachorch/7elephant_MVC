using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _7Elephant.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _7Elephant.Controllers
{
    public class ShoppingCartController : Controller
    {
        PhoneEntities pe = new Models.PhoneEntities();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        private int ifExisting(int id)
        {
            List<item> cart = (List<item>)Session["cart"];
            for(int i =0; i< cart.Count; i++)
            {
                if (cart[i].Phone.product_id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult AddtoCart(int id)
        {
            if(Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                cart.Add(new item(pe.Phones.Find(id), 1));
                Session["cart"] = cart;
            }else
            {
                List<item> cart = (List<item>)Session["cart"];
                int index = ifExisting(id);
                if(index == -1)
                {
                    cart.Add(new item(pe.Phones.Find(id), 1));
                }else
                {
                    cart[index].Quantity++;
                }
                Session["cart"] = cart;

            }

            return View("Cart");
        }
        public ActionResult Delete(int id)
        {
            int index = ifExisting(id);
            List<item> cart = (List<item>)Session["cart"];
            if(cart[index].Quantity > 1)
            {
                cart[index].Quantity--;
            }else
            {
                cart.RemoveAt(index);
            }
            Session["cart"] = cart;

            return View("cart");
        }
        public ActionResult Save_Order(FormCollection fc)
        {
            List<item> items = (List<item>)Session["cart"];
            decimal summary = 0;
            foreach (item it in (List<item>)Session["cart"])
            {
                summary += (it.Phone.price.Value * it.Quantity);
            }

            Order orders = new Order();
            orders.customer_id = User.Identity.GetUserId();
            orders.grand_total = summary;
            orders.card_no = fc["credit_card"];
            orders.order_date = DateTime.Now;
            orders.order_status = "New";
            pe.Order.Add(orders);
            pe.SaveChanges();

            foreach (item it in items)
            {
                Order_Details od = new Order_Details();
                od.order_id = pe.Order.Max(item => item.order_id);
                od.product_id = it.Phone.product_id;
                od.amount = it.Quantity;
                od.sub_total = (it.Phone.price * it.Quantity);
                pe.Order_Details.Add(od);
                pe.SaveChanges();

            }
            return View("Confirm");
        }
    }
}