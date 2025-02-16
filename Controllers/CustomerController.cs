using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.Data.Model.ModelDB;
using WebApplication1.Data.Models;
using WebApplication1.Data.Repositories;


namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private northwinds2Entities db = new northwinds2Entities();
        private CustomerRepository customerRepository;

        public CustomerController()
        {
            customerRepository = new CustomerRepository(db);
        }
        // GET: Customer
        public ActionResult Index(string message =null)
        {
            ViewBag.Message = message;
            var customers = customerRepository.GetCustomers();
            return View(customers);
            //return View(db.Customers.ToList());
            //return View(db.Products.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        
        public ActionResult Edit(int? id)
        {
            CustomerExt customer = id != null ? customerRepository.Find(id) : new CustomerExt();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = @"ID,CompanyName,ContactName,ContactTitle,Address,
            City,Region,PostalCode,Country,Phone,Fax")] CustomerExt customer) 
        {
            if (ModelState.IsValid) //good spot for break points
            {
                /*DbContext northwinds2Entities = db;
                northwinds2Entities.Entry(customer).State = EntityState.Modified;
                northwinds2Entities.SaveChanges();*/

                //db.Entry(customer).State = EntityState.Modified;
                //db.SaveChanges();

                var result = customerRepository.Update(customer);

                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();*/

            var success = customerRepository.Delete(id);
            string message;
            if (success)
            {
                message = "Delete Successful";
            }
            else
            {
                message = "Delete failed";
            }

            return RedirectToAction("Index", new { message }); //could pass multiple in like id=1, message
            //return RedirectToAction("Index");
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
            var success = SaveNameOnlyToDatabase(name,title, id);
            
            return Json (success , JsonRequestBehavior.AllowGet);
        }
        private bool SaveNameOnlyToDatabase(string name, string title, int id)
        {
            Customer customer= db.Customers.Find(id);
            if (customer != null)
            {
                customer.ContactName = name;
                customer.ContactTitle = title;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();


                return true;
            }
         
            return false;
        }

        public ActionResult SaveNameOnlyViaNameTitle(string name, string title, string companyName)
        {
            var success = SaveNameOnlyToDatabaseViaNameTitle(name, title, companyName);

            return Json(success, JsonRequestBehavior.AllowGet);
        }
        private bool SaveNameOnlyToDatabaseViaNameTitle(string name, string title, string companyName)
        {
            //DbSet<Customer> northwinds2Entities.Customers internally
            Customer customer = db.Customers.SingleOrDefault(Customer => Customer.CompanyName == companyName); //single or default returns null if not found

            if (customer != null)
            {
                customer.ContactName = name;
                customer.ContactTitle = title;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }

            return false;
        }

        //get ID function
        public ActionResult GetID(string name)
        {

            Customer customer = db.Customers.SingleOrDefault(Customer => Customer.ContactName == name);
            if (customer != null)
            {
                int ID = customer.ID;
                return Json(ID, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}
