using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data.Model.ModelDB;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Controllers
{
    public class SupplierController : Controller
    {
        private northwinds2Entities db = new northwinds2Entities();

        // GET: Supplier
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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
        public ActionResult SaveNameOnly(string name, string title, int id)
        {
            var success = SaveNameOnlyToDatabase(name, title, id);

            return Json(success, JsonRequestBehavior.AllowGet);
        }
        private bool SaveNameOnlyToDatabase(string name, string title, int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                customer.ContactName = name;
                customer.ContactTitle = title;
                db.Entry(customer).State = EntityState.Modified;  //this is bad because it updates every field, better to attach an entity
                db.SaveChanges();


                return true;
            }

            return false;
        }
        public ActionResult GetProducts(int id)
        {
            var products = db.Products.Find(id);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ReturnSupplierInfo(int id)
        {
            
            var products = db.Products.Where(x => x.SupplierID == id).ToList();

            return Json(products.Select(x => new {productName=x.ProductName, quantityPerUnit=x.QuantityPerUnit, unitsInStock=x.UnitsInStock}), JsonRequestBehavior.AllowGet);
            
        }
        public JsonResult GetSuppliersJson() //JsonResult
        {
            System.Diagnostics.Debug.WriteLine("function reached GetSuppliersJson");
            var jsonProducts = db.Suppliers.ToList();
            

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            System.Diagnostics.Debug.WriteLine("function is about to return jsonProducts");

            //var productArray = conn.Query<ProductExt>(sql).ToList();

            var suppliers = db.Suppliers
            .Select(s => new 
            {
                s.SupplierID,
                s.CompanyName,
                s.ContactName,
                s.ContactTitle,
                s.Address,
                s.City,
                s.Region,
                s.PostalCode,
                s.Country,
                s.Phone,
                s.Fax,
                s.HomePage                
                
            }).ToList();

            string jsonSuppliers = JsonConvert.SerializeObject(suppliers, jsonSettings);
            return Json(jsonSuppliers, JsonRequestBehavior.AllowGet);

            //return Json(JsonConvert.SerializeObject(jsonProducts, jsonSettings), JsonRequestBehavior.AllowGet);
            //            return Json(jsonProducts, JsonRequestBehavior.AllowGet);


            /*using (northwinds2Entities context = new northwinds2Entities())
            {
                


                IQueryable<Supplier> supplierList = context.Suppliers
                    .Select(s => new Supplier
                    {
                        SupplierID = s.SupplierID,
                        CompanyName = s.CompanyName,
                        ContactName = s.ContactName,
                        ContactTitle = s.ContactTitle,
                        Address = s.Address,
                        City = s.City,
                        Region = s.Region,
                        PostalCode = s.PostalCode,
                        Country = s.Country,
                        Phone = s.Phone,
                        Fax = s.Fax,
                        HomePage = s.HomePage
                    });
                *//*foreach (var supplier in supplierList)
                {
                    Console.WriteLine(supplier.CompanyName);
                }*//*
            }*/

            

           
            /*Sql command
             SELECT SupplierID 
	            ,s.Address
	            ,s.City
	            ,s.CompanyName
	            ,s.ContactName
	            ,s.ContactName
	            ,s.Country
	            ,s.Fax
	            ,s.PostalCode
	            ,c.CategoryName
	            ,c.Description

            FROM Suppliers s
            left join Categories c on c.CategoryID = s.SupplierID;*/

            // return Json(JsonConvert.SerializeObject(conn.Query<ProductExt>(sql).ToList()), JsonRequestBehavior.AllowGet);
        }

    }
}
