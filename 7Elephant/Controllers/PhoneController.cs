using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _7Elephant.Models;

namespace _7Elephant.Controllers
{
    public class PhoneController : Controller
    {
        PhoneEntities pe = new PhoneEntities();

        // GET: Phone
        public ActionResult Index()
        {
            ViewBag.ListProduct = pe.Phones.ToList();
            return View();
        }
    }
}