using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _7Elephant.Models;
using System.IO;

namespace _7Elephant
{
    public class PhonesManagementController : Controller
    {
        private PhoneEntities db = new PhoneEntities();

        // GET: PhonesManagement
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.BrandSortParm = String.IsNullOrEmpty(sortOrder) ? "Brand_desc" : "";

            var phone = from p in db.Phones
                        select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                phone = phone.Where(p => p.brand.ToUpper().Contains(searchString.ToUpper()) || p.model.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Brand_desc":
                    phone = phone.OrderByDescending(p => p.brand);
                    break;
                default:
                    phone = phone.OrderByDescending(p => p.product_id);
                    break;

            }

            return View(phone);
        }

        // GET: PhonesManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: PhonesManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhonesManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,brand,model,price,cost,url")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                    file.SaveAs(path);
                    phone.url = fileName;
                }
                db.Phones.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phone);
        }

        // GET: PhonesManagement/Edit/5
        public ActionResult Edit(int? id)
        {
        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: PhonesManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,brand,model,price,cost,url")] Phone phone)
        {
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                file.SaveAs(path);
                phone.url = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phone);
        }

        // GET: PhonesManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: PhonesManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
